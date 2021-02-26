using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAudioSourcesConsequence : Consequence
{
    private bool paused;

    public override void Disengage()
    {
        ToggleAudio();
    }

    public void DisableAudio()
    {
        SetAudioPause(false);
        paused = true;
    }

    public void EnableAudio()
    {
        SetAudioPause(true);
        paused = false;
    }

    public void ToggleAudio()
    {
        if (!paused)
            DisableAudio();
        else
            EnableAudio();
    }

    private void SetAudioPause(bool pause)
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (var audioS in sources)
        {
            if (!pause)
            {
                if (audioS.isPlaying)
                {
                    audioS.Pause();
                }

            }
            else
            {
                audioS.UnPause();
            }
        }
    }
}
