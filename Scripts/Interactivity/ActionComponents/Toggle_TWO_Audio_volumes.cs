using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_TWO_Audio_volumes : Consequence
{
    AudioSource source;
    public new string tag1;
    public new string tag2;


    public override void Disengage()
    {
        source = GameObject.FindWithTag(tag1).GetComponent<AudioSource>();
        if (source.mute == true)
        {
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
        source = GameObject.FindWithTag(tag2).GetComponent<AudioSource>();
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
