using game_trial_3.Properties;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;


namespace game_trial_3.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, IDisposable
    {
        void IDisposable.Dispose() { }
        private readonly MediaPlayer mMediaPlayer = new MediaPlayer();
        private PlayerData playerData;

        public Login()
        {
            InitializeComponent();
            
            playerData = new PlayerData();
            
            CheckBoxMusic.IsChecked = (bool)Settings.Default["music"];
            CheckBoxMusic.Checked += CheckBoxMusic_Checked;
            CheckBoxMusic.Unchecked += CheckBoxMusic_Unchecked;
            mMediaPlayer.Open(new Uri(string.Format("C:\\Users\\Anurag Sedai\\Documents\\Codin\\C#\\game trial 3\\game trial 3\\Music\\106TheRoadToVeridianFromPallet.wav", AppDomain.CurrentDomain.BaseDirectory)));
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
        
        public class PlayerData
        {
            public string PlayerName { get; set; }
            public string Password { get; set; }
        }
        private bool IsValidUser(string playerName, string password)
        {
            string connectionString = "SERVER=localhost;DATABASE=pokemongame;UID=root;PASSWORD=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT playerID " +
                               "FROM players " +
                               "WHERE playerName = @playerName " +
                               "AND playerPassword = @password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playerName", playerName);
                    command.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
                connection.Close();
            }
            
            return false;
        }
        
        
        
        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            playerData.PlayerName = TxtPlayer.Text;
            playerData.Password = TxtPass.Password;
            
            bool isAuthenticated = IsValidUser(playerData.PlayerName, Encrypt.HashString(playerData.Password));
            if (isAuthenticated)
            {
                mMediaPlayer.Stop();
                ((Window)Parent).Content = new Stats(playerData);
            }
            else
            {
                
                MessageBox.Show("Wrong login. Try again please!");
            }
            

        }
        
        private void BtnCloseTab(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnGoBack(object sender, RoutedEventArgs e)
        {
            mMediaPlayer.Stop();
            ((Window)Parent).Content = new MainMenu();
        }
    }
}
