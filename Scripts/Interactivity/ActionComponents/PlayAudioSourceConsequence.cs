using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSourceConsequence : Consequence
{
    public AudioSource source;
    public override void Disengage()
    {
        
    }

    public override void Engage()
    {
        if (!source.isPlaying)
            source.Play();
        else
            source.timeSamples = 0;
    }
    public override bool CanEngage()
    {
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
