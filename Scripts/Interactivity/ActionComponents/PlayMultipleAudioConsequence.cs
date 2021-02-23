using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMultipleAudioConsequence : Consequence
{
    public List<AudioSource> sources;
    int index;
    bool bPlay;
    public override void Disengage()
    {
        PlayAudio();
    }

    public override void Engage()
    {
        PlayAudio();
    }
    public override bool CanEngage()
    {
        if (sources.Any())
            return true;
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        foreach (var source in sources)
        {
            var tag = source.gameObject.tag;
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
    }

    void PlayAudio()
    {
        if (bPlay)
        {
            if (!sources[index].isPlaying)
                sources[index].Play();
            else
                sources[index].timeSamples = 0;
        }
        
        
        index++;
        index %= sources.Count;
        if (!bPlay)
        {
            bPlay = true;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
