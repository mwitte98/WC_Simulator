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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numTeamsDropdown.Items.AddRange(new string[] { "2", "4", "6" });
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            
        }

        private void numTeamsChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Label label = labelPanel.Controls[i] as Label;
                TextBox textbox = textboxPanel.Controls[i] as TextBox;
                if (label != null)
                    label.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
                if (textbox != null)
                    textbox.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
            }
            team1Textbox.Select();
        }

        private void selectAll(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
                textbox.SelectAll();
        }
    }
}
