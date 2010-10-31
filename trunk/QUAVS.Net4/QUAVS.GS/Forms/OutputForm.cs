using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using WeifenLuo.WinFormsUI.Docking;
using QUAVS.Base;
using QUAVS.Log;

namespace QUAVS.GS.Forms
{
    public partial class OutputForm : DockContent
    {

        public MessageQueueTraceListener listener = new MessageQueueTraceListener();

        public OutputForm()
        {
            InitializeComponent();
        }

        private void OutputForm_Load(object sender, EventArgs e)
        {
            Trace.Listeners.Add(listener);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tmp = listener.Text;

            if (tmp.Length > 0)
            {
                dataGridView.Rows.Insert(0,DateTime.Now.ToString(), tmp);
            }
        }
    }
}
