using System.Windows;
using System.Windows.Controls;
using game_trial_3.Pages;

namespace game_trial_3.UserControls
{
    public partial class NewPlayerStart : UserControl
    {
        public NewPlayerStart()
        {
            InitializeComponent();
        }

        private void BtnCloseTab(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            ((Window)Parent).Content = new Intro();
        }
    }
}