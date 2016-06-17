using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Microsoft.Win32;

using System.Text.RegularExpressions;

namespace project1_FileMerge
{
    public partial class Form1 : Form
    {
 
        //选文件夹和目标文件夹的目录
        //string SourceFolderPath, ObjectFolderPath;
        string SourceFolderPath = null, ObjectFolderPath = null;
        //是否选定了源文件夹和目标文件夹的标志
        bool SourceFolderChooseFlag = false, ObjectFolderChooseFlag = false;

        bool StartAutoMergeFlag = false;
        //bool AutoStartFlag = false ; 利用checkBox1.Checked标志来进行

        public Form1()
        {
            InitializeComponent();
            
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStartMerge.Enabled = false;
            //checkBox1.Checked = false;  //默认开机自动启动

            this.fswSouceFolderWatcher.EnableRaisingEvents = false;
            this.fswSouceFolderWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;

            timer1.Enabled = false;

            //捕获配置文件不存在的错误
            try
            {
                // string str = System.Environment.CurrentDirectory;
                string str = @"C:";
                str = str + @"\" + "record" + ".ini";
                //StreamReader TheReadIniFile = new StreamReader(str, Encoding.GetEncoding("gb2312"));
                //string TempSourceFolderPath = TheReadIniFile.ReadLine();
                string[] line = File.ReadAllLines(str, System.Text.Encoding.Default);
                //string[] line = File.ReadAllLines(str, Encoding.GetEncoding("gb2312"));
                string TempSourceFolderPath = line[0];
                string TempObjectFolderPath = line[1];

                if (line[2] == "True")
                    checkBox1.Checked = true;
                else if (line[2] == "False")
                    checkBox1.Checked = false;

                txtSourceFilePath.Text = TempSourceFolderPath;
                txtObjectFolderPath.Text = TempObjectFolderPath;

                //得到保存的源文件夹和目标文件夹后，进行一系列操作
                SourceFolderPath = TempSourceFolderPath;
                ObjectFolderPath = TempObjectFolderPath;


                //捕获路径文件不存在的错误
                try
                {
                    fswSouceFolderWatcher.Path = SourceFolderPath;

                    //改变标志位
                    if (SourceFolderPath != null)
                    {
                        SourceFolderChooseFlag = true;
                    }
                    if (ObjectFolderPath != null)
                    {
                        ObjectFolderChooseFlag = true;
                    }

                    //如果标志位都发生了改变，改变一些状态
                    if (SourceFolderChooseFlag == true && ObjectFolderChooseFlag == true)
                    {
                        btnStartMerge.Text = "停止自动合并";
                        tscStateLabel.Text = "自动合并已开启";

                        btnStartMerge.Enabled = true;
                        btnOpenSourceFile.Enabled = false;
                        btnOpenObjectFile.Enabled = false;


                        this.fswSouceFolderWatcher.EnableRaisingEvents = true;
                        StartAutoMergeFlag = true;
                        //开机先自动合并一次
                        FileMerge();

                        timer1.Enabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show("文件夹不存在,请点击确定后重新选择", "提示");
                }
            }
            //捕获后什么也不做
            catch 
            {
                //第一次打开会捕获到这个错误
               //MessageBox.Show("配置文件不存在，请重新设置", "提示");   
            }
            /*catch (System.ArgumentException)
            {
                MessageBox.Show("System.ArgumentException", "提示");
            }
            catch (System.IO.PathTooLongException)
            {
                MessageBox.Show("System.IO.PathTooLongException", "提示");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("System.IO.IOException", "提示");
            }
            catch (System.Security.SecurityException)
            {
                MessageBox.Show("System.Security.SecurityException", "提示");
            }
             */
            //暂时不使用自动开机功能
            //checkBox1.Visible = true;
        }

        //得到源文件夹的路径
        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            //选定的源文件夹和目标文件夹
            DialogResult SourceFolder;

            SourceFolder = fbdSourceFileChoose.ShowDialog();

            if (SourceFolder == System.Windows.Forms.DialogResult.OK)
            {
                txtSourceFilePath.Text = "";
                //用来显示文件夹的路径             
                SourceFolderPath = fbdSourceFileChoose.SelectedPath;
                txtSourceFilePath.AppendText(SourceFolderPath);
                SourceFolderChooseFlag = true;

                fswSouceFolderWatcher.Path = SourceFolderPath;
            }

            if (SourceFolderChooseFlag == true && ObjectFolderChooseFlag == true)
            {
                btnStartMerge.Enabled = true;
                //timer1.Enabled = true;       //选择完成只有点击按钮才能开始
            }
        }

