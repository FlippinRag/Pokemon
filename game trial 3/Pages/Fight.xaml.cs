using System.Windows;
using System.Windows.Controls;

namespace game_trial_3.Pages
{
    public partial class Fight : Page
    {
        public Fight(string selectedStarter)
        {
            InitializeComponent();
            
            MessageBox.Show($"You chose {selectedStarter}");
        }
    }
}