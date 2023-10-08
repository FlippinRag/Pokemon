
using game_trial_3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace game_trial_3.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, IDisposable
    {
        void IDisposable.Dispose() { }

        private readonly MediaPlayer mMediaPlayer = new MediaPlayer();

        public MainMenu()
        {
            InitializeComponent();

            CheckBoxMusic.IsChecked = (bool)Settings.Default["music"];
            CheckBoxMusic.Checked += CheckBoxMusic_Checked;
            CheckBoxMusic.Unchecked += CheckBoxMusic_Unchecked;
            mMediaPlayer.Open(new Uri(string.Format("C:\\Users\\sedai\\RiderProjects\\Pokemon\\game trial 3\\Music\\106TheRoadToVeridianFromPallet.wava", AppDomain.CurrentDomain.BaseDirectory)));
            mMediaPlayer.MediaEnded += new EventHandler(Media_Ended);

            

            if ((bool)Settings.Default["music"] == false)
            {
                mMediaPlayer.Stop();
            }
            else
            {
                mMediaPlayer.Position = TimeSpan.Zero;
                mMediaPlayer.Play();
            }
        }

        private void CheckBoxMusic_Checked(object sender, RoutedEventArgs e)
        {
            mMediaPlayer.Play();
        }

        private void CheckBoxMusic_Unchecked(object sender, RoutedEventArgs e)
        {

            mMediaPlayer.Stop();
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            if ((bool)Settings.Default["music"] == true)
            {
                mMediaPlayer.Position = TimeSpan.Zero;
                mMediaPlayer.Play();
            }
        }

        private void BtnCloseTab(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            mMediaPlayer.Stop();
           ((Window)Parent).Content = new Login();
        }

        private void BtnNewPlayer(object sender, RoutedEventArgs e)
        {
            mMediaPlayer.Stop();
            ((Window)Parent).Content = new Register();
        }
    }
}
