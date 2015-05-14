using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServerForm.Control
{
    public enum IPType : byte { A, B, C, D, E };  
    public partial class IpControl : UserControl
    {
        public IpControl()
        {
            InitializeComponent();
        }
        private void IpControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            char KeyChar = e.KeyChar;
            int TextLength = ((TextBox)sender).TextLength;

            if (KeyChar == '.' || KeyChar == '。' || KeyChar == ' ')//按“.”“。”或空格进入下一文本框
            {
                if ((((TextBox)sender).SelectedText.Length == 0) && (TextLength > 0) && (((TextBox)sender) != textBox4))
                {   // 进入下一个文本框
                    SendKeys.Send("{Tab}");
                }
                e.Handled = true;
            }

            if (Regex.Match(KeyChar.ToString(), "[0-9]").Success)
            {
                //当文本框内文本长度为2，且文本框内的文本没有选中
                if (TextLength == 2 && ((TextBox)sender).SelectedText.Length==0)
                {
                    if (int.Parse(((TextBox)sender).Text + e.KeyChar.ToString()) > 255)
                    {
                        e.Handled = true;
                    }
                }
                else if (TextLength == 0)
                {
                    if (KeyChar == '0')
                    {
                        e.Handled = true;
                    }
                }
            }
            else
            {   // 回删操作
                if (KeyChar == '\b')
                {
                    if (TextLength == 0)
                    {
                        if (((TextBox)sender) != textBox1)
                        {   // 回退到上一个文本框 Shift+Tab
                            SendKeys.Send("+{TAB}{END}");
                        }
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// string类型的IP地址
        /// </summary>
        override public string Text
        {
            get
            {
                return this.Value.ToString();
            }
            set
            {
                IPAddress address;
                if (IPAddress.TryParse(value, out address))
                {
                    byte[] bytes = address.GetAddressBytes();
                    textBox1.Text = bytes[0].ToString("D");
                    textBox2.Text = bytes[1].ToString("D");
                    textBox3.Text = bytes[2].ToString("D");
                    textBox4.Text = bytes[3].ToString("D");
                }
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPAddress Value
        {
            get
            {
                IPAddress address;
                string ipString = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text;

                if (IPAddress.TryParse(ipString, out address))
                {
                    return address;
                }
                else
                {
                    return new IPAddress(0);
                }
            }
            set
            {
                byte[] bytes = value.GetAddressBytes();
                textBox1.Text = bytes[0].ToString("D");
                textBox2.Text = bytes[1].ToString("D");
                textBox3.Text = bytes[2].ToString("D");
                textBox4.Text = bytes[3].ToString("D");
            }
        }

        /// <summary>
        /// IP地址分类
        /// </summary>
        public IPType Type
        {
            get
            {
                byte[] bytes = this.Value.GetAddressBytes();
                int FirstByte = bytes[0];

                if (FirstByte < 128)
                {
                    return IPType.A;
                }
                else if (FirstByte < 192)
                {
                    return IPType.B;
                }
                else if (FirstByte < 224)
                {
                    return IPType.C;
                }
                else if (FirstByte < 240)
                {
                    return IPType.D;
                }
                else
                {
                    return IPType.E;    // 保留做研究用
                }
            }
        }

        /// <summary>
        /// 控件的边框样式
        /// </summary>
        new public BorderStyle BorderStyle
        {
            get
            {
                return this.textBox1.BorderStyle;
            }
            set
            {
                textBox1.BorderStyle = BorderStyle.Fixed3D;
                textBox2.BorderStyle = BorderStyle.Fixed3D;
                textBox3.BorderStyle = BorderStyle.Fixed3D;
                textBox4.BorderStyle = BorderStyle.Fixed3D;
                //foreach (var item in this.Controls)
                //{
                //    TextBox textBox;
                //    if(item is TextBox)
                //    {
                //        textBox = item as TextBox;
                //        textBox.BorderStyle = value;
                //    }
                //}
            }
        }
    }
}
