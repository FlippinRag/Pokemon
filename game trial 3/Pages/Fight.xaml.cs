using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Org.BouncyCastle.Asn1.X509;

namespace game_trial_3.Pages
{
    public partial class Fight : Page
    {
        public string SelectedStarter { get; private set; }
        private Random random = new Random();
        private readonly List<string> tutorialQuestion = new List<string>
        {
            "Hello! Its me again",
            "would you like a brief tutorial on how to play the game?"
        };
        private readonly List<string> tutorial = new List<string>
        {
            "Alrighty!",
            "Let me BRIEFLY explain how to play the game!",
        };

        private OakSpeaking oakSpeaking;
        private DispatcherTimer checkSpeechDoneTimer;

        public Fight(string selectedStarter)
        {
            InitializeComponent();

            Background.Opacity = 0.5;

            OakSpeechShow();
            MovingAnimation();
            
            oakSpeaking = new OakSpeaking(tutorialQuestion);
            oakSpeaking.OnSpeechUpdated += HandleSpeechUpdated;
            oakSpeaking.StartSpeech();
            
            checkSpeechDoneTimer = new DispatcherTimer();
            checkSpeechDoneTimer.Interval = TimeSpan.FromMilliseconds(100);
            checkSpeechDoneTimer.Tick += CheckSpeechDone;
            checkSpeechDoneTimer.Start();
        }

        private void CheckSpeechDone(object sender, EventArgs e)
        {
            if (oakSpeaking.SpeechDone)
            {
                checkSpeechDoneTimer.Stop();
                ShowButtons();
            }
        }
        private void HandleSpeechUpdated(object sender, string currentCharacter)
        {
            if (string.IsNullOrEmpty(currentCharacter) && oakSpeaking.currentLinesIndex < tutorialQuestion.Count - 1)
            {
                SpeechText.Text = string.Empty;
            }
            else
            {
                SpeechText.Text += currentCharacter;
            }
        }
        

        private void OakSpeechShow()
        {
            oak.Visibility = Visibility.Visible;
            SpeechText.Visibility = Visibility.Visible;
            SpeechBubble.Visibility = Visibility.Visible;
        }

        private void OakSpeechClose()
        {
            oak.Visibility = Visibility.Collapsed;
            SpeechText.Visibility = Visibility.Collapsed;
            SpeechBubble.Visibility = Visibility.Collapsed;
        }
        private void ShowButtons()
        {
            Tutorial.Visibility = Visibility.Visible;
            NoTutorial.Visibility = Visibility.Visible;
        }
        private void HideButtons()
        {
            Tutorial.Visibility = Visibility.Collapsed;
            NoTutorial.Visibility = Visibility.Collapsed;
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
        
        private void NoTutorialClick(object sender, RoutedEventArgs e)
        {
            HideButtons();
            OakSpeechClose();
            OakSpeechClose();
            Background.Opacity = 1;
        }

        private void TutorialClick(object sender, RoutedEventArgs e)
        {
            HideButtons();
        }
    }
}