using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDP网络通信.Properties;

namespace UDP网络通信
{
    public partial class Form1 : Form
    {

        //发送客户端
        private UdpClient sendUdpClient;

        //接收客户端
        private UdpClient ReceiveUdpClient;
        public Form1()
        {
            InitializeComponent();

            //拿到指定主机的IP地址
            IPAddress[] ips = Dns.GetHostAddresses("");

            foreach (var ip in ips.Where(ip => ip.ToString().Contains("192")))
            {
                txtLocalIP.Text = ip.ToString();
                txtSendIP.Text = ip.ToString();
                break;
            }

            int port = 9020;
            int sendPort = 9030;
            txtLocalPort.Text = port.ToString();
            txtSendPort.Text = sendPort.ToString();

        }

        /// <summary>
        /// 接收消息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceive_Click(object sender, EventArgs e)
        {
            ReceiveUdpClient?.Close();
            //创建接收套接字
            IPAddress localIp = IPAddress.Parse(txtLocalIP.Text);

            IPEndPoint localIpEndPoint = new IPEndPoint(localIp, int.Parse(txtLocalPort.Text));

            try
            {
                ReceiveUdpClient = new UdpClient(localIpEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ReceiveMessage();
            // task = Task.Factory.StartNew(ReceiveMessage);
            //task.Wait();

        }


        /// <summary>
        /// 接收消息方法
        /// </summary>
        private  void ReceiveMessage()
        {
            if (ReceiveUdpClient == null)
                return;
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {

                    //关闭receiveUdpClient时会产生异常
                    byte[] receiveBytes = ReceiveUdpClient.Receive(ref remoteIpEndPoint);

                    string message = Encoding.Unicode.GetString(receiveBytes);

                    //显示消息内容
                    ShowMessageForView(txtConent, $"{remoteIpEndPoint}{message}");
                }
                catch
                {
                    break;
                }
            }
        }

        delegate void ShowMessageForViewCallBack(TextBox textbox, string text);


        /// <summary>
        /// 显示消息 
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="text"></param>
        private void ShowMessageForView(TextBox txt, string text)
        {
            if (txt.InvokeRequired)
            {
                ShowMessageForViewCallBack back = ShowMessageForView;

                txt.Invoke(back, new object[] { txt, text });
            }
            else
            {
                txt.Text = DateTime.Now + Environment.NewLine + Resources.Form1_ShowMessageForView___ + text;
            }
        }

        //发送消息按钮
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text))
            {
                MessageBox.Show(Resources.Form1_btnSend_Click_消息不可以为空);
                return;
            }

            IPAddress localIp = IPAddress.Parse(txtLocalIP.Text);
            IPEndPoint localIpEndPoint = new IPEndPoint(localIp, int.Parse(txtLocalPort.Text));

            sendUdpClient = new UdpClient(localIpEndPoint);

            Thread task = new Thread(SendMessage);
            task.Start(txtMessage.Text);
        }


        /// <summary>
        /// 发送消息方法
        /// </summary>
        /// <param name="obj"></param>
        private void SendMessage(object obj)
        {
            string message = (string)obj;

            byte[] sendBytes = Encoding.Unicode.GetBytes(message);
            IPAddress remoteIp = IPAddress.Parse(txtSendIP.Text);
            IPEndPoint remoteIpEndPoint = new IPEndPoint(remoteIp, int.Parse(txtSendPort.Text));
            sendUdpClient.Send(sendBytes, sendBytes.Length, remoteIpEndPoint);

            sendUdpClient.Close();

            //请空消息
            ResetMessage(txtMessage);
        }


        delegate void ResetMessageCallBack(TextBox text);

        private void ResetMessage(TextBox txt)
        {
            //InvokeRequired属性代表如果处理空间与调用线程是在不同的线程上创建的
            if (txt.InvokeRequired)
            {
                ResetMessageCallBack back = ResetMessage;
                txt.Invoke(back, new object[] { txt });
            }
            else
            {
                txt.Clear();
                txt.Focus();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ReceiveUdpClient?.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtConent.Clear();
        }
    }
}
