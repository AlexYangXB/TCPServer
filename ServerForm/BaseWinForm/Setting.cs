using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ServerForm.BaseWinForm
{
    public partial class Setting : Form
    {

        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            string LocalIp = ServerForm.Properties.Settings.Default.LocalIp;
            if (LocalIp == "")
            {
                LocalIp = GetLocalIP();
            }
            ipControl_LocalIp.Text = LocalIp;
            txb_Port.Text = ServerForm.Properties.Settings.Default.Port.ToString();
        }
        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIP() //获取本地IP
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            for (int i = 0; i < ipHost.AddressList.Length; i++)
            {
                if (ipHost.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddr = ipHost.AddressList[i];
                }
            }
            //IPAddress ipAddr = ipHost.AddressList[0];
            return ipAddr.ToString();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            ServerForm.Properties.Settings.Default.LocalIp = ipControl_LocalIp.Text;
            ServerForm.Properties.Settings.Default.Port = int.Parse(txb_Port.Text);
            ServerForm.Properties.Settings.Default.Save();
            Close();
        }

        private void txb_Port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;  //不为数字就移除
            }
        }
    }
}
