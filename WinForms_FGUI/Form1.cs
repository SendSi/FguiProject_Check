using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WinForms_FGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string mFGUIPath = @"C:/fguiPath.txt";
        string mFGUILog = @"C:/fguiLog.txt";
        Dictionary<string, string> mPackageUIdNameDic = new Dictionary<string, string>();//key=包id,,,value=包名字
        Dictionary<string, List<string>> mXmlListDic = new Dictionary<string, List<string>>();
        Dictionary<string, string> mCommonNameUIdDic;// = new Dictionary<string, string>() { { "Common", "" }, { "Items", "" }, { "Font", "" }, };//公有包  key=包名字,,,value=包id
        Dictionary<string, string> mNoneCommonUIdNameDic;//= new Dictionary<string, string>() { { "Common", "" }, { "Items", "" }, { "Font", "" }, };//公有包  key=包名字,,,value=包id
        private void btnPackage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ignoreTxt.Text))
            {
                return;
            }
            var strs = this.ignoreTxt.Text.Split(";");
            mCommonNameUIdDic = new Dictionary<string, string>();
            for (int i = 0; i < strs.Length; i++)
            {
                mCommonNameUIdDic.Add(strs[i], "");
            }

            var contentTxt = this.fguiPath.Text + "_*_" + this.ignoreTxt.Text + "_*_" + this.fguiPKGTxt.Text + "_*_" + this.ignoreIconCommon.Text;
            File.WriteAllText(mFGUIPath, contentTxt);

            mPackageUIdNameDic.Clear();
            mNoneCommonUIdNameDic = new Dictionary<string, string>();
            string directoryPath = this.fguiPath.Text;// 目录路径
            if (Directory.Exists(directoryPath) == false)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            string packagePattern = @"id=""([^""]+)""";

            string[] strTxt;

            foreach (string file in xmlFiles)
            {
                if (file.Contains("package.xml"))
                {
                    strTxt = File.ReadAllLines(file);

                    if (strTxt[1].Contains("packageDescription"))
                    {
                        string packageUId = Regex.Match(strTxt[1], packagePattern).Groups[1].Value;
                        var tSplit = file.Split("\\", StringSplitOptions.None);
                        var packageName = tSplit[tSplit.Length - 2];
                        mPackageUIdNameDic[packageUId] = packageName;
                        if (mCommonNameUIdDic.ContainsKey(packageName))
                        {
                            mCommonNameUIdDic[packageName] = packageUId;
                        }
                        else
                        {
                            mNoneCommonUIdNameDic[packageUId] = packageName;
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in mPackageUIdNameDic)
            {
                sb.AppendLine(item.Key + "__" + item.Value);// +";");
            }
            this.txtConsole.Text = sb.ToString();
        }


        private void btnRef_Click(object sender, EventArgs e)
        {
            if (mCommonNameUIdDic == null)
            {
                MessageBox.Show("请先点击 拥有的包 按钮");
                return;
            }
            mXmlListDic.Clear();
            string directoryPath = this.fguiPath.Text;// 目录路径
            string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            string pkgPattern = @"pkg=""([^""]+)""";
            string urlPattern = @"url=""([^""]+)""";//url="ui://m9pqa398gbxfe9l" 
            string iconPattern = @"icon=""([^""]+)""";//icon="ui://m9pqa398gbxfe9l" 
            string fontPattern = @"font=""([^""]+)""";//font="ui://m9pqa398gbxfe9l" 
            string defaultPattern = @"defaultItem=""([^""]+)""";//defaultItem="ui://m9pqa398gbxfe9l" 
            string namePattern = @"name=""([^""]+)""";//名字
            string[] strTxt;
            var contentLog = "";
            foreach (string file in xmlFiles)
            {
                if (file.Contains("LimitShopRewardView"))
                {
                    Console.WriteLine("测试某个页面依赖");
                }
                if (file.Contains("package.xml") == false)
                {
                    strTxt = File.ReadAllLines(file);

                    var tSplit = file.Split("\\", StringSplitOptions.None);
                    var xmlName = tSplit[tSplit.Length - 1]; //***.xml                 
                    var tIndex = 0;
                    for (int i = 0; i < tSplit.Length; i++)
                    {
                        if (tSplit[i] == "assets")
                        {
                            tIndex = i;
                            break;
                        }
                    }
                    var selfPack = tSplit[tIndex + 1];

                    for (int i = 0; i < strTxt.Length; i++)
                    {
                        string packageUId = Regex.Match(strTxt[i], pkgPattern).Groups[1].Value;//某包id
                        string urlUIAll = Regex.Match(strTxt[i], urlPattern).Groups[1].Value;//url全路径
                        string iconUIAll = Regex.Match(strTxt[i], iconPattern).Groups[1].Value;//url全路径
                        string fontUIAll = Regex.Match(strTxt[i], fontPattern).Groups[1].Value;//url全路径
                        string defaultUIAll = Regex.Match(strTxt[i], defaultPattern).Groups[1].Value;//url全路径

                        if (string.IsNullOrEmpty(packageUId) == false)
                        {
                            var packageName = "";
                            mPackageUIdNameDic.TryGetValue(packageUId, out packageName);
                            //var packageName = mPackageUIdNameDic.ContainsKey(packageUId)? mPackageUIdNameDic[packageUId]:"";//某包名字
                            if (string.IsNullOrEmpty(packageName) == false && mCommonNameUIdDic.ContainsKey(packageName) == false && packageName != selfPack)
                            {
                                AddDicTry(selfPack, xmlName, packageName);

                                string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value;//元素名字
                                contentLog += xmlName + "-->" + eleName + "\r\n";
                            }
                        }
                        else if (string.IsNullOrEmpty(urlUIAll) == false)//懒 得 去提取了      
                        {
                            foreach (var item in mNoneCommonUIdNameDic)//key=包id,,,value=包名字
                            {
                                if (urlUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value;//元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(iconUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic)//key=包id,,,value=包名字
                            {
                                if (iconUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value;//元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(defaultUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic)//key=包id,,,value=包名字
                            {
                                if (defaultUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value;//元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(fontUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic)//key=包id,,,value=包名字
                            {
                                if (fontUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value;//元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1.可能丢失的引用 也计算在内了(若FGUI中查询我的依赖的资源 并没找到时,应该是丢失了,理论上丢失的也要把其清空的)  2.只能找到直接引用,间接引用无法查询");
            sb.AppendLine();
            foreach (var item in mXmlListDic)
            {
                var str = "";
                for (int i = 0; i < item.Value.Count; i++)
                {
                    str += item.Value[i] + ";";
                }
                sb.AppendLine(item.Key + " 依赖了其他业务包 : " + str);
            }
            this.txtConsole.Text = sb.ToString() + "\r\n\r\n修正时,看这个方便点xml-->元素\r\n" + contentLog;

        }

        void AddDicTry(string selfPack, string xmlName, string Value)
        {
            var name = selfPack + "包的" + xmlName;
            if (mXmlListDic.ContainsKey(name))
            {
                if (mXmlListDic[name].Contains(Value) == false)
                {
                    mXmlListDic[name].Add(Value);
                }
            }
            else
            {
                mXmlListDic[name] = new List<string>();
                mXmlListDic[name].Add(Value);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ignoreIconCommon.Text.Length <= 2 || this.ignoreIconCommon.Text.Contains(";") == false)
            {
                return;
            }

            var ignoreList = this.ignoreIconCommon.Text.Split(";");

            string directoryPath;
            if (this.fguiPKGTxt.Text.Length <= 2)
            {
                directoryPath = this.fguiPath.Text;//全路径
            }
            else
            {
                directoryPath = this.fguiPath.Text + "/" + this.fguiPKGTxt.Text;// 目录路径
            }
            var isPath = (directoryPath.Contains("Fgui") || directoryPath.Contains("fgui") || directoryPath.Contains("Project"));
            if (isPath == false)
            {
                MessageBox.Show("你的目录咋不含Fgui | fgui | Project 字眼___判定为非法目录");
                return;
            }
            if (Directory.Exists(directoryPath) == false)
            {
                MessageBox.Show("目录不存在");
                return;
            }
            var contentTxt = this.fguiPath.Text + "_*_" + this.ignoreTxt.Text + "_*_" + this.fguiPKGTxt.Text + "_*_" + this.ignoreIconCommon.Text;
            File.WriteAllText(mFGUIPath, contentTxt);

            string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            string packagePattern = @"id=""([^""]+)""";

            string[] strTxt;

            string packageId = string.Empty; ;
            Dictionary<string, string> idNameDic = new Dictionary<string, string>();
            Dictionary<string, string> pathPngDic = new Dictionary<string, string>();
            Dictionary<string, string> urlIdDic = new Dictionary<string, string>();
            string idPattern = @"id=""([^""]+)""";
            string namePattern = @"name=""([^""]+)""";
            string pathPattern = @"path=""([^""]+)""";
            foreach (string file in xmlFiles)
            {
                if (file.Contains(ignoreList[0]) == false && file.Contains("package.xml"))
                {
                    strTxt = File.ReadAllLines(file);
                    packageId = string.Empty;
                    for (int i = 0; i < strTxt.Length; i++)
                    {
                        if (strTxt[i].Contains("image id"))
                        {
                            string idValue = Regex.Match(strTxt[i], idPattern).Groups[1].Value;
                            string nameValue = Regex.Match(strTxt[i], namePattern).Groups[1].Value;
                            string pathValue = Regex.Match(strTxt[i], pathPattern).Groups[1].Value;
                            idNameDic[idValue] = nameValue;
                            pathPngDic[idValue] = file.Replace("package.xml", "") + (pathValue + nameValue);

                            if (strTxt[1].Contains("packageDescription"))
                            {
                                string packageValue = Regex.Match(strTxt[1], packagePattern).Groups[1].Value;
                                urlIdDic["ui://" + packageValue + idValue] = idValue;
                            }
                        }
                    }
                }
            }

            string srcPattern = @"src=""([^""]+)""";
            string urlPattern = @"url=""([^""]+)""";
            string iconPattern = @"icon=""([^""]+)""";
            string iconSelectPattern = @"selectedIcon=""([^""]+)""";
            string valusePattern = @"values=""([^""]+)""";
            string defaultPattern = @"default=""([^""]+)""";
            string iconItemPattern = @"icon=""([^""]+)""";//      <item icon="ui://qllwua2i9mq2w57"/>
            string currTxt_I = "";
            if (this.fguiPKGTxt.Text == ignoreList[1])
            {
                xmlFiles = Directory.GetFiles(this.fguiPath.Text, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            }
            foreach (string file in xmlFiles)
            {
                if (file.Contains("package.xml") == false)
                {
                    //if (file.Contains("RankView"))
                    //{
                    strTxt = File.ReadAllLines(file);
                    for (int i = 0; i < strTxt.Length; i++)
                    {
                        currTxt_I = strTxt[i];
                        {
                            if (currTxt_I.Contains("src"))
                            {
                                string srcValue = Regex.Match(currTxt_I, srcPattern).Groups[1].Value;
                                if (idNameDic.ContainsKey(srcValue) == true)
                                {
                                    idNameDic.Remove(srcValue);
                                }
                            }
                            else if (currTxt_I.Contains("url"))
                            {
                                string srcValue = Regex.Match(currTxt_I, urlPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(srcValue) == true)
                                {
                                    idNameDic.Remove(urlIdDic[srcValue]);
                                }
                            }
                            else if (currTxt_I.Contains("Button"))
                            {
                                string srcValue = Regex.Match(currTxt_I, iconPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(srcValue) == true)
                                {
                                    idNameDic.Remove(urlIdDic[srcValue]);
                                }

                                string srcValue2 = Regex.Match(currTxt_I, iconSelectPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(srcValue2) == true)
                                {
                                    idNameDic.Remove(urlIdDic[srcValue2]);
                                }
                            }
                            else if (currTxt_I.Contains("<item "))
                            {
                                string srcValue = Regex.Match(currTxt_I, iconItemPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(srcValue) == true)
                                {
                                    idNameDic.Remove(urlIdDic[srcValue]);
                                }

                                string srcValue2 = Regex.Match(currTxt_I, iconSelectPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(srcValue2) == true)
                                {
                                    idNameDic.Remove(urlIdDic[srcValue2]);
                                }
                            }
                            else if (currTxt_I.Contains("controller") && (currTxt_I.Contains("values=\"ui://") || currTxt_I.Contains("values=\"|")))
                            {
                                string srcValue = Regex.Match(currTxt_I, valusePattern).Groups[1].Value;
                                var ctrlIcons = srcValue.Split('|');
                                foreach (var item in ctrlIcons)
                                {
                                    if (urlIdDic.ContainsKey(item) == true)
                                    {
                                        idNameDic.Remove(urlIdDic[item]);
                                    }
                                }

                                string defalutValue = Regex.Match(currTxt_I, defaultPattern).Groups[1].Value;
                                if (urlIdDic.ContainsKey(defalutValue) == true)
                                {
                                    idNameDic.Remove(urlIdDic[defalutValue]);
                                }
                            }
                        }
                        //}
                    }
                }
            }
            List<MySortPng> listSort = new List<MySortPng>();
            foreach (var item in idNameDic)
            {
                if (File.Exists(pathPngDic[item.Key]))
                {
                    Image image = Image.FromFile(pathPngDic[item.Key]);
                    listSort.Add(new MySortPng
                    {
                        rectArea = (image.Width * image.Height),
                        outLineTxt = string.Format("{0}    长{1},高{2},面积{3}", item.Value, image.Width, image.Height, (image.Width * image.Height))
                    });
                    image.Dispose();
                }
            }
            var newlistSort = listSort.OrderByDescending(o => o.rectArea);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("已为倒序了 优先处理大的碎图\r\n");
            foreach (var item in newlistSort)
            {
                sb.AppendLine(item.outLineTxt);
            }
            this.txtConsole.Text = sb.ToString();


            Console.WriteLine("共计 " + idNameDic.Count);
        }


        private void fguiPKGTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string txtContent;
            if (File.Exists(mFGUIPath))
            {
                txtContent = File.ReadAllText(mFGUIPath);
            }
            else
            {
                txtContent = @"G:\Bingganren2021_SVN\client\FguiProject\assets_*_Common;Items;Font_*_Common_*_Items;Common";
                File.WriteAllText(mFGUIPath, txtContent);
            }
            var strs = txtContent.Split("_*_");
            this.fguiPath.Text = strs[0];
            this.ignoreTxt.Text = strs[1];
            this.fguiPKGTxt.Text = strs[2];
            this.ignoreIconCommon.Text = strs[3];
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void checkImgBtn_Click(object sender, EventArgs e)
        {
            //string inputDirectory = @"C:\TestImg";
            string inputDirectory = this.fguiPath.Text;// -- @"C:\TestImg";       

            string[] imageFiles = Directory.GetFiles(inputDirectory, "*.png", SearchOption.AllDirectories);     // 获取输入目录下所有图片文件    
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>();        // 字典用于存储哈希值及其对应的文件路径列表

            foreach (var imageFile in imageFiles)
            {
                string hash = GetImageHash(imageFile);         // 计算图片文件的哈希值

                // 将哈希值和文件路径添加到字典中
                if (!hashDictionary.ContainsKey(hash))
                {
                    hashDictionary[hash] = new List<string>();
                }
                hashDictionary[hash].Add(imageFile);
            }


            List<IgnoreImg> ignoreList = new List<IgnoreImg>();

            // 输出具有相同哈希值的图片文件
            foreach (var hashEntry in hashDictionary)
            {
                if (hashEntry.Value.Count > 1)
                {
                    Image tmp = Image.FromFile(hashEntry.Value[0]);
                    List<string> list = new List<string>();
                    foreach (var imagePath in hashEntry.Value)
                    {
                        string path = imagePath.Replace(this.fguiPath.Text, "");
                        list.Add(path);
                    }
                    ignoreList.Add(new IgnoreImg(tmp.Width * tmp.Height, list));

                }
            }

            ignoreList.Sort((a, b) =>
            {
                return a.size < b.size ? 1 : -1;
            });

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("已为倒序了 优先处理大的相同碎图\r\n");
            var index = 0;
            foreach (var iList in ignoreList)
            {
                index++;
                sb.AppendLine($"第{index}张,面积是{iList.size},路径有-->{this.fguiPath.Text}");
                foreach (var imgPath in iList.imgs)
                {
                    sb.AppendLine(imgPath);
                }
                sb.AppendLine("");
            }

            this.txtConsole.Text = sb.ToString();
        }

        class IgnoreImg
        {
            public int size;
            public List<string> imgs;
            public IgnoreImg(int size, List<string> tags)
            {
                this.size = size;
                this.imgs = tags;
            }
        }
        static string GetImageHash(string imagePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(imagePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }
    }

    public class MySortPng
    {
        public string outLineTxt { get; set; }
        public int rectArea { get; set; }
    }
}