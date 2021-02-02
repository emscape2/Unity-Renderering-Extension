using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_ALL_Audio_volumes : Consequence
{
    private bool muted;

    public override void Disengage()
    {
        ToggleAudio();
    }

    public void DisableAudio()
    {
        SetAudioMute(false);
    }

    public void EnableAudio()
    {
        SetAudioMute(true);
    }

    public void ToggleAudio()
    {
        if (muted)
            DisableAudio();
        else
            EnableAudio();
    }

    private void SetAudioMute(bool mute)
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = mute;
        }
        muted = mute;
    }
}
