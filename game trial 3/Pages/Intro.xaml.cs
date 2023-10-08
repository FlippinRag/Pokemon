using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using game_trial_3.Pages;

namespace game_trial_3.Pages
{
    public partial class Intro : Page, IDisposable
    {
        void IDisposable.Dispose() { }
        
        public string SelectedStarter { get; private set; }
        private Random random = new Random();
        private OakSpeaking oakSpeaking;
        private List<string> Lines = new List<string>
        {
            "I think so...",
            "I think it's time...",
            "WELCOME TO THE POKEMON BATTLE SIMULATOR",
            "My name is Professor Samuel Oak",
            "Today I declare you prepared for the Battle world! :D",
            "Its not easy mind you.. BUT I THINK YOU'RE READY!!!",
            "The first step is to CHOOSE YOUR POKEMON",
            
        };

        public Intro()
        {
            InitializeComponent();
            oakSpeaking = new OakSpeaking(Lines);
            oakSpeaking.OnSpeechUpdated += HandleSpeechUpdated;
            oakSpeaking.StartSpeech();
            MovingAnimation();
        }

        private void HandleSpeechUpdated(object sender, string currentCharacter)
        {
            if (string.IsNullOrEmpty(currentCharacter))
            {
                SpeechText.Text = string.Empty;
            }
            else
            {
                SpeechText.Text += currentCharacter;
            }
        }
        
        private void CreatePokemonButtons() // creates buttons for the pokemon and assigns them a place in the stack panel
        {
            Button bulbasaurButton = new Button();
            Image bulbasaurImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pokemon/bulbasaur.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.None
            };
            ChooseYourPokemon.Children.Add(bulbasaurButton);
            bulbasaurButton.Content = bulbasaurImage;
            bulbasaurButton.Width = bulbasaurImage.Source.Width;
            bulbasaurButton.Height = bulbasaurImage.Source.Height;
            bulbasaurButton.Background = Brushes.Transparent;
            bulbasaurButton.Click += BulbSelected;

            Button charmanderButton = new Button();
            Image charmanderImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pokemon/charmander.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.None
            };
            ChooseYourPokemon.Children.Add(charmanderButton);
            charmanderButton.Content = charmanderImage;
            charmanderButton.Width = charmanderImage.Source.Width;
            charmanderButton.Height = charmanderImage.Source.Height;
            charmanderButton.Background = Brushes.Transparent;
            charmanderButton.Click += CharSelected;
            
            Button squirtleButton = new Button();
            Image squirtleImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pokemon/squirtle.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.None  
            };
            ChooseYourPokemon.Children.Add(squirtleButton);
            squirtleButton.Content = squirtleImage;
            squirtleButton.Width = squirtleImage.Source.Width;
            squirtleButton.Height = squirtleImage.Source.Height;
            squirtleButton.Background = Brushes.Transparent;
            squirtleButton.Click += SquirtSelected;
            
            Button rioluButton = new Button();
            Image rioluImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/Pokemon/riolu.png", UriKind.RelativeOrAbsolute)),
                Stretch = Stretch.None
            };
            ChooseYourPokemon.Children.Add(rioluButton);
            rioluButton.Content = rioluImage;
            rioluButton.Width = rioluImage.Source.Width;
            rioluButton.Height = rioluImage.Source.Height;
            rioluButton.Background = Brushes.Transparent;
            rioluButton.Click += RioSelected;
            
        }

        private void BulbSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nice... i lied whats wrong with you");
            SelectedStarter = "Bulbasaur";
            ((Window)Parent).Content = new Fight(SelectedStarter);

        }

        private void CharSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nice... someones tryna beat the game fast");
            SelectedStarter = "Charmander";
            ((Window)Parent).Content = new Fight(SelectedStarter);
        }

        private void SquirtSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nice... water bender wanna be");
            SelectedStarter = "Squirtle";
            ((Window)Parent).Content = new Fight(SelectedStarter);
        }

        private void RioSelected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("nice... daniel");
            SelectedStarter = "Riolu";
            ((Window)Parent).Content = new Fight(SelectedStarter);
        }
        
        private void MovingAnimation() // adds oak moving to make it look cool
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = Canvas.GetTop(oak),
                Duration = TimeSpan.FromSeconds(0.25),
                AutoReverse = true, 
                RepeatBehavior = new RepeatBehavior(5), // Choose repetitions.. yea that's it
            };

            double targetTop = Canvas.GetTop(oak) + random.Next(-50, 101); // Setting how far you can move them

            animation.To = targetTop;

            Storyboard.SetTarget(animation, oak);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin(oak);
        }
    }
}