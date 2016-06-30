using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP服务器.Properties;

namespace TCP服务器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtIP.Text)|| string.IsNullOrEmpty(txtPort.Text))
            {
                MessageBox.Show(Resources.Form1_btnStart_Click_请输入IP地址或端口号);
            }
        }
    }
}
