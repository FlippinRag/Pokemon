using game_trial_3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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
using game_trial_3.Pages;
using MySql.Data.MySqlClient;

namespace game_trial_3.UserControls
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl, IDisposable
    {
        void IDisposable.Dispose() { }
        private readonly MediaPlayer mMediaPlayer = new MediaPlayer();

        public Register()
        {
            InitializeComponent();
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

        private void BtnCloseTab(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnRegisterClick(object sender, RoutedEventArgs e)
        {
            // Get the entered player name and password.
            string playerName = NewtxtPlayer.Text;
            string password = NewtxtPass.Password;

            // Check if the player name is unique.
            if (IsPlayerNameUnique(playerName))
            {
                // Insert the new player into the database.
                if (InsertNewPlayer(playerName, password))
                {
                    MessageBox.Show("Registration successful!");
                    mMediaPlayer.Stop();
                    ((Window)Parent).Content = new NewPlayerStart();
                }
                else
                {
                    MessageBox.Show("An error occurred while registering the player.");
                }
            }
            else
            {
                MessageBox.Show("Player name is already taken. Please choose a different name.");
            }
        }
        
        private bool IsPlayerNameUnique(string playerName)
        {
            string connectionString = "SERVER=localhost;DATABASE=pokemongame;UID=root;PASSWORD=root";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT playerID FROM players WHERE playerName = @playerName";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playerName", playerName);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        return !reader.Read(); // If a row is found, the name is not unique.
                    }
                }
            }
        }
        private bool InsertNewPlayer(string playerName, string password)
        {
            string connectionString = "SERVER=localhost;DATABASE=pokemongame;UID=root;PASSWORD=root";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO players (playerName, playerPassword) VALUES (@playerName, @password)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playerName", playerName);
                    command.Parameters.AddWithValue("@password", password);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // If at least one row is affected, the insertion was successful.
                }
            }
        }
        
        private void BtnGoBack(object sender, RoutedEventArgs e)
        {
            mMediaPlayer.Stop();
            ((Window)Parent).Content = new MainMenu();
        }
    }
}