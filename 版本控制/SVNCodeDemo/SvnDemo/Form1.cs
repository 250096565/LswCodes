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
namespace SvnDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        //登录
        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(userName.Text))
            {
                MessageBox.Show("请输入Svn账号");
                return;
            }
            if (string.IsNullOrEmpty(pwd.Text))
            {
                MessageBox.Show("请输入Svn密码");
                return;
            }
            if (SvnManager.Initialize(userName.Text, pwd.Text))
            {
                this.Hide();
                new SvnMain().Show();
            }
            else
            {
                MessageBox.Show(operSVN.lastErrMsg);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void pwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//如果输入的是回车键  
            {
                this.btnLogin_Click(sender, e);//触发button事件  
            }
        }
    }
}
