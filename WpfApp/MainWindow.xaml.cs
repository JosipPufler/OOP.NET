using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Utilities;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using WpfApp.controls;
using WpfApp.Localization;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<PlayerCard> FavStartingPlayerCards { get; set; }
        public ObservableCollection<PlayerCard> OpStartingPlayerCards { get; set; }
        public ObservableCollection<PlayerCard> FavSubPlayerCards { get; set; }
        public ObservableCollection<PlayerCard> OpSubPlayerCards { get; set; }

        private Dictionary<string, string> playerPictureDictionary;

        public UCPlayerIcon[] OpEleven;
        public UCPlayerIcon[] FavEleven;

        public bool LicenceToDie { get; set; } = false;

        IRepo repository = RepoFactory.getInstance();
        IEnumerable<Match> selectedMatches;
        Window childForm;

        SettingsWindow parentWindow;

        /*public MainWindow()
        {
            InitializeComponent();
            InitializeItemControls();

            NameScope.SetNameScope(myGrid, new NameScope());
        }*/

        public MainWindow(SettingsWindow parent)
        {
            parentWindow = parent;

            
            InitializeComponent();
            InitializeItemControls();


            NameScope.SetNameScope(myGrid, new NameScope());
            btnFirstDetails.Tag = cbFirst;
            btnSecondDetails.Tag = cbSecond;
            ApplySettings();
            
        }

        private void InitializeItemControls()
        {
            OpEleven = new UCPlayerIcon[]{ OpPlayer1, OpPlayer2, OpPlayer3, OpPlayer4, OpPlayer5, OpPlayer6, OpPlayer7, OpPlayer8, OpPlayer9, OpPlayer10, OpPlayer11 };
            FavEleven = new UCPlayerIcon[] { FavPlayer1, FavPlayer2, FavPlayer3, FavPlayer4, FavPlayer5, FavPlayer6, FavPlayer7, FavPlayer8, FavPlayer9, FavPlayer10, FavPlayer11 };

            playerPictureDictionary = FileUtils.GetPlayerPictures() == null ? new() : FileUtils.GetPlayerPictures();

            FavStartingPlayerCards = new();
            FavSubPlayerCards = new();
            OpStartingPlayerCards = new();
            OpSubPlayerCards = new();

            favTeam11.ItemsSource = FavStartingPlayerCards;
            opTeam11.ItemsSource = OpStartingPlayerCards;
            favTeamSubs.ItemsSource = FavSubPlayerCards;
            opTeamSubs.ItemsSource = OpSubPlayerCards;
        }

        public void UpdateSettings(Categories? userSettings) {
            if (userSettings != null && userSettings != ConfigService.UserSettings.category)
            {
                ClearCBSecond();
                ClearCBFirst();
                ConfigService.UserSettings.favCountry = null;
                ConfigService.UserSettings.favPlayers.Clear();
                ConfigService.SaveConfig();
                
                cbSecond.IsEnabled = false;
                FavStartingPlayerCards.Clear();
                OpStartingPlayerCards.Clear();
                FavSubPlayerCards.Clear();
                OpSubPlayerCards.Clear();
                for (int i = 0; i < 11; i++)
                {
                    OpEleven[i].Visibility = Visibility.Collapsed;
                    FavEleven[i].Visibility = Visibility.Collapsed;
                }
                ApplySettings();
            }
        }

        private void SetResolution()
        {
            switch (ConfigService.UserSettings.WpfResolution)
            {
                case Resolutions.FullScreen:
                    WindowState = WindowState.Maximized;
                    break;
                case Resolutions.Large:
                    Height = 1050;
                    Width = 1600;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    break;
                case Resolutions.Medium:
                    Height = 1024;
                    Width = 1280;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    break;
                case Resolutions.Wide:
                    Height = 768;
                    Width = 1366;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    break;
                default:
                    throw new ArgumentException("No such resolution");
            }
        }

        private async void ApplySettings()
        {
            SetResolution();
            ToggleLoading();
            List<Team> teams = (List<Team>)await repository.Get<Team>(ConfigService.UserSettings.category);
            ToggleLoading();

            foreach (Team team in teams)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Tag = team;
                comboBoxItem.Name = team.FifaCode;
                comboBoxItem.Content = team.ToString();

                myGrid.RegisterName(comboBoxItem.Name, comboBoxItem);

                cbFirst.Items.Add(comboBoxItem);
            }
            
            if (ConfigService.UserSettings.favCountry != null)
            {
                await SetTeam(ConfigService.UserSettings.favCountry);
            }
        }

        private async Task SetTeam(string? favCountry)
        {
            ToggleLoading();
            ConfigService.UserSettings.favCountry = favCountry;
            ConfigService.SaveConfig();
            var search = (ComboBoxItem)cbFirst.FindName(ConfigService.UserSettings.favCountry);
            if (cbFirst.SelectedItem == null)
            {
                cbFirst.SelectionChanged -= cbFirst_SelectionChanged;
                cbFirst.SelectedItem = search;
                cbFirst.SelectionChanged += cbFirst_SelectionChanged;
            }
            ClearCBSecond();
            cbSecond.IsEnabled = false;
            
            selectedMatches = await repository.GetMatchByFifaCode(ConfigService.UserSettings.category, ConfigService.UserSettings.favCountry);
            
            foreach (Match match in selectedMatches)
            {
                if (match.AwayTeam.Code != ConfigService.UserSettings.favCountry)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Tag = new Team() { 
                        FifaCode =  match.AwayTeam.Code,
                        Country = match.AwayTeam.Country,
                    };
                    comboBoxItem.Name = $"op{match.FifaId}";
                    comboBoxItem.Content = $"{match.AwayTeam.Country} ({match.AwayTeam.Code}) - {match.StageName}";

                    myGrid.RegisterName(comboBoxItem.Name, comboBoxItem);

                    cbSecond.Items.Add(comboBoxItem);
                }
                else
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Tag = new Team()
                    {
                        FifaCode = match.HomeTeam.Code,
                        Country = match.HomeTeam.Country,
                    };
                    comboBoxItem.Name = $"op{match.FifaId}";
                    comboBoxItem.Content = $"{match.HomeTeam.Country} ({match.HomeTeam.Code}) - {match.StageName}";

                    myGrid.RegisterName(comboBoxItem.Name, comboBoxItem);

                    cbSecond.Items.Add(comboBoxItem);
                }
            }
            cbSecond.IsEnabled = true;
            ToggleLoading();
        }

        private void ClearCBSecond()
        {
            cbSecond.SelectionChanged -= cbSecond_SelectionChanged;
            if (cbSecond.SelectedItem != null)
            {
                ((ComboBoxItem)cbSecond.SelectedItem).Content = string.Empty;
            }
            foreach (ComboBoxItem item in cbSecond.Items)
            {
                myGrid.UnregisterName(item.Name);
            }
            cbSecond.Items.Clear();
            lblScore.Content = Strings.ChooseOpponent;
            cbSecond.SelectionChanged += cbSecond_SelectionChanged;
        }

        private void ClearCBFirst()
        {
            cbFirst.SelectionChanged -= cbFirst_SelectionChanged;
            if (cbFirst.SelectedItem != null)
            {
                ((ComboBoxItem)cbFirst.SelectedItem).Content = string.Empty;
            }
            foreach (ComboBoxItem item in cbFirst.Items)
            {
                myGrid.UnregisterName(item.Name);
            }
            cbFirst.Items.Clear();
            cbFirst.SelectionChanged += cbFirst_SelectionChanged;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            childForm = new ConfirmWindow(this);
            childForm.ShowDialog();
            childForm = null;
            if (!LicenceToDie)
            {
                e.Cancel = true;
            }
        }

        private void cbSecond_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FavStartingPlayerCards.Clear();
            OpStartingPlayerCards.Clear();
            FavSubPlayerCards.Clear();
            OpSubPlayerCards.Clear();

            Match selectedMatch = null;
            TeamStatistics favTeam = null, opTeam = null;
            TeamEvent[] favTeamEvents = new TeamEvent[0];
            TeamEvent[] opTeamEvents = new TeamEvent[0];
            foreach (Match match in selectedMatches)
            {
                if (((ComboBoxItem)cbSecond.SelectedItem).Name.EndsWith(match.FifaId.ToString()))
                {
                    selectedMatch = match;
                    if ( ((string)((ComboBoxItem)cbSecond.SelectedItem).Content).StartsWith(match.HomeTeam.Country))
                    {
                        favTeam = match.AwayTeamStatistics;
                        opTeam = match.HomeTeamStatistics;

                        favTeamEvents = match.AwayTeamEvents;
                        opTeamEvents = match.HomeTeamEvents;
                        lblScore.Content = $"{match.AwayTeam.Goals}:{match.HomeTeam.Goals}";
                        break;
                    }
                    else if (((string)((ComboBoxItem)cbSecond.SelectedItem).Content).StartsWith(match.AwayTeam.Country))
                    {
                        favTeam = match.HomeTeamStatistics;
                        opTeam = match.AwayTeamStatistics;

                        favTeamEvents = match.HomeTeamEvents;
                        opTeamEvents = match.AwayTeamEvents;
                        lblScore.Content = $"{match.HomeTeam.Goals}:{match.AwayTeam.Goals}";
                        break;
                    }
                }
            }

            List<Player> FavStartingEleven = favTeam.StartingEleven.OrderBy(x => (int)x.Position).ToList();
            List<Player> OpStartingEleven = opTeam.StartingEleven.OrderBy(x => (int)x.Position).ToList();

            int i = 0;
            foreach (Player player in FavStartingEleven)
            {
                FavStartingPlayerCards.Add(new PlayerCard()
                {
                    Name = player.Name,
                    ShirtNumber = player.ShirtNumber < 10 ? $" {player.ShirtNumber} " : player.ShirtNumber.ToString()
                });

                foreach (var teamEvent in favTeamEvents)
                {
                    if (teamEvent.Player == player.Name && (teamEvent.TypeOfEvent == TypeOfEvent.YellowCard || teamEvent.TypeOfEvent == TypeOfEvent.YellowCardSecond))
                    {
                        player.YellowCards++;
                    }
                    else if(teamEvent.Player == player.Name && (teamEvent.TypeOfEvent == TypeOfEvent.Goal || teamEvent.TypeOfEvent == TypeOfEvent.GoalPenalty))
                    {
                        player.Goals++;
                    }
                }

                playerPictureDictionary.TryGetValue(player.ToString(), out string picturePath);
                BitmapImage playerPicture = new BitmapImage();
                playerPicture.BeginInit();
                playerPicture.DecodePixelHeight = 75;
                playerPicture.DecodePixelWidth = 75;
                if (picturePath != null)
                {
                    playerPicture.UriSource = new Uri(picturePath);
                }
                else
                {
                    playerPicture.UriSource = new Uri(@"..\..\..\Images\defaultIcon.png", UriKind.Relative);
                }
                playerPicture.EndInit();

                FavEleven[i].Player = player;
                FavEleven[i].PlayerPicture = playerPicture;
                FavEleven[i].Visibility = Visibility.Visible;

                i++;
            }

            i = 0;
            foreach (Player player in OpStartingEleven)
            {
                OpStartingPlayerCards.Add(new PlayerCard()
                {
                    Name = player.Name,
                    ShirtNumber = player.ShirtNumber < 10 ? $" {player.ShirtNumber} " : player.ShirtNumber.ToString()
                });

                foreach (var teamEvent in opTeamEvents)
                {
                    if (teamEvent.Player == player.Name && (teamEvent.TypeOfEvent == TypeOfEvent.YellowCard || teamEvent.TypeOfEvent == TypeOfEvent.YellowCardSecond))
                    {
                        player.YellowCards++;
                    }
                    else if (teamEvent.Player == player.Name && (teamEvent.TypeOfEvent == TypeOfEvent.Goal || teamEvent.TypeOfEvent == TypeOfEvent.GoalPenalty))
                    {
                        player.Goals++;
                    }
                }

                playerPictureDictionary.TryGetValue(player.ToString(), out string picturePath);
                BitmapImage playerPicture = new BitmapImage();
                playerPicture.BeginInit();
                playerPicture.DecodePixelHeight = 75;
                playerPicture.DecodePixelWidth = 75;
                if (picturePath != null)
                {
                    playerPicture.UriSource = new Uri(picturePath);
                }
                else
                {
                    playerPicture.UriSource = new Uri(@"..\..\..\Images\defaultIcon.png", UriKind.Relative);
                }
                playerPicture.EndInit();


                OpEleven[i].Player = player;
                OpEleven[i].PlayerPicture = playerPicture;
                OpEleven[i].Visibility = Visibility.Visible;

                i++;
            }

            foreach (Player player in favTeam.Substitutes)
            {
                FavSubPlayerCards.Add(new PlayerCard()
                {
                    Name = player.Name,
                    ShirtNumber = player.ShirtNumber < 10 ? $" {player.ShirtNumber} " : player.ShirtNumber.ToString()
                });
            }

            foreach (Player player in opTeam.Substitutes)
            {
                OpSubPlayerCards.Add(new PlayerCard()
                {
                    Name = player.Name,
                    ShirtNumber = player.ShirtNumber.ToString().Length == 1 ? $" {player.ShirtNumber}" : player.ShirtNumber.ToString()
                });
            }
        }

        private async void cbFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await SetTeam(((ComboBoxItem)cbFirst.SelectedItem).Name);
            foreach (var item in FavEleven)
            {
                item.Visibility = Visibility.Hidden;
            }
            foreach (var item in OpEleven)
            {
                item.Visibility = Visibility.Hidden;
            }
            FavStartingPlayerCards.Clear();
            OpStartingPlayerCards.Clear();
            FavSubPlayerCards.Clear();
            OpSubPlayerCards.Clear();
        }

        private async void ShowTeamStats(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)((Button)sender).Tag).SelectedItem != null)
            {
                ToggleLoading();
                var selected = await repository.GetMatchByFifaCode(ConfigService.UserSettings.category, ((Team)((ComboBoxItem)((ComboBox)((Button)sender).Tag).SelectedItem).Tag).FifaCode);
                ToggleLoading();
                childForm = new TeamStatsWindow((Team)((ComboBoxItem)((ComboBox)((Button)sender).Tag).SelectedItem).Tag, selected);
                childForm.ShowDialog();
                childForm = null;
            }
        }

        private void btnSettings_click(object sender, RoutedEventArgs e)
        {
            if (childForm == null)
            {
                childForm = new SettingsWindow(this);
                childForm.ShowDialog();
                childForm = null;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void ToggleLoading() {
            if (loadingGrid.Visibility == Visibility.Visible)
            {
                loadingGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                loadingGrid.Visibility = Visibility.Visible;
            }

            if (myGrid.Visibility == Visibility.Visible)
            {
                myGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                myGrid.Visibility = Visibility.Visible;
            }
        }
    }

    public class PlayerCard
    {
        public string ShirtNumber { get; set; }
        public string Name { get; set; }
    }
}   