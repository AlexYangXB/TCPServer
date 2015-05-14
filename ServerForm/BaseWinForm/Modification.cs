using System;
using System.Windows.Forms;

namespace ServerForm.BaseWinForm
{
    public partial class Modification : Form
    {
        private string _machineType = "";
        private string _ipAddress = "";
        private string[] _ipList;
        private int _index=0;
        public delegate void SendMessage(object sender, string MachineType,string IpAddress);
        public event SendMessage MessageSent;
        public Modification()
        {
            InitializeComponent();
        }
        public Modification(string MachineType,string IpAddress,string[] IpList,int Index)
        {
            InitializeComponent();
            _machineType = MachineType;
            _ipAddress = IpAddress;
            _ipList = IpList;
            _index = Index;
        }
        

        private void Modification_Load(object sender, EventArgs e)
        {
            if(_machineType!="")
            {
                cmb_MachineType.Text = _machineType;
            }
            if(_ipAddress!="")
            {
                //txb_IpAddress.Text = _ipAddress;
                ipControl_IP.Text = _ipAddress;
            }
        
        }

        private void btn_Modified_Click(object sender, EventArgs e)
        {
            if(cmb_MachineType.Text=="")
            {
                MessageBox.Show("请设置机具类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(ipControl_IP.Text=="0.0.0.0")
            {
                MessageBox.Show("请设置IP地址", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < _ipList.Length;i++ )
            {
                if (ipControl_IP.Text == _ipList[i] && i != _index)
                {
                    string message = string.Format("列表中已经存在该IP地址:" + ipControl_IP.Text + "，请重新设置。");
                    MessageBox.Show(message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (this.MessageSent != null)
                MessageSent(this, cmb_MachineType.Text, ipControl_IP.Text);
            this.Close();
        }
    }
}
