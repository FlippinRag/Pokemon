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
        private readonly List<string> Lines = new List<string>
        {
            "Hello! Its me again",
            "would you like a brief tutorial on how to play the game?",
            "asdasd"
        };

        private int currentLinesIndex = 0;
        private int currentSpeechIndex = 0;
        private bool isLinesDone = false;
        private DispatcherTimer textAnimationTimer;
        private bool speechAnimationStarted = false;
        private int lastTutorialLineIndex = 0;

        private bool playerChoseTutorial = false;
        
        private TaskCompletionSource<bool> yesButtonClickTaskCompletionSource;
        
        public Fight(string selectedStarter)
        {
            InitializeComponent();

            Background.Opacity = 0.5;
            
            MovingAnimation();
            textAnimationTimer = new DispatcherTimer();
            textAnimationTimer.Interval = TimeSpan.FromMilliseconds(50);
            textAnimationTimer.Tick += TextAnimationTickWrapper;
            StartSpeech();
            
        }
        
        private void TextAnimationTickWrapper(object sender, EventArgs e)
        {
            _ = TextAnimationTick();
        }

        private async Task TextAnimationTick()
        {
            if (currentLinesIndex < Lines.Count)
            {
                if (!isLinesDone)
                {
                    SpeechText.Text = "";
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

                    ShowButtons();
                    if (currentLinesIndex == 2)
                    {
                        await WaitForYesButtonClickAsync();
                    }
                    else
                    {
                        currentSpeechIndex = 0;
                        StartSpeech();
                    }
                }
            }
            else
            {
                textAnimationTimer.Stop();
            }
        }


        private async Task WaitForYesButtonClickAsync()
        {
            yesButtonClickTaskCompletionSource = new TaskCompletionSource<bool>();
            await yesButtonClickTaskCompletionSource.Task;
            yesButtonClickTaskCompletionSource = null;
            StartSpeech();
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
        
        private void StartSpeech() 
        {
            
            OakSpeechShow();
            
            textAnimationTimer.Start();
            MovingAnimation();
            
            if (!speechAnimationStarted) //checks if the speech started or not
            {
                textAnimationTimer.Tick += (sender, e) =>
                {
                    if (currentLinesIndex >= Lines.Count) 
                    {
                        textAnimationTimer.Stop();

                        if (playerChoseTutorial)
                        {
                            currentLinesIndex = lastTutorialLineIndex;
                        }
                    }
                };

                speechAnimationStarted = true;
            }
            
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
            textAnimationTimer.Stop();
            OakSpeechClose();
            Background.Opacity = 1;
        }

        private void TutorialClick(object sender, RoutedEventArgs e)
        {
            HideButtons();
            playerChoseTutorial = true;
            lastTutorialLineIndex = currentLinesIndex;
            yesButtonClickTaskCompletionSource?.SetResult(true);
            StartSpeech();
        }
    }
}