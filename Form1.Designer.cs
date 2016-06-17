namespace project1_FileMerge
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fbdSourceFileChoose = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenObjectFile = new System.Windows.Forms.Button();
            this.btnOpenSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFilePath = new System.Windows.Forms.TextBox();
            this.txtObjectFolderPath = new System.Windows.Forms.TextBox();
            this.fswSouceFolderWatcher = new System.IO.FileSystemWatcher();
            this.btnStartMerge = new System.Windows.Forms.Button();
            this.ssrStateStrip = new System.Windows.Forms.StatusStrip();
            this.tscStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fswSouceFolderWatcher)).BeginInit();
            this.ssrStateStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "原始文件夹路径";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "目标文件夹路径";
            // 
            // btnOpenObjectFile
            // 
            this.btnOpenObjectFile.Location = new System.Drawing.Point(352, 89);
            this.btnOpenObjectFile.Name = "btnOpenObjectFile";
            this.btnOpenObjectFile.Size = new System.Drawing.Size(75, 22);
            this.btnOpenObjectFile.TabIndex = 1;
            this.btnOpenObjectFile.Text = "浏览";
            this.btnOpenObjectFile.UseVisualStyleBackColor = true;
            this.btnOpenObjectFile.Click += new System.EventHandler(this.btnOpenObjectFile_Click);
            // 
            // btnOpenSourceFile
            // 
            this.btnOpenSourceFile.Location = new System.Drawing.Point(352, 34);
            this.btnOpenSourceFile.Name = "btnOpenSourceFile";
            this.btnOpenSourceFile.Size = new System.Drawing.Size(75, 21);
            this.btnOpenSourceFile.TabIndex = 0;
            this.btnOpenSourceFile.Text = "浏览";
            this.btnOpenSourceFile.UseVisualStyleBackColor = true;
            this.btnOpenSourceFile.Click += new System.EventHandler(this.btnOpenSourceFile_Click);
            // 
            // txtSourceFilePath
            // 
            this.txtSourceFilePath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtSourceFilePath.Location = new System.Drawing.Point(12, 35);
            this.txtSourceFilePath.Name = "txtSourceFilePath";
            this.txtSourceFilePath.ReadOnly = true;
            this.txtSourceFilePath.Size = new System.Drawing.Size(334, 21);
            this.txtSourceFilePath.TabIndex = 6;
            // 
            // txtObjectFolderPath
            // 
            this.txtObjectFolderPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtObjectFolderPath.Location = new System.Drawing.Point(14, 89);
            this.txtObjectFolderPath.Name = "txtObjectFolderPath";
            this.txtObjectFolderPath.ReadOnly = true;
            this.txtObjectFolderPath.Size = new System.Drawing.Size(332, 21);
            this.txtObjectFolderPath.TabIndex = 7;
            // 
            // fswSouceFolderWatcher
            // 
            this.fswSouceFolderWatcher.EnableRaisingEvents = true;
            this.fswSouceFolderWatcher.SynchronizingObject = this;
            this.fswSouceFolderWatcher.Changed += new System.IO.FileSystemEventHandler(this.fswSourceFolderWatcher_Changed);
            this.fswSouceFolderWatcher.Created += new System.IO.FileSystemEventHandler(this.fswSourceFolderWatcher_Created);
            // 
            // btnStartMerge
            // 
            this.btnStartMerge.Location = new System.Drawing.Point(138, 125);
            this.btnStartMerge.Name = "btnStartMerge";
            this.btnStartMerge.Size = new System.Drawing.Size(152, 33);
            this.btnStartMerge.TabIndex = 5;
            this.btnStartMerge.Text = "开始自动合并";
            this.btnStartMerge.UseVisualStyleBackColor = true;
            this.btnStartMerge.Click += new System.EventHandler(this.btnStartMerge_Click);
            // 
            // ssrStateStrip
            // 
            this.ssrStateStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscStateLabel});
            this.ssrStateStrip.Location = new System.Drawing.Point(0, 168);
            this.ssrStateStrip.Name = "ssrStateStrip";
            this.ssrStateStrip.Size = new System.Drawing.Size(439, 22);
            this.ssrStateStrip.TabIndex = 13;
            // 
            // tscStateLabel
            // 
            this.tscStateLabel.Name = "tscStateLabel";
            this.tscStateLabel.Size = new System.Drawing.Size(32, 17);
            this.tscStateLabel.Text = "停止";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 142);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "开机自启动";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 190);
            this.Controls.Add(this.ssrStateStrip);
            this.Controls.Add(this.txtObjectFolderPath);
            this.Controls.Add(this.btnStartMerge);
            this.Controls.Add(this.txtSourceFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenObjectFile);
            this.Controls.Add(this.btnOpenSourceFile);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DFD/DFX文件合并";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.fswSouceFolderWatcher)).EndInit();
            this.ssrStateStrip.ResumeLayout(false);
            this.ssrStateStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdSourceFileChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenObjectFile;
        private System.Windows.Forms.Button btnOpenSourceFile;
        private System.Windows.Forms.TextBox txtSourceFilePath;
        private System.Windows.Forms.TextBox txtObjectFolderPath;
        private System.IO.FileSystemWatcher fswSouceFolderWatcher;
        private System.Windows.Forms.Button btnStartMerge;
        private System.Windows.Forms.StatusStrip ssrStateStrip;
        private System.Windows.Forms.ToolStripStatusLabel tscStateLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

