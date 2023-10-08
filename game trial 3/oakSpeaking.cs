using System;
using System.Collections.Generic;
using System.Windows.Threading;

public class OakSpeaking
{
    private List<string> lines;
    private int currentLinesIndex;
    private int currentSpeechIndex;
    private DispatcherTimer textAnimationTimer;

    public event EventHandler<string> OnSpeechUpdated;

    public OakSpeaking(List<string> speechLines)
    {
        lines = speechLines ?? throw new ArgumentNullException(nameof(speechLines));
        currentLinesIndex = 0;
        currentSpeechIndex = 0;

        textAnimationTimer = new DispatcherTimer();
        textAnimationTimer.Interval = TimeSpan.FromMilliseconds(20);
        textAnimationTimer.Tick += TextAnimationTick;
    }

    public void StartSpeech()
    {
        textAnimationTimer.Start();
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
                currentLinesIndex++;
                currentSpeechIndex = 0;
                OnSpeechUpdated?.Invoke(this, "");
            }
        }
        else
        {
            textAnimationTimer.Stop();
        }
    }
}