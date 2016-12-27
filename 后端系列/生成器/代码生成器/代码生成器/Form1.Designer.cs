namespace 代码生成器
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            this.TxtConnection = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnViewList = new System.Windows.Forms.Button();
            this.BtnBuild = new System.Windows.Forms.Button();
            this.ViewList = new System.Windows.Forms.ListView();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtConnection
            // 
            this.TxtConnection.Location = new System.Drawing.Point(123, 12);
            this.TxtConnection.Name = "TxtConnection";
            this.TxtConnection.Size = new System.Drawing.Size(425, 21);
            this.TxtConnection.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "连接字符串";
            // 
            // BtnViewList
            // 
            this.BtnViewList.Location = new System.Drawing.Point(24, 410);
            this.BtnViewList.Name = "BtnViewList";
            this.BtnViewList.Size = new System.Drawing.Size(75, 23);
            this.BtnViewList.TabIndex = 3;
            this.BtnViewList.Text = "显示列表";
            this.BtnViewList.UseVisualStyleBackColor = true;
            this.BtnViewList.Click += new System.EventHandler(this.BtnViewList_Click);
            // 
            // BtnBuild
            // 
            this.BtnBuild.Location = new System.Drawing.Point(123, 410);
            this.BtnBuild.Name = "BtnBuild";
            this.BtnBuild.Size = new System.Drawing.Size(75, 23);
            this.BtnBuild.TabIndex = 3;
            this.BtnBuild.Text = "生成";
            this.BtnBuild.UseVisualStyleBackColor = true;
            this.BtnBuild.Click += new System.EventHandler(this.BtnBuild_Click);
            // 
            // ViewList
            // 
            this.ViewList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.ViewList.Location = new System.Drawing.Point(24, 96);
            this.ViewList.Name = "ViewList";
            this.ViewList.Size = new System.Drawing.Size(700, 282);
            this.ViewList.TabIndex = 4;
            this.ViewList.UseCompatibleStateImageBehavior = false;
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(24, 55);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(143, 21);
            this.txtTableName.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(194, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 454);
            this.Controls.Add(this.ViewList);
            this.Controls.Add(this.BtnBuild);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.BtnViewList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.TxtConnection);
            this.Name = "Form1";
            this.Text = "代码生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnViewList;
        private System.Windows.Forms.Button BtnBuild;
        private System.Windows.Forms.ListView ViewList;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Button btnSearch;
    }
}

