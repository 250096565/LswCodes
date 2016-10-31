namespace SvnDemo
{
    partial class SvnMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkOut = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.showLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.servierUrl = new System.Windows.Forms.TextBox();
            this.localPath = new System.Windows.Forms.TextBox();
            this.submit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkOut
            // 
            this.checkOut.Location = new System.Drawing.Point(31, 274);
            this.checkOut.Name = "checkOut";
            this.checkOut.Size = new System.Drawing.Size(75, 23);
            this.checkOut.TabIndex = 3;
            this.checkOut.Text = "检出";
            this.checkOut.UseVisualStyleBackColor = true;
            this.checkOut.Click += new System.EventHandler(this.checkOut_Click);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(188, 274);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(75, 23);
            this.update.TabIndex = 4;
            this.update.Text = "更新";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // showLog
            // 
            this.showLog.Location = new System.Drawing.Point(468, 274);
            this.showLog.Name = "showLog";
            this.showLog.Size = new System.Drawing.Size(75, 23);
            this.showLog.TabIndex = 6;
            this.showLog.Text = "查看日志";
            this.showLog.UseVisualStyleBackColor = true;
            this.showLog.Click += new System.EventHandler(this.showLog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器文件夹路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "本地文件夹路径";
            // 
            // servierUrl
            // 
            this.servierUrl.Location = new System.Drawing.Point(188, 53);
            this.servierUrl.Name = "servierUrl";
            this.servierUrl.Size = new System.Drawing.Size(276, 21);
            this.servierUrl.TabIndex = 1;
            // 
            // localPath
            // 
            this.localPath.Location = new System.Drawing.Point(188, 174);
            this.localPath.Name = "localPath";
            this.localPath.Size = new System.Drawing.Size(276, 21);
            this.localPath.TabIndex = 2;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(327, 274);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 5;
            this.submit.Text = "提交";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // SvnMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 353);
            this.Controls.Add(this.localPath);
            this.Controls.Add(this.servierUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.showLog);
            this.Controls.Add(this.update);
            this.Controls.Add(this.checkOut);
            this.Name = "SvnMain";
            this.Text = "SvnMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SvnMain_FormClosing);
            this.Load += new System.EventHandler(this.SvnMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button checkOut;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button showLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox servierUrl;
        private System.Windows.Forms.TextBox localPath;
        private System.Windows.Forms.Button submit;
    }
}