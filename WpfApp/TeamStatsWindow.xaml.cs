using DataLayer.Models;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TeamStatsWindow.xaml
    /// </summary>
    public partial class TeamStatsWindow : Window
    {
        IEnumerable<Match> matches;
        Team selectedTeam;
        public TeamStatsWindow(Team selectedTeam, IEnumerable<Match> matches)
        {
            this.selectedTeam = selectedTeam;
            this.matches = matches;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            int losses = 0, wins = 0, undecided = 0, played = matches.Count();
            int goalsGiven = 0, goalsRecieved = 0;

            lblCountry.Content = $"{selectedTeam.Country} ({selectedTeam.FifaCode})";
            foreach (Match match in matches)
            {
                MatchTeam targetTeam, opposingTeam;
                if (match.HomeTeam.Code == selectedTeam.FifaCode && match.Status == Status.Completed)
                {
                    targetTeam = match.HomeTeam;
                    opposingTeam = match.AwayTeam;
                }
                else if (match.AwayTeam.Code == selectedTeam.FifaCode && match.Status == Status.Completed)
                {
                    targetTeam = match.AwayTeam;
                    opposingTeam = match.HomeTeam;
                }
                else
                {
                    undecided++;
                    continue;
                }

                if (targetTeam.Code == match.WinnerCode)
                {
                    wins++;
                }
                else
                {
                    losses++;
                }

                goalsGiven += targetTeam.Goals;
                goalsRecieved += opposingTeam.Goals;
            }

            lblLoses.Content = losses;
            lblPlayed.Content = played;
            lblUndecided.Content = undecided;
            lblWon.Content = wins;

            lblGoalsGiven.Content = goalsGiven;
            lblGoalsReceived.Content = goalsRecieved;
            lblDiff.Content = goalsGiven - goalsRecieved;
        }
    }
}
