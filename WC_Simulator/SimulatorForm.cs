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
        List<string> teams = new List<string>(); // team names
        List<int> numLines = new List<int>(); // number of lines for each team
        List<List<List<int>>> teamInfo = new List<List<List<int>>>(); // ai, exp, minutes of each line for each team

        public SimulatorForm()
        {
            InitializeComponent();
            numTeamsDropdown.Text = "2";
            tabControl1.TabPages.Remove(getLineInfoTab);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            // verify each team name and number of lines selected
            int numTeams = int.Parse(numTeamsDropdown.Text);
            if (verifyTeamDataNotEmpty(getTeamTextbox1, numLinesDropdown1, 1))
                return;
            if (verifyTeamDataNotEmpty(getTeamTextbox2, numLinesDropdown2, 2))
                return;
            if (numTeams >= 4)
            {
                if (verifyTeamDataNotEmpty(getTeamTextbox3, numLinesDropdown3, 3))
                    return;
                if (verifyTeamDataNotEmpty(getTeamTextbox4, numLinesDropdown4, 4))
                    return;
            }
            if (numTeams == 6)
            {
                if (verifyTeamDataNotEmpty(getTeamTextbox5, numLinesDropdown5, 5))
                    return;
                if (verifyTeamDataNotEmpty(getTeamTextbox6, numLinesDropdown6, 6))
                    return;
            }

            // setup labels and textboxes on next tab only after verifying all data
            // filled in because user has option to change data after error message
            // note: lines 1-3 of teams 1 and 2 have visible set to true by default,
            // all other lines have visible set to false by default
            int numLinesTeam1 = int.Parse(numLinesDropdown1.Text);
            int numLinesTeam2 = int.Parse(numLinesDropdown2.Text);
            teams.Add(getTeamTextbox1.Text);
            teams.Add(getTeamTextbox2.Text);
            numLines.Add(numLinesTeam1);
            numLines.Add(numLinesTeam2);
            getLineTeamLabel1.Text = getTeamTextbox1.Text;
            getLineTeamLabel2.Text = getTeamTextbox2.Text;
            if (numLinesTeam1 == 4)
                getLinePanel14.Visible = true;
            if (numLinesTeam2 == 4)
                getLinePanel24.Visible = true;
            if (numTeams >= 4)
            {
                int numLinesTeam3 = int.Parse(numLinesDropdown3.Text);
                int numLinesTeam4 = int.Parse(numLinesDropdown4.Text);
                teams.Add(getTeamTextbox3.Text);
                teams.Add(getTeamTextbox4.Text);
                numLines.Add(numLinesTeam3);
                numLines.Add(numLinesTeam4);
                getLineTeamLabel3.Text = getTeamTextbox3.Text;
                getLineTeamLabel4.Text = getTeamTextbox4.Text;
                getLinePanel31.Visible = getLinePanel32.Visible = getLinePanel33.Visible = true;
                getLinePanel41.Visible = getLinePanel42.Visible = getLinePanel43.Visible = true;
                if (numLinesTeam3 == 4)
                    getLinePanel34.Visible = true;
                if (numLinesTeam4 == 4)
                    getLinePanel44.Visible = true;
            }
            if (numTeams == 6)
            {
                int numLinesTeam5 = int.Parse(numLinesDropdown5.Text);
                int numLinesTeam6 = int.Parse(numLinesDropdown6.Text);
                teams.Add(getTeamTextbox5.Text);
                teams.Add(getTeamTextbox6.Text);
                numLines.Add(numLinesTeam5);
                numLines.Add(numLinesTeam6);
                getLineTeamLabel5.Text = getTeamTextbox5.Text;
                getLineTeamLabel6.Text = getTeamTextbox6.Text;
                getLinePanel51.Visible = getLinePanel52.Visible = getLinePanel53.Visible = true;
                getLinePanel61.Visible = getLinePanel62.Visible = getLinePanel63.Visible = true;
                if (numLinesTeam5 == 4)
                    getLinePanel54.Visible = true;
                if (numLinesTeam6 == 4)
                    getLinePanel64.Visible = true;
            }

            // all names and lines filled in so move to next tab
            tabControl1.TabPages.Add(getLineInfoTab);
            tabControl1.TabPages.Remove(getTeamsTab);
            aiUpDown11.Select();
        }

        private void numTeamsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numTeams = int.Parse(numTeamsDropdown.Text);
            if (numTeams == 2)
                getTeamPanel3.Visible = getTeamPanel4.Visible = getTeamPanel5.Visible = getTeamPanel6.Visible = false;
            else if (numTeams == 4)
            {
                getTeamPanel3.Visible = getTeamPanel4.Visible = true;
                getTeamPanel5.Visible = getTeamPanel6.Visible = false;
            }
            else
                getTeamPanel3.Visible = getTeamPanel4.Visible = getTeamPanel5.Visible = getTeamPanel6.Visible = true;
            getTeamTextbox1.Select();
        }

        private void textbox_Enter(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.SelectAll();
        }

        private void updown_Enter(object sender, EventArgs e)
        {
            NumericUpDown updown = sender as NumericUpDown;
            updown.Select(0, updown.Value.ToString().Length);
        }

        private Boolean verifyTeamDataNotEmpty(TextBox textbox, ComboBox dropdown, int teamNumber)
        {
            if (string.IsNullOrWhiteSpace(textbox.Text) || (string.IsNullOrEmpty(dropdown.Text)))
            {
                MessageBox.Show("Team " + teamNumber + " is missing team name or number of lines",
                                "Team " + teamNumber + " Invalid",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return true;
            }
            return false;
        }
    }
}
