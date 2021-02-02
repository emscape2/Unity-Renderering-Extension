using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_SINGLE_Audio_Volume : Consequence
{
    AudioSource source;
    public new string tag;


    public override void Disengage()
    {
        source = GameObject.FindWithTag(tag).GetComponent<AudioSource>();
        if (source.mute == true)
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
