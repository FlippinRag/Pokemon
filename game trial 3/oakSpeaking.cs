using System;
using System.Collections.Generic;
using System.Windows.Threading;

public class OakSpeaking
{
    private List<string> lines;
    public int currentLinesIndex;
    private int currentSpeechIndex;
    private DispatcherTimer textAnimationTimer;
    private bool speechAnimationStarted = false;

    public bool SpeechDone { get; private set; }
    public event EventHandler<string> OnSpeechUpdated;

    public OakSpeaking(List<string> speechLines)
    {
        lines = speechLines ?? throw new ArgumentNullException(nameof(speechLines));
        currentLinesIndex = 0;
        currentSpeechIndex = 0;

        textAnimationTimer = new DispatcherTimer();
        textAnimationTimer.Interval = TimeSpan.FromMilliseconds(0.5);
        textAnimationTimer.Tick += TextAnimationTick;
    }

    public void StartSpeech()
    {
        textAnimationTimer.Start();

        if (!speechAnimationStarted) // checks if the speech started or not
        {
            textAnimationTimer.Tick += (sender, e) =>
            {
                if (currentLinesIndex >= lines.Count) // once speech has ended, set SpeechDone to true
                {
                    textAnimationTimer.Stop();
                    SpeechDone = true;
                }
            };
            speechAnimationStarted = true;
        }
    }

    private void TextAnimationTick(object sender, EventArgs e)
    {
        if (currentLinesIndex < lines.Count)
        {
            string currentSentence = lines[currentLinesIndex];
            if (currentSpeechIndex < currentSentence.Length)
            {
                OnSpeechUpdated?.Invoke(this, currentSentence[currentSpeechIndex].ToString());
                currentSpeechIndex++;
            }
            else
            {
                OnSpeechUpdated?.Invoke(this, ""); // Display an empty string for line breaks
                currentLinesIndex++;
                currentSpeechIndex = 0;
            }
        }
    }
}