        //得到输出文件夹的路径
        private void btnOpenObjectFile_Click(object sender, EventArgs e)
        {
            
            DialogResult ObjectFolder;

            ObjectFolder = fbdSourceFileChoose.ShowDialog();

            if (ObjectFolder == System.Windows.Forms.DialogResult.OK)
            {
                txtObjectFolderPath.Text = "";

                //显示目标文件夹路径
                ObjectFolderPath = fbdSourceFileChoose.SelectedPath;
                txtObjectFolderPath.AppendText(ObjectFolderPath);
                ObjectFolderChooseFlag = true;
            }

 
            if (SourceFolderChooseFlag == true && ObjectFolderChooseFlag == true)
            {
                btnStartMerge.Enabled = true;
                //timer1.Enabled = true;  //选择完成只有点击按钮才能开始
            }

        }

        private void FileMerge()
        {

            string strSourceDFDFileName = null;                       //存放源文件夹中DFD文件的路径
            List<string> ListSourceDFXFileName = new List<string>();  //定义一个链表存放源文件DFX路径和名称

            //因为文件夹下面只有一层子文件，所以考虑不使用递归遍历
            //从源文件夹的路径实例化一个文件夹
            DirectoryInfo TheSourceFloder = new DirectoryInfo(SourceFolderPath);
            bool SourceDFDFileExistFlag;

            //遍历源文件夹下的所有子文件夹
            foreach (DirectoryInfo dir in TheSourceFloder.GetDirectories())
            {
                SourceDFDFileExistFlag = false;
                ListSourceDFXFileName.Clear();

                //在目标文件夹下新建文件
                if (dir.Name == "新建文件夹")
                {
                    continue;
                }

                FileStream fs = new FileStream(ObjectFolderPath + @"\" + dir.Name + ".DFQ", FileMode.Create, FileAccess.Write);
                
                   
                //实例化源文件夹中的每一个子文件夹
                DirectoryInfo TheSubFolder = new DirectoryInfo(SourceFolderPath + @"\" + dir.Name);

                
                //实例化一个对象用于写文件
                StreamWriter TheWriteDFQFile = new StreamWriter(fs, System.Text.Encoding.Default);

                //得到某个源子文件夹中的DFX文件
                foreach (FileInfo SourceFileName in TheSubFolder.GetFiles("*.DFX"))
                {
                    //得到文件的路径
                    ListSourceDFXFileName.Add(SourceFolderPath + @"\" + dir.Name + @"\" + SourceFileName.ToString());
                }

                strSourceDFDFileName = null; 
                //如果还没有拿到DFD文件的路径，去找到；如果拿到了，不再寻找
                if (SourceDFDFileExistFlag == false)
                {
                    foreach (FileInfo SourceFileName in TheSubFolder.GetFiles("*.DFD"))
                    {
                        //得到文件的路径
                        strSourceDFDFileName = SourceFolderPath + @"\" + dir.Name + @"\" + SourceFileName.ToString();
                        SourceDFDFileExistFlag = true;
                    }

                    //先写入DFD文件    
                    if (strSourceDFDFileName != null)
                    {
                        //原来是gb2312
                        StreamReader TheReadDFDFile = new StreamReader(strSourceDFDFileName, System.Text.Encoding.Default);
                        string TheDFDFileTxt = TheReadDFDFile.ReadToEnd();
                        TheReadDFDFile.Close();

                        TheWriteDFQFile.Write(TheDFDFileTxt);
                    }

                }


                //遍历链表中的每一个DFX文件
                foreach (string SourceFileName in ListSourceDFXFileName)
                {
                    //实例化一个对象用于读取文件
                    StreamReader TheReadDFXFile = new StreamReader(SourceFileName, System.Text.Encoding.Default);
                    string TheDFXFileTxt = TheReadDFXFile.ReadToEnd();
                    TheReadDFXFile.Close();

                    //写入新文件
                    TheWriteDFQFile.Write(TheDFXFileTxt);
                }

                TheWriteDFQFile.Flush();  //为了保护硬盘？
                TheWriteDFQFile.Close();
                fs.Close();

                //List<string> lstr = new List<string>();
                //File.ReadAllLines(ObjectFileName, Encoding.GetEncoding("gb2312")).ToList<string>().ForEach(str =>
                //{
                //    if (string.Empty.CompareTo(str.Trim()) != 0)
                //        lstr.Add(str);
                //});
                //File.WriteAllLines(ObjectFileName, lstr.ToArray<string>());

                
                //原来是UTF-8
                //目标文件路径及名称
                string ObjectFileName = ObjectFolderPath + @"\" + dir.Name + ".DFQ";
                string tempStr = File.ReadAllText(ObjectFileName, System.Text.Encoding.Default);//读取文档
                //tempStr = Regex.Replace(tempStr, @"(\s)+", "\r\n\r\n");//替换为两个个换行,可以自己输入
                //tempStr = Regex.Replace(tempStr, "\r\n\r\n", "");//替换为两个换行,可以自己输入
                for (int i = 0; i <= 50; i++)
                {
                    tempStr = Regex.Replace(tempStr, "\r\n\r\n", "\r\n");//替换为两个换行,可以自己输入
                }
                File.WriteAllText(ObjectFileName, tempStr, System.Text.Encoding.Default);//写入文档 
                
            }//结束文件夹的遍历
        }


        private void btnStartMerge_Click(object sender, EventArgs e)
        {
            if (SourceFolderPath != ObjectFolderPath)
            {
                if (StartAutoMergeFlag == true)
                {
                    StartAutoMergeFlag = false;
                }
                else if (StartAutoMergeFlag == false)
                {
                    StartAutoMergeFlag = true;
                }

                //开始自动合并
                if (StartAutoMergeFlag == true)
                {


                    btnStartMerge.Text = "停止自动合并";
                    tscStateLabel.Text = "自动合并已开启";
                    btnOpenSourceFile.Enabled = false;
                    btnOpenObjectFile.Enabled = false;
                    timer1.Enabled = true;

                    try
                    {
                        this.fswSouceFolderWatcher.EnableRaisingEvents = true;
                        FileMerge();
                    }
                    catch
                    {
                        MessageBox.Show("文件夹不存在,请点击确定后重新选择", "提示");
                    }
                }

                //停止自动合并
                else if (StartAutoMergeFlag == false)
                {
                    this.fswSouceFolderWatcher.EnableRaisingEvents = false;

                    btnStartMerge.Text = "开始自动合并";
                    tscStateLabel.Text = "停止";

                    timer1.Enabled = false;
                    btnOpenSourceFile.Enabled = true;
                    btnOpenObjectFile.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("目标文件夹和源文件夹相同,请重新选择", "提示");
            }
        }

        //子文件夹下有文件时触发
        private void fswSourceFolderWatcher_Changed(object sender, FileSystemEventArgs e)
        {
#if false
            //txtTestBox.AppendText("触发了事件");

            tscStateLabel.Text = "正在自动合并文件...";
            
            //一种不太好的解决办法,一旦有改变就重新合并文件
            FileMerge();   
         

            tscStateLabel.Text = "自动合并已开启";
#endif     
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("您确定要退出吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            //确认关闭
            if (result == DialogResult.OK)
            {
                if (SourceFolderPath != null && ObjectFolderPath != null && SourceFolderPath != ObjectFolderPath)
                {
                    //string str = System.Environment.CurrentDirectory;
                    string str = @"C:";
                    //在目标文件夹下新建文件
                    FileStream fs = new FileStream(str + @"\" + "record" + ".ini", FileMode.Create, FileAccess.Write);
                    

                    StreamWriter RecordFile = new StreamWriter(fs, System.Text.Encoding.Default);
                    RecordFile.WriteLine(SourceFolderPath);
                    RecordFile.WriteLine(ObjectFolderPath);
                    RecordFile.WriteLine(checkBox1.Checked);  //写入最终是否选中的标志
                    //RecordFile.WriteLine(System.Environment.CurrentDirectory);
                    RecordFile.Flush();  //为了保护硬盘？
                    RecordFile.Close();

                    fs.Close();
                }

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            if (checkBox1.Checked) //设置开机自启动  
            {
                try
                {
                    //MessageBox.Show("设置开机自启动，需要修改注册表", "提示");
                    string path = Application.ExecutablePath;

                    //修改两个注册表选项
                    RegistryKey rk = Registry.LocalMachine;
                    //该路径为一个启动项的位置
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.SetValue("JcShutdown", path);
                    rk2.Close();
                    rk.Close();
                }
                catch 
                {
                    MessageBox.Show("注册表拒绝访问", "提示");
                }
            }
            else //取消开机自启动  
            {
                try
                {
                    //MessageBox.Show("取消开机自启动，需要修改注册表", "提示");
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.DeleteValue("JcShutdown", false);
                    rk2.Close();
                    rk.Close();
                }
                catch 
                {
                    MessageBox.Show("注册表拒绝访问", "提示");
                }
            }
        }

        private void fswSourceFolderWatcher_Created(object sender, FileSystemEventArgs e)
        {
//这里什么也不做
#if false

            tscStateLabel.Text = "正在自动合并文件...";

            //一种不太好的解决办法,一旦有改变就重新合并文件
            FileMerge();


            tscStateLabel.Text = "自动合并已开启";
#endif
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)//最小化　　　　　 
            {
                this.Hide();
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtTestBox.AppendText("触发了事件");

            tscStateLabel.Text = "正在自动合并文件...";

            //一种不太好的解决办法,一旦有改变就重新合并文件
            FileMerge();


            tscStateLabel.Text = "自动合并已开启";
        }

 
    }
}
