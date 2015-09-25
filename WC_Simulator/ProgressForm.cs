using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WC_Simulator
{
    public partial class ProgressForm : Form
    {
        public string Message
        {
            get { return progressLabel.Text; }
            set { progressLabel.Text = value; }
        }

        public int ProgressValue
        {
            get { return progressBar1.Value; }
            set { progressBar1.Value = value; }
        }

        public ProgressForm()
        {
            InitializeComponent();
        }
    }
}
