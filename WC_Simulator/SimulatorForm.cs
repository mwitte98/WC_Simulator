using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace WC_Simulator
{
    public partial class SimulatorForm : Form
    {
        int numSimulations = 1000000;
        string[] teams; // team names
        int[] numLinesPerTeam; // number of lines for each team
        List<List<double[]>> teamInfo = new List<List<double[]>>(); // ai, exp, minutes of each line for each team
        List<Game> gamesPlayed = new List<Game>(); // games already played
        List<string> mustWinList = new List<string>(); // if a team must win a matchup or either team can win
        int[,] places;
        RunSimulator simulator;
        ProgressForm progressForm;

        public SimulatorForm()
        {
            InitializeComponent();
            numTeamsDropdown.Text = "2";
            tabControl1.TabPages.Remove(getLineInfoTab);
            tabControl1.TabPages.Remove(gamesPlayedTab);
            tabControl1.TabPages.Remove(resultsTab);
        }

        /***** CLICK EVENTS *****/

        private void getTeamsContinueButton_Click(object sender, EventArgs e)
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

            teams = new string[numTeams];
            numLinesPerTeam = new int[numTeams];

            // setup labels and textboxes on next tab only after verifying all data
            // filled in because user has option to change data after error message
            // note: lines 1-3 of teams 1 and 2 have visible set to true by default,
            // all other lines have visible set to false by default
            int numLinesTeam1 = int.Parse(numLinesDropdown1.Text);
            int numLinesTeam2 = int.Parse(numLinesDropdown2.Text);
            teams[0] = getTeamTextbox1.Text;
            teams[1] = getTeamTextbox2.Text;
            numLinesPerTeam[0] = numLinesTeam1;
            numLinesPerTeam[1] = numLinesTeam2;
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
                teams[2] = getTeamTextbox3.Text;
                teams[3] = getTeamTextbox4.Text;
                numLinesPerTeam[2] = numLinesTeam3;
                numLinesPerTeam[3] = numLinesTeam4;
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
                teams[4] = getTeamTextbox5.Text;
                teams[5] = getTeamTextbox6.Text;
                numLinesPerTeam[4] = numLinesTeam5;
                numLinesPerTeam[5] = numLinesTeam6;
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
            this.AcceptButton = getLineContinueButton;
        }

        private void getLineContinueButton_Click(object sender, EventArgs e)
        {
            List<Panel> getLinePanels = getLineInfoTab.Controls.OfType<Panel>().ToList<Panel>();

            // store ai, exp, and min for easy access later and
            // verify minutes for each team add up to 60
            for (int teamNumber = 0; teamNumber < numLinesPerTeam.Length; teamNumber++)
            {
                List<double[]> teamInfo = new List<double[]>();
                int teamMinutes = 0;

                for (int lineNumber = 0; lineNumber < numLinesPerTeam[teamNumber]; lineNumber++)
                {
                    Panel linePanel = getLinePanels[(teamNumber * 4) + lineNumber];
                    List<NumericUpDown> updowns = linePanel.Controls.OfType<NumericUpDown>().ToList<NumericUpDown>();
                    int lineAI = int.Parse(updowns[0].Value.ToString());
                    int lineExp = int.Parse(updowns[1].Value.ToString());
                    int lineMin = int.Parse(updowns[2].Value.ToString());
                    teamMinutes += lineMin;
                    teamInfo.Add(new double[] { lineAI, lineExp, lineMin });
                }

                if (teamMinutes != 60)
                {
                    MessageBox.Show("Line minutes for " + teams[teamNumber] + " add up to " + teamMinutes + ", but should equal 60",
                                    teams[teamNumber] + " - Invalid Minutes",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    this.teamInfo = new List<List<double[]>>();
                    return;
                }

                this.teamInfo.Add(teamInfo);
            }

            // if 2 teams selected, show results
            // otherwise, allow for games already played to be input
            if (teams.Length == 2)
            {
                tabControl1.TabPages.Add(resultsTab);
                tabControl1.TabPages.Remove(getLineInfoTab);
                this.AcceptButton = rerunSimsButton;
                editGamesPlayedButton.Visible = false;
                if (!simBackgroundWorker.IsBusy)
                {
                    progressForm = new ProgressForm();
                    progressForm.Show();
                    simBackgroundWorker.RunWorkerAsync();
                }
            }
            else
            {
                tabControl1.TabPages.Add(gamesPlayedTab);
                tabControl1.TabPages.Remove(getLineInfoTab);
                gamesPlayedTeam1Goals1.Select();
                this.AcceptButton = gamesPlayedContinueButton;

                List<Panel> gamesPlayedPanels = gamesPlayedTab.Controls.OfType<Panel>().ToList<Panel>();
                int matchupNum = 0;
                for (int team1 = 0; team1 < teams.Length; team1++)
                {
                    for (int team2 = team1 + 1; team2 < teams.Length; team2++)
                    {
                        Panel panel = gamesPlayedPanels[matchupNum];
                        panel.Visible = true;
                        List<Label> labels = panel.Controls.OfType<Label>().ToList<Label>();
                        labels[0].Text = teams[team1];
                        labels[1].Text = teams[team2];
                        ComboBox dropdown = panel.Controls.OfType<ComboBox>().ToList<ComboBox>()[0];
                        dropdown.Items.Add("Neither");
                        dropdown.Items.Add(teams[team1]);
                        dropdown.Items.Add(teams[team2]);
                        dropdown.Text = "Neither";
                        matchupNum++;
                    }
                }
            }
        }

        private void gamesPlayedContinueButton_Click(object sender, EventArgs e)
        {
            gamesPlayed = new List<Game>();
            mustWinList = new List<string>();
            List<Panel> gamesPlayedPanels = gamesPlayedTab.Controls.OfType<Panel>().ToList<Panel>();
            int matchupNum = 0;

            // if any games played are entered, verify goals are not equal and add to list.
            // add the text of each must win dropdown to a list for use during simulation calculation
            for (int team1 = 0; team1 < teams.Length; team1++)
            {
                for (int team2 = team1 + 1; team2 < teams.Length; team2++)
                {
                    Panel panel = gamesPlayedPanels[matchupNum];
                    List<NumericUpDown> updowns = panel.Controls.OfType<NumericUpDown>().ToList<NumericUpDown>();
                    int goals1 = int.Parse(updowns[0].Value.ToString());
                    int goals2 = int.Parse(updowns[1].Value.ToString());
                    if (goals1 != 0 || goals2 != 0)
                    {
                        if (goals1 == goals2)
                        {
                            MessageBox.Show(teams[team1] + " v. " + teams[team2] + " - The number of goals cannot be equal for both teams",
                                        "Invalid Number of Goals",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                            gamesPlayed = new List<Game>();
                            mustWinList = new List<string>();
                            return;
                        }
                        else
                            gamesPlayed.Add(new Game(team1, team2, goals1, goals2));
                    }
                    mustWinList.Add(panel.Controls.OfType<ComboBox>().ToList<ComboBox>()[0].Text);
                    matchupNum++;
                }
            }

            // move to next tab and run simulations, popping up a progress bar
            // indicating how far along the simulation calculation is
            tabControl1.TabPages.Add(resultsTab);
            tabControl1.TabPages.Remove(gamesPlayedTab);
            this.AcceptButton = rerunSimsButton;
            if (!simBackgroundWorker.IsBusy)
            {
                progressForm = new ProgressForm();
                progressForm.Show();
                simBackgroundWorker.RunWorkerAsync();
            }
        }

        private void rerunSimsButton_Click(object sender, EventArgs e)
        {
            // rerun the simulation calculation using all of the same data
            if (!simBackgroundWorker.IsBusy)
            {
                progressForm = new ProgressForm();
                progressForm.Show();
                simBackgroundWorker.RunWorkerAsync();
            }
        }

        private void editGamesPlayedButton_Click(object sender, EventArgs e)
        {
            // move back to games played tab to allow user to make changes
            tabControl1.TabPages.Add(gamesPlayedTab);
            tabControl1.TabPages.Remove(resultsTab);
            gamesPlayedTeam1Goals1.Select();
            this.AcceptButton = gamesPlayedContinueButton;
        }

        /***** OTHER EVENTS *****/

        private void numTeamsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show/hide team textbox and number of lines dropdown
            // based on text in number of teams dropdown
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
            // when a textbox is selected, highlight all text
            TextBox textbox = sender as TextBox;
            textbox.SelectAll();
        }

        private void updown_Enter(object sender, EventArgs e)
        {
            // when a numeric updown is selected, highlight the number
            NumericUpDown updown = sender as NumericUpDown;
            updown.Select(0, updown.Value.ToString().Length);
        }

        private void simBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // run the simulation calculation using a background worker
            BackgroundWorker worker = sender as BackgroundWorker;
            Simulate(worker);
        }

        private void simBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // update the progress bar value and message
            progressForm.Message = "Simulation progress: " + e.ProgressPercentage.ToString() + "%";
            progressForm.ProgressValue = e.ProgressPercentage;
        }

        private void simBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // when the simulation calculation is complete, update the results in the results tab
            if (teams.Length == 2)
            {
                double percent = (places[0, 0] * 100.0) / numSimulations;
                resultsLabel1.Text = teams[0] + " wins " + places[0, 0] + " of " + numSimulations + " times (" + (places[0, 0] * 100.0 / numSimulations) + "%)";
                resultsLabel2.Text = teams[1] + " wins " + places[1, 0] + " of " + numSimulations + " times (" + (places[1, 0] * 100.0 / numSimulations) + "%)";
            }
            else
            {
                int labelNum = 0;
                for (int i = 0; i < teams.Length; i++)
                {
                    resultsLabelPanel.Controls[labelNum].Text = teams[i];
                    labelNum++;
                    for (int place = 0; place < teams.Length; place++)
                    {
                        double placeFinishes = places[i, place];
                        double percent = placeFinishes * 100.0 / numSimulations;
                        resultsLabelPanel.Controls[labelNum].Text = "\tPlace " + (place + 1) + " - " + placeFinishes + " of " + numSimulations + " times (" + percent + "%)";
                        labelNum++;
                    }
                    // if 4 teams, after two teams start adding results to 2nd column
                    if (i == 1 && teams.Length == 4)
                        labelNum = 21;
                }
            }
            progressForm.Close();
        }

        /***** HELPERS *****/

        private Boolean verifyTeamDataNotEmpty(TextBox textbox, ComboBox dropdown, int teamNumber)
        {
            // verify that the team name and number of lines are not empty
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

        private void Simulate(BackgroundWorker worker)
        {
            // runs the simulation calculation method based on the number of teams chosen
            simulator = new RunSimulator(worker, teams, numLinesPerTeam, teamInfo, mustWinList);
            if (teams.Length == 2)
                places = simulator.SimulateTwoTeams();
            else
                places = simulator.SimulateGroup(gamesPlayed);
        }
    }
}
