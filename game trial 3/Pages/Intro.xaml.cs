using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace game_trial_3.Pages
{
    public partial class Intro : Page, IDisposable
    {
        void IDisposable.Dispose() { }
        private Random random = new Random();
        private List<string> Lines = new List<string>
        {
            "I think so...",
            "I think it's time...",
            "WELCOME TO THE POKEMON BATTLE SIMULATOR",
            "My name is Professor Samuel Oak",
            "Today I declare you prepared for the Battle world! :D",
            "Its not easy mind you.. BUT I THINK YOU'RE READY!!!",
            "The last step is to CHOOSE YOUR POKEMON",
            
        };

        private int currentLinesIndex = 0;
        private int currentSpeechIndex = 0;
        private bool isLinesDone = false;
        private DispatcherTimer textAnimationTimer;
        private bool isSpeechDone = false;
        private bool speechAnimationStarted = false;
        
        public Intro()
        {
            InitializeComponent();
            MovingAnimation();
            textAnimationTimer = new DispatcherTimer();
            textAnimationTimer.Interval = TimeSpan.FromMilliseconds(10); // can change how fast you want it to read the text
            textAnimationTimer.Tick += TextAnimationTick;
            StartSpeech();
            
        }
        private void TextAnimationTick(object sender, EventArgs e)
        {
            if (currentLinesIndex < Lines.Count)
            {
                if (!isLinesDone)
                {
                    SpeechText.Text = ""; // Clear current line
                    isLinesDone = true;
                }
                string currentSentence = Lines[currentLinesIndex];
                if (currentSpeechIndex < currentSentence.Length)
                {
                    SpeechText.Text += currentSentence[currentSpeechIndex];
                    currentSpeechIndex++;
                }
                else
                {
                    isLinesDone = false;
                    currentLinesIndex++;
                    currentSpeechIndex = 0;
                    StartSpeech();
                }
            }
            else
            {
                textAnimationTimer.Stop();
                isSpeechDone = true;

            }
        }
    
        private void CreatePokemonButtons() // creates buttons for the pokemon and assigns them a place in the stack panel
        {
            Button bulbasaurButton = new Button();
            Image bulbasaurImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/bulbasaur.png", UriKind.RelativeOrAbsolute)),
            };
            bulbasaurButton.Content = bulbasaurImage;
            bulbasaurButton.Width = bulbasaurImage.Source.Width;
            bulbasaurButton.Height = bulbasaurImage.Source.Height;
            bulbasaurButton.Click += PokemonSelected;

            Button charmanderButton = new Button();
            Image charmanderImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/charmander.png", UriKind.RelativeOrAbsolute))
            };
            charmanderButton.Content = charmanderImage;
            charmanderButton.Width = charmanderImage.Source.Width;
            charmanderButton.Height = charmanderImage.Source.Height;
            charmanderButton.Click += PokemonSelected;
            
            ChooseYourPokemon.Children.Add(bulbasaurButton);
            ChooseYourPokemon.Children.Add(charmanderButton);
            
            
        }

        private void PokemonSelected(object sender, RoutedEventArgs e) // place holder for actually using it
        {
            MessageBox.Show("nice");
        }

        private void StartSpeech() 
        {
            textAnimationTimer.Start();
            MovingAnimation();
            
            if (!speechAnimationStarted) //checks if the speech started or not
            {
                textAnimationTimer.Tick += (sender, e) =>
                {
                    if (currentLinesIndex >= Lines.Count) // once speech has ended the buttons can now be made!!
                    {
                        textAnimationTimer.Stop();
                        CreatePokemonButtons();
                    }
                };

                speechAnimationStarted = true;
            }
            
        }
        
        private void MovingAnimation() // adds oak moving to make it look cool
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = Canvas.GetTop(Player),
                Duration = TimeSpan.FromSeconds(0.25),
                AutoReverse = true, 
                RepeatBehavior = new RepeatBehavior(4), // Choose repetitions.. yea that's it
            };

            double targetTop = Canvas.GetTop(Player) + random.Next(1, 101); // Setting how far you can move them

            animation.To = targetTop;

            Storyboard.SetTarget(animation, Player);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin(Player);
        }

        
    }
}