using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using svnOper;
using SharpSvn;

namespace SvnDemo
{
    public partial class SvnMain : Form
    {
        public SvnMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 检出操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(servierUrl.Text))
            {
                MessageBox.Show("请输入服务器路径");
                return;
            }
            if (string.IsNullOrEmpty(localPath.Text))
            {
                MessageBox.Show("请输入本地路径");
                return;
            }
            MessageBox.Show("检出中.....时间可能较长");
            if (SvnManager.CheckOut(servierUrl.Text, localPath.Text))
            {
                MessageBox.Show("检出成功");
                return;
            }
            MessageBox.Show(operSVN.lastErrMsg);
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(localPath.Text))
            {
                MessageBox.Show("请输入本地路径");
                return;
            }

            if (SvnManager.Update(localPath.Text))
            {
                MessageBox.Show("更新成功");
                return;
            }
            MessageBox.Show(operSVN.lastErrMsg);
        }

        /// <summary>
        /// 查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showLog_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(localPath.Text))
            {
                MessageBox.Show("请输入本地路径");
                return;
            }
            showLog dlg = new showLog();
            if (!operSVN.serchLog(localPath.Text, dlg))
            {
                MessageBox.Show(operSVN.lastErrMsg);
                return;
            }
            dlg.ShowDialog();
        }
        /// <summary>
        /// 提交操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(localPath.Text))
            {
                MessageBox.Show("请输入本地路径");
                return;
            }
            if (SvnManager.Commit(localPath.Text))
            {
                MessageBox.Show("提交成功!");
                return;
            }
            MessageBox.Show(operSVN.lastErrMsg);
        }

        private void SvnMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            new Form1().Show();
        }

        private void SvnMain_Load(object sender, EventArgs e)
        {

        }
    }
}
