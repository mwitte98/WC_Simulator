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
    public partial class SimulatorForm : Form
    {
        string[] teams = new string[6]; // team names
        int[] numLines = new int[6]; // number of lines for each team
        int[, ,] teamInfo = new int[6, 4, 3]; // ai, exp, minutes of each line for each team

        public SimulatorForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(getLineInfoTab);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < int.Parse(numTeamsDropdown.Text); i++)
            {
                // verify name and number of lines not empty
                TextBox textbox = getTeamsTextboxPanel.Controls[i] as TextBox;
                ComboBox dropdown = numLinesDropdownPanel.Controls[i] as ComboBox;
                if (string.IsNullOrWhiteSpace(textbox.Text) || (string.IsNullOrEmpty(numTeamsDropdown.Text)))
                {
                    MessageBox.Show("Team " + (i+1) + " missing", "Invalid Team Name",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(dropdown.Text))
                {
                    MessageBox.Show("Number of lines is missing for team " + (i + 1), "Number of Lines Missing",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // setup labels and textboxes on next tab
            for (int i = 0; i < int.Parse(numTeamsDropdown.Text); i++)
            {
                TextBox textbox = getTeamsTextboxPanel.Controls[i] as TextBox;
                ComboBox dropdown = numLinesDropdownPanel.Controls[i] as ComboBox;
                teams[i] = textbox.Text;
                numLines[i] = int.Parse(dropdown.Text);
                Label label = getLinesTeamLabelPanel.Controls[i] as Label;
                label.Text = teams[i];
                label.Visible = true;
                for (int a = 0; a < numLines[i]; a++)
                {
                    Label lineLabel = linesLabelPanel.Controls[(i * 4) + a] as Label;
                    Label aiLabel = aiLabelPanel.Controls[(i * 4) + a] as Label;
                    NumericUpDown aiUpDown = aiUpDownPanel.Controls[(i * 4) + a] as NumericUpDown;
                    Label expLabel = expLabelPanel.Controls[(i * 4) + a] as Label;
                    NumericUpDown expUpDown = expUpDownPanel.Controls[(i * 4) + a] as NumericUpDown;
                    Label minutesLabel = minutesLabelPanel.Controls[(i * 4) + a] as Label;
                    NumericUpDown minutesUpDown = minutesUpDownPanel.Controls[(i * 4) + a] as NumericUpDown;
                    lineLabel.Visible = true;
                    aiLabel.Visible = true;
                    aiUpDown.Visible = true;
                    expLabel.Visible = true;
                    expUpDown.Visible = true;
                    minutesLabel.Visible = true;
                    minutesUpDown.Visible = true;
                }
            }

            // all names and lines filled in so move to next tab
            tabControl1.TabPages.Add(getLineInfoTab);
            tabControl1.TabPages.Remove(getTeamsTab);
        }

        private void numTeamsChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                Label teamLabel = getTeamsLabelPanel.Controls[i] as Label;
                TextBox textbox = getTeamsTextboxPanel.Controls[i] as TextBox;
                Label linesLabel = numLinesLabelPanel.Controls[i] as Label;
                ComboBox dropdown = numLinesDropdownPanel.Controls[i] as ComboBox;
                teamLabel.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
                textbox.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
                textbox.TabIndex = (i * 2) + 1;
                linesLabel.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
                dropdown.Visible = (i < int.Parse(numTeamsDropdown.Text)) ? true : false;
                dropdown.TabIndex = (i * 2) + 2;
            }
            getTeamTextbox1.Select();
        }

        private void textboxSelectAll(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.SelectAll();
        }

        private void updownSelectAll(object sender, EventArgs e)
        {
            NumericUpDown updown = sender as NumericUpDown;
            updown.Select(0, updown.Value.ToString().Length);
        }
    }
}
