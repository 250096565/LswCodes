namespace OSSWinForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.startNum = new System.Windows.Forms.TextBox();
            this.endNum = new System.Windows.Forms.TextBox();
            this.localUrl = new System.Windows.Forms.TextBox();
            this.serviceUrl = new System.Windows.Forms.TextBox();
            this.Upload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SelectedSite = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.selectedPath = new System.Windows.Forms.Button();
            this.annotation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UploadStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "开始版本";
            // 
            // startNum
            // 
            this.startNum.Location = new System.Drawing.Point(26, 202);
            this.startNum.Name = "startNum";
            this.startNum.Size = new System.Drawing.Size(35, 21);
            this.startNum.TabIndex = 2;
            // 
            // endNum
            // 
            this.endNum.Location = new System.Drawing.Point(135, 202);
            this.endNum.Name = "endNum";
            this.endNum.Size = new System.Drawing.Size(35, 21);
            this.endNum.TabIndex = 3;
            // 
            // localUrl
            // 
            this.localUrl.Location = new System.Drawing.Point(116, 83);
            this.localUrl.Name = "localUrl";
            this.localUrl.Size = new System.Drawing.Size(259, 21);
            this.localUrl.TabIndex = 0;
            this.localUrl.TextChanged += new System.EventHandler(this.localUrl_TextChanged);
            // 
            // serviceUrl
            // 
            this.serviceUrl.Location = new System.Drawing.Point(116, 143);
            this.serviceUrl.Name = "serviceUrl";
            this.serviceUrl.Size = new System.Drawing.Size(259, 21);
            this.serviceUrl.TabIndex = 1;
            // 
            // Upload
            // 
            this.Upload.Location = new System.Drawing.Point(284, 230);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(75, 23);
            this.Upload.TabIndex = 4;
            this.Upload.Text = "上传";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "结束版本";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "服务器路径";
            // 
            // SelectedSite
            // 
            this.SelectedSite.FormattingEnabled = true;
            this.SelectedSite.Location = new System.Drawing.Point(116, 43);
            this.SelectedSite.Name = "SelectedSite";
            this.SelectedSite.Size = new System.Drawing.Size(121, 20);
            this.SelectedSite.TabIndex = 5;
            this.SelectedSite.SelectionChangeCommitted += new System.EventHandler(this.SelectedSite_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "选择网站";
            // 
            // selectedPath
            // 
            this.selectedPath.Location = new System.Drawing.Point(20, 83);
            this.selectedPath.Name = "selectedPath";
            this.selectedPath.Size = new System.Drawing.Size(63, 23);
            this.selectedPath.TabIndex = 6;
            this.selectedPath.Text = "选择路径 ";
            this.selectedPath.UseVisualStyleBackColor = true;
            this.selectedPath.Click += new System.EventHandler(this.selectedPath_Click);
            // 
            // annotation
            // 
            this.annotation.AutoSize = true;
            this.annotation.Location = new System.Drawing.Point(18, 267);
            this.annotation.Name = "annotation";
            this.annotation.Size = new System.Drawing.Size(0, 12);
            this.annotation.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "本地项目路径请选择到具体的网站，如:D:/SAYDProjects/DP";
            // 
            // UploadStatus
            // 
            this.UploadStatus.AutoSize = true;
            this.UploadStatus.Location = new System.Drawing.Point(18, 309);
            this.UploadStatus.Name = "UploadStatus";
            this.UploadStatus.Size = new System.Drawing.Size(0, 12);
            this.UploadStatus.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(482, 330);
            this.Controls.Add(this.UploadStatus);
            this.Controls.Add(this.annotation);
            this.Controls.Add(this.selectedPath);
            this.Controls.Add(this.SelectedSite);
            this.Controls.Add(this.Upload);
            this.Controls.Add(this.endNum);
            this.Controls.Add(this.serviceUrl);
            this.Controls.Add(this.localUrl);
            this.Controls.Add(this.startNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Name = "Form1";
            this.Text = "OSS静态文件管理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox startNum;
        private System.Windows.Forms.TextBox endNum;
        private System.Windows.Forms.TextBox localUrl;
        private System.Windows.Forms.TextBox serviceUrl;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SelectedSite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button selectedPath;
        private System.Windows.Forms.Label annotation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UploadStatus;
    }
}

