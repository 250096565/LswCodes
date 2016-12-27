using OSSWinForm.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSSWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            //调用Manager进行初始化
            ConfigurationManager.AppSettings.Get("svnUser");
            string userName = ConfigurationManager.AppSettings["svnUser"];
            string pwd = ConfigurationManager.AppSettings["svnPwd"];
            if (!Manager.Initialize(userName, pwd))
            {
                MessageBox.Show("初始化失败,请检查配置文件中的svn账号");
            }
            string site = SelectedSite.SelectedValue.ToString();
            if (site == "-1")
            {
                MessageBox.Show("请选择网站");
                return;
            }
            UploadStatus.Text = "上传中.....";
            OutputModel output = Manager.GetStaticFile(startNum.Text, endNum.Text, localUrl.Text, serviceUrl.Text);

            if (output.Status != "1")
            {
                MessageBox.Show(output.Message);
                UploadStatus.Text = "上传失败";
                return;
            }
            UploadStatus.Text = "上传成功";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //在应用初始化时,向下拉框中添加网站名称
            //如果需要添加网站与资源地址,请到AppConfig中添加
            IDictionary sites = (IDictionary)ConfigurationManager.GetSection("sites");

            List<SiteModel> list = new List<SiteModel>();
            list.Add(new SiteModel { Text = "请选择网站", Value = "-1" });

            foreach (DictionaryEntry temp in sites)
            {
                list.Add(new SiteModel() { Text = temp.Value.ToString(), Value = temp.Key.ToString() });
            }

            SelectedSite.DataSource = list;
            SelectedSite.DisplayMember = "Text";
            SelectedSite.ValueMember = "Value";

        }

        private void SelectedSite_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string value = SelectedSite.SelectedValue.ToString();

            if (value != "-1")
            {
                if (value.Contains("DianPing"))
                {
                    serviceUrl.Text = "statics/" + value;
                }
                else
                {
                    serviceUrl.Text = "statics/" + value;
                }
            }
        }

        private void selectedPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "请选择路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                localUrl.Text = dialog.SelectedPath;
            }
        }

        private void SetVersionInfo()
        {
            if (!string.IsNullOrEmpty(localUrl.Text))
            {
                OutputModel output = Manager.GetLofInfo(localUrl.Text);
                if (output.Status != "1")
                {
                    MessageBox.Show(output.Message);
                    return;
                }
                List<string> result = (List<string>)output.Data;
                if (result.Count > 0)
                {
                    startNum.Text = result[1];
                    endNum.Text = result[1];
                    annotation.Text = result[0];
                }
                else
                {
                    startNum.Text = "";
                    endNum.Text = "";
                    annotation.Text = "";
                }
            }
        }

        private void localUrl_TextChanged(object sender, EventArgs e)
        {
            SetVersionInfo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
