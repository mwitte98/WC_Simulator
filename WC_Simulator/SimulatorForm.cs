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
            numTeamsDropdown.Text = "2";
            tabControl1.TabPages.Remove(getLineInfoTab);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            // verify each team name and number of lines selected
            if (string.IsNullOrWhiteSpace(getTeamTextbox1.Text) || (string.IsNullOrEmpty(numLinesDropdown1.Text)))
            {
                MessageBox.Show("Team 1 is missing team name or number of lines", "Team 1 Invalid",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(getTeamTextbox2.Text) || (string.IsNullOrEmpty(numLinesDropdown2.Text)))
            {
                MessageBox.Show("Team 2 is missing team name or number of lines", "Team 2 Invalid",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(numTeamsDropdown.Text) >= 4)
            {
                if (string.IsNullOrWhiteSpace(getTeamTextbox3.Text) || (string.IsNullOrEmpty(numLinesDropdown3.Text)))
                {
                    MessageBox.Show("Team 3 is missing team name or number of lines", "Team 3 Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(getTeamTextbox4.Text) || (string.IsNullOrEmpty(numLinesDropdown4.Text)))
                {
                    MessageBox.Show("Team 4 is missing team name or number of lines", "Team 4 Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (int.Parse(numTeamsDropdown.Text) == 6)
            {
                if (string.IsNullOrWhiteSpace(getTeamTextbox5.Text) || (string.IsNullOrEmpty(numLinesDropdown5.Text)))
                {
                    MessageBox.Show("Team 5 is missing team name or number of lines", "Team 5 Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(getTeamTextbox6.Text) || (string.IsNullOrEmpty(numLinesDropdown6.Text)))
                {
                    MessageBox.Show("Team 6 is missing team name or number of lines", "Team 6 Invalid",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // setup labels and textboxes on next tab only after verifying all data
            // filled in because user has option to change data after error message
            // note: lines 1-3 of teams 1 and 2 have visible set to true by default,
            // all other lines have visible set to false by default
            if (int.Parse(numLinesDropdown1.Text) == 4)
            {
                getLineTeamLabel1.Text = getTeamTextbox1.Text;
                getLinePanel14.Visible = true;
            }
            if (int.Parse(numLinesDropdown2.Text) == 4)
            {
                getLineTeamLabel2.Text = getTeamTextbox2.Text;
                getLinePanel24.Visible = true;
            }
            if (int.Parse(numTeamsDropdown.Text) >= 4)
            {
                getLineTeamLabel3.Text = getTeamTextbox3.Text;
                getLinePanel31.Visible = true;
                getLinePanel32.Visible = true;
                getLinePanel33.Visible = true;
                getLineTeamLabel4.Text = getTeamTextbox4.Text;
                getLinePanel41.Visible = true;
                getLinePanel42.Visible = true;
                getLinePanel43.Visible = true;
                if (int.Parse(numLinesDropdown3.Text) == 4)
                {
                    getLineTeamLabel3.Text = getTeamTextbox3.Text;
                    getLinePanel34.Visible = true;
                }
                if (int.Parse(numLinesDropdown4.Text) == 4)
                {
                    getLineTeamLabel4.Text = getTeamTextbox4.Text;
                    getLinePanel44.Visible = true;
                }
            }
            if (int.Parse(numTeamsDropdown.Text) == 6)
            {
                getLineTeamLabel5.Text = getTeamTextbox5.Text;
                getLinePanel51.Visible = true;
                getLinePanel52.Visible = true;
                getLinePanel53.Visible = true;
                getLineTeamLabel6.Text = getTeamTextbox6.Text;
                getLinePanel61.Visible = true;
                getLinePanel62.Visible = true;
                getLinePanel63.Visible = true;
                if (int.Parse(numLinesDropdown5.Text) == 4)
                {
                    getLineTeamLabel5.Text = getTeamTextbox5.Text;
                    getLinePanel54.Visible = true;
                }
                if (int.Parse(numLinesDropdown6.Text) == 4)
                {
                    getLineTeamLabel6.Text = getTeamTextbox6.Text;
                    getLinePanel64.Visible = true;
                }
            }

            // all names and lines filled in so move to next tab
            tabControl1.TabPages.Add(getLineInfoTab);
            tabControl1.TabPages.Remove(getTeamsTab);
            aiUpDown11.Select();
        }

        private void numTeamsChanged(object sender, EventArgs e)
        {
            if (int.Parse(numTeamsDropdown.Text) == 2)
            {
                getTeamPanel3.Visible = false;
                getTeamPanel4.Visible = false;
                getTeamPanel5.Visible = false;
                getTeamPanel6.Visible = false;
            }
            else if (int.Parse(numTeamsDropdown.Text) == 4)
            {
                getTeamPanel3.Visible = true;
                getTeamPanel4.Visible = true;
                getTeamPanel5.Visible = false;
                getTeamPanel6.Visible = false;
            }
            else
            {
                getTeamPanel3.Visible = true;
                getTeamPanel4.Visible = true;
                getTeamPanel5.Visible = true;
                getTeamPanel6.Visible = true;
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

        private void leave(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            ComboBox dropdown = sender as ComboBox;
        }
    }
}
