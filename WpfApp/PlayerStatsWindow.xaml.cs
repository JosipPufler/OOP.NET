using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for PlayerStatsWindow.xaml
    /// </summary>
    public partial class PlayerStatsWindow : Window
    {
        public PlayerStatsWindow(PlayerPicture playerData)
        {
            InitializeComponent();

            lblGoals.Content = playerData.player.Goals;
            lblPosition.Content = playerData.player.Position;
            lblYellow.Content = playerData.player.YellowCards;
            lblShirtNumber.Content = playerData.player.ShirtNumber;
            lblPlayerName.Content = $"{playerData.player.Name}{(playerData.player.Captain ? " (C)" : "")}";
            playerPicture.ImageSource = playerData.picturePath;
        }
    }
}
