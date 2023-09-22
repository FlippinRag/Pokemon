using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using game_trial_3.Pages;
using MySql.Data.MySqlClient;

namespace game_trial_3.UserControls
{
    public partial class Stats : UserControl, IDisposable
    {
        void IDisposable.Dispose() { }
        private Login.PlayerData _playerData;
        public Stats(Login.PlayerData playerData)
        {
            InitializeComponent();
            
            _playerData = playerData;

            string connectionString = "SERVER=localhost;DATABASE=pokemongame;UID=root;PASSWORD=root";

            MySqlConnection connection = new MySqlConnection(connectionString);

            string query = "SELECT pokemonName, xp " +
                           "FROM pokemon, players " +
                           "WHERE pokemon.playerID = players.playerID " +
                           "AND players.playerName = @PlayerName " +
                           "AND players.playerPassword = @PlayerPassword;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PlayerName", _playerData.PlayerName);
            cmd.Parameters.AddWithValue("@PlayerPassword", _playerData.Password);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            DtGrid.DataContext = dt;
            
            
               /* <DataGrid  Margin="50,100,50,300" 
            Name="DtGrid" 
            ItemsSource="{Binding}"
            IsReadOnly="True">
                
                </DataGrid>*/
        }

        private void BtnGoBack(object sender, RoutedEventArgs e)
        {
            ((Window)Parent).Content = new MainMenu();
        }

        private void BtnCloseTab(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("going to be implemented very very soon!!!!!!!!!!!");   
        }
    }
}