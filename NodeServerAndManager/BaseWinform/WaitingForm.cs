﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace KangYiCollection.BaseWinform
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
            
        }
        public void SetText(string str)
        {
            label1.Text = str;
        }

    }
}
