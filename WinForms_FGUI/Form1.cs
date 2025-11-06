using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WinForms_FGUI
{
    public partial class Form1 : Form
    {
        string mSaveTxtPath;

        public Form1()
        {
            InitializeComponent();
            mSaveTxtPath = Path.Combine(Application.StartupPath, "checkFGUI.txt");
        }

        void SaveFguiPath()
        {
            string contentTxt = this.fguiProjectPath.Text + "_*_" + this.txt_Ignore.Text + "_*_" + this.txtNoneImg.Text + "_*_" + this.txtNoneCom.Text + "_*_" + this.txtScriptJson.Text;
            File.WriteAllText(mSaveTxtPath, contentTxt);
        }

        Dictionary<string, string> mPackageUIdNameDic = new Dictionary<string, string>(); //key=包id,,,value=包名字
        Dictionary<string, List<string>> mXmlListDic = new Dictionary<string, List<string>>();
        Dictionary<string, string> mCommonNameUIdDic; // = new Dictionary<string, string>() { { "Common", "" }, { "Items", "" }, { "Font", "" }, };//公有包  key=包名字,,,value=包id
        Dictionary<string, string> mNoneCommonUIdNameDic; //= new Dictionary<string, string>() { { "Common", "" }, { "Items", "" }, { "Font", "" }, };//公有包  key=包名字,,,value=包id

        private void Form1_Load(object sender, EventArgs e)
        {
            globalTip.AutoPopDelay = 25000;
            globalTip.InitialDelay = 100;
            globalTip.ReshowDelay = 0;
            globalTip.ShowAlways = true;
            globalTip.SetToolTip(this.btn_GlobalCom, "这个会全局搜索下,一般[非业务包]用这个____[业务包]用左边的就够了_全局搜索比较慢_卡");
            globalTip.SetToolTip(this.btn_GlobalImg, "这个会全局搜索下,一般[非业务包]用这个____[业务包]用左边的就够了_全局搜索比较慢_卡");
            globalTip.SetToolTip(this.btnPackage, "可查看本项目的所有包,好copy去查询");
            globalTip.SetToolTip(this.checkImgBtn, "同一张相同的图片 可能在多个包中,得考虑一下[大图]是否要挪到公共包呢");
            globalTip.SetToolTip(this.btnRef, "理论上,[业务包]不会去依赖[业务包]的___[业务包]仅可依赖[本包]与[公共包]");
            globalTip.SetToolTip(this.lblCommon, "有格式要求的,用分号分隔开..最后一个是策划配的包.不参与检测的");
            globalTip.SetToolTip(this.lblNoneCom, "若空白不填,则全去检测.大搜索要耗时哦");
            globalTip.SetToolTip(this.lblNoneImg, "若空白不填,则全去检测.大搜索要耗时哦");
            globalTip.SetToolTip(this.txtScriptJson, "可为空;;;;若不为空,查无引用图时,则也检测文本目录是否有对应的字符串(*.json|*.cs|*.lua),搜索要耗时哦");
            globalTip.SetToolTip(this.cbExtend, "若勾上了,则仅检测[导出]的[组件\\图片];;;;若不勾上,则检测所有的[组件\\图片]\r\nTips:此复选框[用途],暂舍不得删无用的[组\\图],留着备用");

            string txtContent;
            if (File.Exists(mSaveTxtPath))
            {
                txtContent = File.ReadAllText(mSaveTxtPath);
            }
            else
            {
                txtContent = @"D:\WorkProject\UnityClient\Unity\FGUIProject\assets_*_Common;ItemPKG_*_Common_*_Common_*_D:\WorkProject\UnityClient\Unity\Assets";
                File.WriteAllText(mSaveTxtPath, txtContent);
                // 显示 MessageBox
                DialogResult result = MessageBox.Show(
                    "首次进来，请先设置 FGUI 路径。使用过后，下次就不用再设置了。\n\n" +
                    "首次点[确定]在网址看看解说吧----->另：记得点个赞",
                    "提示",
                    MessageBoxButtons.OK, // 提供“确定”和“取消”按钮
                    MessageBoxIcon.Information
                );
                // 如果用户点击“确定”，则打开 GitHub 链接
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://github.com/SendSi/FguiProject_Check",
                            UseShellExecute = true // 关键设置！
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"无法打开链接：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            var strs = txtContent.Split("_*_");
            if (strs.Length < 5) //增加功能  _*_增加了框框,值不够  就重新设置一下默认值
            {
                txtContent = @"D:\WorkProject\UnityClient\Unity\FGUIProject\assets_*_Common;ItemPKG_*_Common_*_Common_*_D:\WorkProject\UnityClient\Unity\Assets";
                File.WriteAllText(mSaveTxtPath, txtContent);
                strs = txtContent.Split("_*_");
            }

            this.fguiProjectPath.Text = strs[0];
            this.txt_Ignore.Text = strs[1];
            this.txtNoneImg.Text = strs[2];
            this.txtNoneCom.Text = strs[3];
            this.txtScriptJson.Text = strs[4]; //增加框框时

            txtNoneCom_TextChanged(null, null);
            txtNoneImg_TextChanged(null, null);
        }

        private async void btnPackage_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(btnPackage_Click22);
        }

        void btnPackage_Click22()
        {
            if (string.IsNullOrEmpty(this.txt_Ignore.Text))
            {
                return;
            }

            var strs = this.txt_Ignore.Text.Split(";");
            mCommonNameUIdDic = new Dictionary<string, string>();
            for (int i = 0; i < strs.Length; i++)
            {
                mCommonNameUIdDic.Add(strs[i], "");
            }

            SaveFguiPath();
            mPackageUIdNameDic.Clear();
            mNoneCommonUIdNameDic = new Dictionary<string, string>();
            string directoryPath = this.fguiProjectPath.Text; // 目录路径
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
                sb.AppendLine(item.Key + "__" + item.Value); // +";");
            }

            this.txtConsole.Text = sb.ToString();
        }

        private async void btnRef_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(btnRef_Click22);
        }

        void btnRef_Click22()
        {
            btnPackage_Click(null, null);

            mXmlListDic.Clear();
            string directoryPath = this.fguiProjectPath.Text; // 目录路径
            string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            string pkgPattern = @"pkg=""([^""]+)""";
            string urlPattern = @"url=""([^""]+)"""; //url="ui://m9pqa398gbxfe9l" 
            string iconPattern = @"icon=""([^""]+)"""; //icon="ui://m9pqa398gbxfe9l" 
            string fontPattern = @"font=""([^""]+)"""; //font="ui://m9pqa398gbxfe9l" 
            string defaultPattern = @"defaultItem=""([^""]+)"""; //defaultItem="ui://m9pqa398gbxfe9l" 
            string namePattern = @"name=""([^""]+)"""; //名字
            string[] strTxt;
            var contentLog = "";
            foreach (string file in xmlFiles)
            {
                //if (file.Contains("LimitShopRewardView"))
                //{
                //    Console.WriteLine("测试某个页面依赖");
                //}

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
                        string packageUId = Regex.Match(strTxt[i], pkgPattern).Groups[1].Value; //某包id
                        string urlUIAll = Regex.Match(strTxt[i], urlPattern).Groups[1].Value; //url全路径
                        string iconUIAll = Regex.Match(strTxt[i], iconPattern).Groups[1].Value; //url全路径
                        string fontUIAll = Regex.Match(strTxt[i], fontPattern).Groups[1].Value; //url全路径
                        string defaultUIAll = Regex.Match(strTxt[i], defaultPattern).Groups[1].Value; //url全路径

                        if (string.IsNullOrEmpty(packageUId) == false)
                        {
                            var packageName = "";
                            mPackageUIdNameDic.TryGetValue(packageUId, out packageName);
                            //var packageName = mPackageUIdNameDic.ContainsKey(packageUId)? mPackageUIdNameDic[packageUId]:"";//某包名字
                            if (string.IsNullOrEmpty(packageName) == false && mCommonNameUIdDic.ContainsKey(packageName) == false && packageName != selfPack)
                            {
                                AddDicTry(selfPack, xmlName, packageName);

                                string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value; //元素名字
                                contentLog += xmlName + "-->" + eleName + "\r\n";
                            }
                        }
                        else if (string.IsNullOrEmpty(urlUIAll) == false) //懒 得 去提取了      
                        {
                            foreach (var item in mNoneCommonUIdNameDic) //key=包id,,,value=包名字
                            {
                                if (urlUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value; //元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(iconUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic) //key=包id,,,value=包名字
                            {
                                if (iconUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value; //元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(defaultUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic) //key=包id,,,value=包名字
                            {
                                if (defaultUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value; //元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                        else if (string.IsNullOrEmpty(fontUIAll) == false)
                        {
                            foreach (var item in mNoneCommonUIdNameDic) //key=包id,,,value=包名字
                            {
                                if (fontUIAll.Contains(item.Key) && item.Value != selfPack)
                                {
                                    AddDicTry(selfPack, xmlName, item.Value);
                                    string eleName = Regex.Match(strTxt[i], namePattern).Groups[1].Value; //元素名字
                                    contentLog += xmlName + "-->" + eleName + "\r\n";
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"1.可能丢失的引用 也计算在内了(若FGUI中查询我的依赖的资源 并没找到时,应该是丢失了,理论上丢失的也要把其清空的)  2.只能找到直接引用,间接引用无法查询 time={DateTime.Now.ToString("HH:mm:ss")}");
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

        private async Task ExecuteWithTimerAsync(Action action, Action<Exception> errorHandler = null)
        {
            var originalTitle = this.Text;
            this.Text = $"{originalTitle}（开始啦）";
            var timer = new System.Windows.Forms.Timer { Interval = 1000 };
            int elapsedSeconds = 0;
            timer.Tick += (s, ev) =>
            {
                elapsedSeconds++;
                this.Text = $"{originalTitle}（执行第{elapsedSeconds}秒）    这里在读秒就别点其他按钮咯";
            };
            try
            {
                timer.Start();
                await Task.Run(action);
            }
            catch (Exception ex)
            {
                errorHandler?.Invoke(ex); // 自定义错误处理
                if (errorHandler == null) // 默认处理
                    MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                timer.Stop();
                this.Text = originalTitle;
            }
        }


        private async void checkImgBtn_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(checkImgBtn_Click22);
        }

        void checkImgBtn_Click22()
        {
            string inputDirectory = this.fguiProjectPath.Text; // -- @"C:\TestImg";       

            string[] imageFiles = Directory.GetFiles(inputDirectory, "*.png", SearchOption.AllDirectories); // 获取输入目录下所有图片文件    
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>(); // 字典用于存储哈希值及其对应的文件路径列表

            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Contains("\\HF\\") == false) //将某一个文件不参与查重
                {
                    string hash = GetImageHash(imageFile); // 计算图片文件的哈希值

                    // 将哈希值和文件路径添加到字典中
                    if (!hashDictionary.ContainsKey(hash))
                    {
                        hashDictionary[hash] = new List<string>();
                    }

                    hashDictionary[hash].Add(imageFile);
                }
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
                        //string path = imagePath.Replace(this.fguiPath.Text, "");
                        list.Add(imagePath);
                    }

                    ignoreList.Add(new IgnoreImg(tmp.Width * tmp.Height, list));
                    tmp.Dispose();
                }
            }

            ignoreList.Sort((a, b) => { return a.size <= b.size ? 1 : -1; });

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"已为倒序了 优先处理相同的大碎图  HF包为热更包,不参与查重 time={DateTime.Now.ToString("HH:mm:ss")}\r\n");
            var index = 0;
            foreach (var iList in ignoreList)
            {
                index++;
                sb.AppendLine($"第{index}张,面积是 {iList.size} ,路径有-->");
                foreach (var imgPath in iList.imgs)
                {
                    sb.AppendLine(imgPath);
                }

                sb.AppendLine("");
            }

            this.txtConsole.Text = sb.ToString();
        }

        private async void btn_SelfImg_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(() =>
            {
                if (CheckTextPathIsExist() == false) return;
                var serachPath = $"{this.fguiProjectPath.Text}/{this.txtNoneImg.Text}";
                SearchImg(serachPath);
            });
        }

        void SearchImg(string pSearchPath)
        {
            if (this.txt_Ignore.Text.Length <= 2)
            {
                return;
            }

            var ignoreList = this.txt_Ignore.Text.Split(";");
            string directoryPath;
            if (this.txtNoneImg.Text.Length <= 2)
            {
                directoryPath = this.fguiProjectPath.Text; //全路径
            }
            else
            {
                directoryPath = this.fguiProjectPath.Text + "/" + this.txtNoneImg.Text; // 目录路径
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

            if (string.IsNullOrEmpty(this.txtScriptJson.Text) == false)
            {
                DialogResult result = MessageBox.Show("右边的Unity工程目录,搜索文本框,有路径哦,请确认是否要搜索(*.json|*.lua|*.cs),估计搜索起来要20多秒_工程越大越耗时", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // 根据用户的选择执行相应的操作
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            SaveFguiPath();

            StringBuilder sbItem = new StringBuilder();
            sbItem.AppendLine($"已为倒序了 优先处理大的碎图 time={DateTime.Now.ToString("HH:mm:ss")}");
            sbItem.AppendLine(GetNoneUsingImg(directoryPath, pSearchPath));
            this.txtConsole.Text = sbItem.ToString();
        }

        /// <summary>         搜索文件夹中所有文件的指定文本     若无,则返回-1    </summary> 
        public int SearchTextInFiles(string searchText, string[] files)
        {
            var directoryPath = this.txtScriptJson.Text;
            foreach (var file in files)
            {
                int lineNumber = 1;
                foreach (var line in File.ReadLines(file))
                {
                    if (line.Contains(searchText))
                    {
                        return lineNumber;
                    }

                    lineNumber++;
                }
            }

            return -1;
        }

        string GetNoneUsingImg(string directoryPath, string pSearchPath)
        {
            string[] targetXmlFiles = Directory.GetFiles(directoryPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            string packagePattern = @"id=""([^""]+)""";
            string[] fntFiles = Directory.GetFiles(directoryPath, "*.fnt", SearchOption.AllDirectories); // 获取以.fnt为后缀的所有文件  艺术字体

            string[] strTxt;

            string packageId = string.Empty;
            Dictionary<string, string> idNameDic = new Dictionary<string, string>();
            Dictionary<string, string> pathPngDic = new Dictionary<string, string>();
            Dictionary<string, string> urlIdDic = new Dictionary<string, string>();
            string idPattern = @"id=""([^""]+)""";
            string namePattern = @"name=""([^""]+)""";
            string pathPattern = @"path=""([^""]+)""";
            bool checkExport = cbExtend.Checked; //是否检查导出
            foreach (string file in targetXmlFiles)
            {
                if (file.Contains("package.xml"))
                {
                    strTxt = File.ReadAllLines(file);
                    packageId = string.Empty;
                    for (int i = 0; i < strTxt.Length; i++)
                    {
                        if ((checkExport == false && strTxt[i].Contains("image id")) || //忽略 是否导出(全检)
                            (checkExport == true && strTxt[i].Contains("image id") && strTxt[i].Contains("exported="))) //仅检测  导出的
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

            string fntPattern = @"img=(\w+) xoffset"; //获取以.fnt为后缀的所有文件  艺术字体
            foreach (string file in fntFiles)
            {
                strTxt = File.ReadAllLines(file);
                for (int i = 0; i < strTxt.Length; i++)
                {
                    if (strTxt[i].Contains("img=")) //char id=43 img=uzxrck xoffset=0 yoffset=0 xadvance=0
                    {
                        string idValue = Regex.Match(strTxt[i], fntPattern).Groups[1].Value;
                        if (idNameDic.ContainsKey(idValue) == true)
                        {
                            idNameDic.Remove(idValue);
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
            string iconItemPattern = @"icon=""([^""]+)"""; //      <item icon="ui://qllwua2i9mq2w57"/>
            string currTxt_I = "";
            string[] matchXmlFiles = Directory.GetFiles(pSearchPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            foreach (string file in matchXmlFiles)
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

            bool isNeedCheckTxt = (string.IsNullOrEmpty(txtScriptJson.Text) == false);

            List<MySortPng> listSort = new List<MySortPng>();
            if (isNeedCheckTxt == false)
            {
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
            }
            else
            {
                //需去检测文本(脚本或json)
                // 获取所有 .cs 和 .json 和 .lua 文件
                var files = Directory.GetFiles(this.txtScriptJson.Text, "*.*", SearchOption.AllDirectories).Where(file => file.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".json", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".lua", StringComparison.OrdinalIgnoreCase)).ToArray();
                foreach (var item in idNameDic)
                {
                    if (File.Exists(pathPngDic[item.Key]))
                    {
                        string pngJPG_name = Path.GetFileNameWithoutExtension(item.Value);
                        var hasNum = SearchTextInFiles(pngJPG_name, files);
                        if (hasNum <= 0)
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
                }
            }

            var newlistSort = listSort.OrderByDescending(o => o.rectArea);
            StringBuilder sb = new StringBuilder();
            foreach (var item in newlistSort)
            {
                sb.AppendLine(item.outLineTxt);
            }

            return sb.ToString();
        }

        private async void btn_GlobalImg_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(() =>
            {
                if (CheckTextPathIsExist() == false) return;
                SearchImg(this.fguiProjectPath.Text);
            });
        }

        bool CheckTextPathIsExist()
        {
            if (string.IsNullOrEmpty(this.txtScriptJson.Text) == false && Directory.Exists(this.txtScriptJson.Text) == false)
            {
                MessageBox.Show("右边的搜索目录 [非真实目录]   可悬停在输入框,看其作用");
                return false;
            }

            return true;
        }

        private async void btn_ProjectImg_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(btn_ProjectImg_Click22);
        }

        void btn_ProjectImg_Click22()
        {
            if (CheckTextPathIsExist() == false) return;

            if (string.IsNullOrEmpty(this.txtScriptJson.Text) == false)
            {
                DialogResult result = MessageBox.Show("右边的搜索文本框,有路径哦,请确认是否要搜索(*.json|*.lua|*.cs),估计搜索起来要40多秒", "确认操作", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // 根据用户的选择执行相应的操作
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            var subDirectories = Directory.GetDirectories(this.fguiProjectPath.Text);

            var ignoreList = txt_Ignore.Text.Split(";");
            var lastPKG = ignoreList[ignoreList.Length - 1];

            StringBuilder sbAll = new StringBuilder();
            sbAll.AppendLine($"已为倒序了 优先处理大的碎图      另:策划配表的ItemIcon包不参与检查:{lastPKG}");
            foreach (string dir in subDirectories)
            {
                if (dir.Contains(lastPKG) == false)
                {
                    sbAll.AppendLine($"\r\n{dir.Replace(this.fguiProjectPath.Text, "")} 包下");
                    var packagePath = $"{dir}\\package.xml";
                    if (File.Exists(packagePath))
                    {
                        var sbOne = GetNoneUsingImg(dir, this.fguiProjectPath.Text);
                        sbAll.Append(sbOne);
                    }
                }
            }

            this.txtConsole.Text = sbAll.ToString();
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

        private async void btn_SelfCom_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(() => { DependentCom($"{this.fguiProjectPath.Text}\\{this.txtNoneCom.Text}"); });
        }

        private async void btn_GlobalCom_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(() => { DependentCom(this.fguiProjectPath.Text); });
        }

        bool IsDirectory(string path)
        {
            if (Directory.Exists(path) == false)
            {
                MessageBox.Show("目录不存在");
                return false;
            }

            return true;
        }

        void DependentCom(string findPath)
        {
            var comView = this.txtNoneCom.Text;
            var bigPath = this.fguiProjectPath.Text + "\\" + comView;
            if (IsDirectory(bigPath) == false) return;
            var packagePath = bigPath + "\\package.xml";

            if (File.Exists(packagePath) == false && string.IsNullOrEmpty(this.txtNoneCom.Text) == false)
            {
                MessageBox.Show("目录不存在,请检查 是否输入正确"); //填了,没填对
                return;
            }

            var sbOne = GetPackageDependTxt(packagePath, findPath);
            sbOne.Insert(0, $"有一些组件或页面没有被直接引用,程序去查下代码有无引用,若无引用,最好删除(此处[页面View]也会被输出的)\r\n因为:有些碎图被弃用的组件所引用着,只能删除了弃用的组件,查无引用的碎图才直观 time={DateTime.Now.ToString("HH:mm:ss")} \r\n");
            this.txtConsole.Text = sbOne.ToString();
        }

        private async void btn_ProjectCom_Click(object sender, EventArgs e)
        {
            await ExecuteWithTimerAsync(btn_ProjectCom_Click22);
        }

        void btn_ProjectCom_Click22()
        {
            var subDirectories = Directory.GetDirectories(this.fguiProjectPath.Text);

            StringBuilder sbAll = new StringBuilder();
            sbAll.AppendLine($"有一些组件或页面没有被直接引用,程序去查下代码有无引用,若无引用,最好删除(此处[页面View]也会被输出的)\r\n因为:有些碎图被弃用的组件所引用着,只能删除了弃用的组件,查无引用的碎图才直观 time={DateTime.Now.ToString("HH:mm:ss")}");
            foreach (string dir in subDirectories)
            {
                sbAll.AppendLine($"\r\n{dir.Replace(this.fguiProjectPath.Text, "")} 包下");
                var packagePath = $"{dir}\\package.xml";
                if (File.Exists(packagePath))
                {
                    var sbOne = GetPackageDependTxt(packagePath, this.fguiProjectPath.Text);
                    sbAll.Append(sbOne);
                }
            }

            this.txtConsole.Text = sbAll.ToString();
        }

        StringBuilder GetPackageDependTxt(string packagePath, string findPath)
        {
            SaveFguiPath();
            var strTxt = File.ReadAllLines(packagePath);

            bool checkExport = cbExtend.Checked; //是否检查导出
            Dictionary<string, string> idNameDic = new Dictionary<string, string>();
            string idPattern = @"id=""([^""]+)""";
            string namePattern = @"name=""([^""]+)""";
            for (int i = 0; i < strTxt.Length; i++)
            {
                if ((checkExport == false && strTxt[i].Contains("component id")) || //忽略 是否导出(全检)
                    (checkExport == true && strTxt[i].Contains("component id") && strTxt[i].Contains("exported="))) //仅检测  导出的
                {
                    string idValue = Regex.Match(strTxt[i], idPattern).Groups[1].Value;
                    string nameValue = Regex.Match(strTxt[i], namePattern).Groups[1].Value;
                    idNameDic[idValue] = nameValue;
                }
            }

            var bigXML = Directory.GetFiles(findPath, "*.xml", SearchOption.AllDirectories); // 获取以.xml为后缀的所有文件
            foreach (var item in bigXML)
            {
                if (item.Contains("package.xml") == false)
                {
                    var strTxt2 = File.ReadAllLines(item);
                    for (int st = 0; st < strTxt2.Length; st++)
                    {
                        for (int iN = 0; iN < idNameDic.Count; iN++)
                        {
                            (string key, string value) = idNameDic.ElementAt(iN);
                            if (strTxt2[st].Contains(key))
                            {
                                idNameDic[key] = string.Empty;
                            }
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in idNameDic)
            {
                if (string.IsNullOrEmpty(item.Value) == false)
                {
                    sb.AppendLine(item.Value);
                }
            }

            return sb;
        }

        private void txtNoneCom_TextChanged(object sender, EventArgs e)
        {
            this.btn_ProjectCom.Visible = (this.txtNoneCom.Text.Length <= 0);
            this.btn_GlobalCom.Visible = (this.txtNoneCom.Text.Length > 0);
            this.btn_SelfCom.Visible = (this.txtNoneCom.Text.Length > 0);
        }

        private void txtNoneImg_TextChanged(object sender, EventArgs e)
        {
            this.btn_ProjectImg.Visible = (this.txtNoneImg.Text.Length <= 0);
            this.btn_GlobalImg.Visible = (this.txtNoneImg.Text.Length > 0);
            this.btn_SelfImg.Visible = (this.txtNoneImg.Text.Length > 0);
        }
    }

    public class MySortPng
    {
        public string outLineTxt { get; set; }
        public int rectArea { get; set; }
    }
}