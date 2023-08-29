using game_trial_3.Properties;
using game_trial_3.UserControls;
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

namespace game_trial_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        void IDisposable.Dispose() { }
        public MainWindow()
        {
            InitializeComponent();

            if ((bool)Settings.Default["windowed"] == true)
            {
                WindowStyle = WindowStyle.ToolWindow;
            }

            WindowState = WindowState.Maximized;
            Content = new MainMenu();
        }
    }
}
