using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace game_trial_3.Pages
{
    public partial class Intro : Page, IDisposable
    {
        void IDisposable.Dispose() { }
        private readonly Random _random = new Random();
        
        public Intro()
        {
            InitializeComponent();
            MovingAnimation();
            
            /*GameScreen.Focus();

            GameTimer.Interval = TimeSpan.FromMilliseconds(16);
            GameTimer.Tick += GameTick;
            GameTimer.Start();*/
        }
        
        private void MovingAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = Canvas.GetTop(Player),
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true, // Make it jittery by moving up and down
                RepeatBehavior = new RepeatBehavior(10), // Repeat 3 times, adjust as needed
            };

            animation.Completed += (sender, e) =>
            {
                // Reset the position when the animation is done
                Canvas.SetTop(Player, 100);
            };

            // Set new target positions for jittery movement
            double targetTop = Canvas.GetTop(Player) + _random.Next(-300, 301); // This is where you can set how far you want them to move

            animation.To = targetTop;

            Storyboard.SetTarget(animation, Player);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.TopProperty));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin(this);
        }

        
    }
}