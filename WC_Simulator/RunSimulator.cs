using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WC_Simulator
{
    class RunSimulator
    {
        private int numSimulations = 1000000;
        private string[] teams; // team names
        private int[] numLinesPerTeam; // number of lines for each team
        private List<List<double[]>> teamInfo = new List<List<double[]>>(); // ai, exp, minutes of each line for each team
        BackgroundWorker worker;

        public RunSimulator(string[] teams, int[] numLinesPerTeam, List<List<double[]>> teamInfo, BackgroundWorker worker)
        {
            this.teams = teams;
            this.numLinesPerTeam = numLinesPerTeam;
            this.teamInfo = teamInfo;
            this.worker = worker;
        }

        public int[,] SimulateTwoTeams()
        {
            // Initialize team total based on AI, half exp, and minutes of each line
            double[] aiExpTotals = new double[teams.Length];
            for (int i = 0; i < teams.Length; i++)
            {
                aiExpTotals[i] = 0;
                foreach (double[] line in teamInfo[i])
                {
                    aiExpTotals[i] += line[0] * (line[2] / 60); // adds AI of line based on min
                    aiExpTotals[i] += line[1] * (line[2] / 60) * 0.5; // adds exp of line based on min
                }
            }

            // calculate average goals for each team based on ai/exp
            double avgGoals = 2.61;
            double difference1 = aiExpTotals[0] - aiExpTotals[1];
            double difference2 = aiExpTotals[1] - aiExpTotals[0];
            double avgGoals1;
            double avgGoals2;
            if (Math.Abs(difference1) > 100)
            {
                avgGoals1 = avgGoals + Math.Pow((difference1 / 100), 3);
                avgGoals2 = avgGoals + Math.Pow((difference2 / 100), 3);
            }
            else
            {
                avgGoals1 = avgGoals + (difference1 / 100);
                avgGoals2 = avgGoals + (difference2 / 100);
            }

            Random rand = new Random();
            int[,] wins = new int[2, 1];

            for (int simNum = 0; simNum < numSimulations; simNum++)
            {
                int progressPercent = 0;
                if (simNum % 10000 == 0)
                {
                    progressPercent = (simNum * 100) / numSimulations;
                    worker.ReportProgress(progressPercent);
                }
                // simulate a game between each team
                int gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                int gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);
                while (gaussian1 == gaussian2)
                {
                    gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                    gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);
                }

                if (gaussian1 > gaussian2)
                    wins[0, 0] += 1;
                else
                    wins[1, 0] += 1;
            }

            return wins;
        }

        public int[,] SimulateGroup(List<Game> gamesPlayed)
        {
            double[] aiExpTotals = new double[teams.Length];
            int[] teamNumbers = new int[teams.Length];

            // Initialize team total based on AI, half exp, and minutes of each line
            for (int i = 0; i < teams.Length; i++)
            {
                teamNumbers[i] = i;
                aiExpTotals[i] = 0;
                foreach (double[] line in teamInfo[i])
                {
                    aiExpTotals[i] += line[0] * (line[2] / 60); // adds AI of line based on min
                    aiExpTotals[i] += line[1] * (line[2] / 60) * 0.5; // adds exp of line based on min
                }
            }

            List<Matchup> matchups = new List<Matchup>();
            double avgGoals = 2.61;
            for (int team1 = 0; team1 < teams.Length; team1++)
            {
                for (int team2 = team1 + 1; team2 < teams.Length; team2++)
                {
                    if (!gamesPlayed.Exists(game => game.team1 == team1 && game.team2 == team2))
                    {
                        // calculate average goals for each team based on ai/exp
                        double difference1 = aiExpTotals[team1] - aiExpTotals[team2];
                        double difference2 = aiExpTotals[team2] - aiExpTotals[team1];
                        double avgGoals1;
                        double avgGoals2;
                        if (Math.Abs(difference1) > 100)
                        {
                            avgGoals1 = avgGoals + Math.Pow((difference1 / 100), 3);
                            avgGoals2 = avgGoals + Math.Pow((difference2 / 100), 3);
                        }
                        else
                        {
                            avgGoals1 = avgGoals + (difference1 / 100);
                            avgGoals2 = avgGoals + (difference2 / 100);
                        }

                        matchups.Add(new Matchup(team1, team2, avgGoals1, avgGoals2, "Both"));
                    }
                }
            }

            int[,] places = new int[teams.Length, teams.Length];
            Random rand = new Random();

            for (int simNum = 0; simNum < numSimulations; simNum++)
            {
                int progressPercent = 0;
                if (simNum % 10000 == 0)
                {
                    progressPercent = (simNum * 100) / numSimulations;
                    worker.ReportProgress(progressPercent);
                }
                List<Game> games = gamesPlayed.ConvertAll(game => new Game(game.team1, game.team2, game.goals1, game.goals2));

                // simulate a game between each team
                foreach (Matchup matchup in matchups)
                {
                    double avgGoals1 = matchup.avgGoals1;
                    double avgGoals2 = matchup.avgGoals2;
                    int gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                    int gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);

                    // get new random goals scored for each team if they're the same
                    if (matchup.resultType == "Both") //either team can win
                    {
                        while (gaussian1 == gaussian2)
                        {
                            gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                            gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);
                        }
                    }
                    else if (matchup.resultType == "WinOnly") //only team1 can win
                    {
                        while (gaussian1 <= gaussian2)
                        {
                            gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                            gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);
                        }
                    }
                    else if (matchup.resultType == "LoseOnly") //only team2 can win
                    {
                        while (gaussian1 >= gaussian2)
                        {
                            gaussian1 = GetGoalsScored(rand, avgGoals1, 1.59);
                            gaussian2 = GetGoalsScored(rand, avgGoals2, 1.59);
                        }
                    }

                    games.Add(new Game(matchup.team1, matchup.team2, gaussian1, gaussian2));
                }

                List<int> sortedStandings = GetStandings(teams, teamNumbers, games);
                for (int place = 0; place < sortedStandings.Count; place++)
                {
                    int team = sortedStandings[place];
                    places[team, place] += 1;
                }
            }

            // output percentages of each team finishing in each place
            for (int i = 0; i < teamNumbers.Length; i++)
            {
                for (int place = 0; place < teamNumbers.Length; place++)
                {
                    double placeFinishes = places[i, place];
                    double percent = placeFinishes / numSimulations * 100;
                }
            }

            return places;
        }

        private List<int> GetStandings(string[] teams, int[] teamNumbers, List<Game> games)
        {
            // compile standings
            int[,] standings = CompileStandings(teams.Length, teamNumbers, games);

            // sort standings
            List<int> sortedStandings = SortStandings(false, teams, new List<int>(), games, standings, null);

            return sortedStandings;
        }

        private List<int> SortStandings(bool miniTable, string[] teams, List<int> sortedStandings, List<Game> games, int[,] standings, int[,] miniStandings)
        {
            int[,] standingsToUse;
            if (miniTable)
                standingsToUse = miniStandings;
            else
                standingsToUse = standings;

            // since points are based solely on wins, group teams based on wins so tiebreakers can be determined later
            Dictionary<int, List<int>> winsDict = new Dictionary<int, List<int>>();
            for (int row = 0; row < standingsToUse.GetLength(0); row++)
            {
                int wins = standingsToUse[row, 0];
                int losses = standingsToUse[row, 1];
                if (wins != 0 || losses != 0)
                {
                    List<int> teamsWithWin;
                    if (winsDict.TryGetValue(wins, out teamsWithWin))
                    {
                        teamsWithWin.Add(row);
                        winsDict[wins] = teamsWithWin;
                    }
                    else
                        winsDict[wins] = new List<int>() { row };
                }
            }

            // most wins should be first so reverse array
            List<int> winsList = winsDict.Keys.ToList();
            winsList.Sort((x, y) => y.CompareTo(x));

            // if more than one team have the same number of wins, determine who finishes ahead based on tiebreakers:
            // (all teams are guaranteed to have played each other because standings
            // only generated after all games have been played in the group)
            // 1) If 2 teams are tied, the team that won the mutual game is ahead in the standings
            // 2) If 3 or more teams are tied, make a "mini standings" with only those teams that are tied
            // ---If 3 or more teams are still tied in the "mini standings":
            // ---a) Goal differential in "mini standings"
            // ---b) Goals for in "mini standings"
            // ---c) Goal differential in standings
            // ---d) Goals for in standings
            // ---e) Alphabetical
            foreach (int win in winsList)
            {
                List<int> teamsWithWin = winsDict[win];
                if (teamsWithWin.Count == 1)
                    sortedStandings.Add(teamsWithWin[0]);
                else if (teamsWithWin.Count == 2)
                {
                    int team1 = teamsWithWin[0];
                    int team2 = teamsWithWin[1];
                    foreach (Game game in games)
                    {
                        if (game.team1 == team1 && game.team2 == team2)
                        {
                            if (game.goals1 > game.goals2)
                            {
                                sortedStandings.Add(teamsWithWin[0]);
                                sortedStandings.Add(teamsWithWin[1]);
                            }
                            else
                            {
                                sortedStandings.Add(teamsWithWin[1]);
                                sortedStandings.Add(teamsWithWin[0]);
                            }
                            break;
                        }
                        else if (game.team2 == team1 && game.team1 == team2)
                        {
                            if (game.goals1 > game.goals2)
                            {
                                sortedStandings.Add(teamsWithWin[1]);
                                sortedStandings.Add(teamsWithWin[0]);
                            }
                            else
                            {
                                sortedStandings.Add(teamsWithWin[0]);
                                sortedStandings.Add(teamsWithWin[1]);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    // at least 3 teams tied
                    if (miniTable)
                        // if get to here, using a mini table didn't determine who should be ahead in standings so use tiebreakers
                        sortedStandings = UseTiebreakers(teams, teamsWithWin, "miniGD", sortedStandings, standings, miniStandings);
                    else
                    {
                        // using all games so get only games that involve 2 of the
                        // teams that are tied in points and make mini standings
                        List<Game> miniGames = new List<Game>();
                        int numGames;
                        if (teamsWithWin.Count == 3)
                            numGames = 3;
                        else if (teamsWithWin.Count == 4)
                            numGames = 6;
                        else
                            numGames = 10;

                        foreach (Game game in games)
                        {
                            if (teamsWithWin.Contains(game.team1) && teamsWithWin.Contains(game.team2))
                            {
                                miniGames.Add(game);
                                if (miniGames.Count == numGames)
                                    break;
                            }
                        }

                        miniStandings = CompileStandings(teams.Length, teamsWithWin.ToArray(), miniGames);
                        sortedStandings = SortStandings(true, teams, sortedStandings, miniGames, standings, miniStandings);
                    }
                }
            }
            return sortedStandings;
        }

        private List<int> UseTiebreakers(string[] teams, List<int> teamsToBreakTie, string tiebreaker, List<int> sortedStandings, int[,] standings, int[,] miniStandings)
        {
            // teams are still tied after use of "mini standings" so use tiebreakers
            // to determine who should be ahead in the standings
            // if tiebreaker is "miniGD", use goal differential of the teams from the mini standings
            // if tiebreaker is "miniGF", use goals for of the teams from the mini standings
            // if tiebreaker is "mainGD", use goal differential of the teams from the main standings
            // if tiebreaker is "mainGF", use goals for of the teams from the main standings
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            foreach (int team in teamsToBreakTie)
            {
                int standingsStat = 0;

                if (tiebreaker == "miniGD")
                    standingsStat = miniStandings[team, 2] - miniStandings[team, 3];
                else if (tiebreaker == "miniGF")
                    standingsStat = miniStandings[team, 2];
                if (tiebreaker == "mainGD")
                    standingsStat = standings[team, 2] - standings[team, 3];
                else if (tiebreaker == "mainGF")
                    standingsStat = standings[team, 2];

                List<int> teamsWithSameStat;
                if (dict.TryGetValue(standingsStat, out teamsWithSameStat))
                {
                    teamsWithSameStat.Add(team);
                    dict[standingsStat] = teamsWithSameStat;
                }
                else
                    dict[standingsStat] = new List<int>() { team };
            }

            List<int> dictList = dict.Keys.ToList();
            dictList.Sort((x, y) => y.CompareTo(x));

            foreach (int stat in dictList)
            {
                List<int> teamsWithSameStat = dict[stat];
                if (teamsWithSameStat.Count == 1)
                    sortedStandings.Add(teamsWithSameStat[0]);
                else
                {
                    // teams are tied using current tiebreaker so move onto the next tiebreaker
                    if (tiebreaker == "miniGD")
                        sortedStandings = UseTiebreakers(teams, teamsWithSameStat, "miniGF", sortedStandings, standings, miniStandings);
                    else if (tiebreaker == "miniGF")
                        sortedStandings = UseTiebreakers(teams, teamsWithSameStat, "mainGD", sortedStandings, standings, miniStandings);
                    else if (tiebreaker == "mainGD")
                        sortedStandings = UseTiebreakers(teams, teamsWithSameStat, "mainGF", sortedStandings, standings, miniStandings);
                    else if (tiebreaker == "mainGF")
                    {
                        // no more tiebreakers left so use alphabetical order
                        List<string> teamsToAlphabetize = new List<string>();
                        for (int i = 0; i < teamsWithSameStat.Count; i++)
                            teamsToAlphabetize.Add(teams[teamsWithSameStat[i]]);
                        teamsToAlphabetize.Sort();
                        foreach (string teamName in teamsToAlphabetize)
                        {
                            foreach (int teamNum in teamsWithSameStat)
                            {
                                if (teamName == teams[teamNum])
                                {
                                    sortedStandings.Add(teamNum);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return sortedStandings;
        }

        private int[,] CompileStandings(int numTeams, int[] teamNumbers, List<Game> games)
        {
            // take each game and put into standings for both teams involved
            // wins, losses, goals for, goals against
            int[,] standings = new int[numTeams, 4];

            foreach (Game game in games)
            {
                int team1 = game.team1;
                int team2 = game.team2;
                int goalsTeam1 = game.goals1;
                int goalsTeam2 = game.goals2;
                if (goalsTeam1 > goalsTeam2)
                {
                    standings[team1, 0] += 1; // add win to team1
                    standings[team2, 1] += 1; // add loss to team2
                }
                else
                {
                    standings[team1, 1] += 1; // add loss to team1
                    standings[team2, 0] += 1; // add win to team2
                }
                standings[team1, 2] += goalsTeam1;
                standings[team2, 2] += goalsTeam2;
                standings[team1, 3] += goalsTeam2;
                standings[team2, 3] += goalsTeam1;
            }

            return standings;
        }

        private int GetGoalsScored(Random rand, double mean, double stdDev)
        {
            double u1 = rand.NextDouble();
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + stdDev * randStdNormal;
            int gaussian = (int)Math.Round(randNormal, 0, MidpointRounding.AwayFromZero);

            if (gaussian < 0)
                gaussian = 0;
            else if (gaussian >= 20)
                gaussian = 19;

            return gaussian;
        }
    }

    public class Game
    {
        public int team1;
        public int team2;
        public int goals1;
        public int goals2;

        public Game(int team1, int team2, int goals1, int goals2)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.goals1 = goals1;
            this.goals2 = goals2;
        }
    }

    public class Matchup
    {
        public int team1;
        public int team2;
        public double avgGoals1;
        public double avgGoals2;
        public string resultType;

        public Matchup(int team1, int team2, double avgGoals1, double avgGoals2, string resultType)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.avgGoals1 = avgGoals1;
            this.avgGoals2 = avgGoals2;
            this.resultType = resultType;
        }
    }
}
