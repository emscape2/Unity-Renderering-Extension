using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceConsequence : Consequence
{
    public AudioSource source;
    public bool muteIfUnMuted;
    public override void Disengage()
    {
        
    }

    public override void Engage()
    {
        var global = GlobalVars.getGlobalVars();
        var waarde = global.getVar(tag + "Audio");
        if (!source.isPlaying )
        {
            source.Play();
            if ( waarde == 0)
                source.mute = false;
        }
        else
        {
            if (muteIfUnMuted)
               source.mute = true;
            source.timeSamples = 0;
        }
    }
    public override bool CanEngage()
    {
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        var global = GlobalVars.getGlobalVars();
        var waarde = global.getVar(tag + "Audio");
        if (waarde == 0)
        {
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
