using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMultipleAudioConsequence : Consequence
{
    public List<AudioSource> sources;
    int index;
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
    }

    void PlayAudio()
    {
        
        if (!sources[index].isPlaying)
            sources[index].Play();
        else
            sources[index].timeSamples = 0;
        
        index++;
        index %= sources.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
