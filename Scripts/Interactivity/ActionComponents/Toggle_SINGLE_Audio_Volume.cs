using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interactivity.ActionComponents;

public class Toggle_SINGLE_Audio_Volume : Consequence
{
    AudioSource source;
    public new string tag;

    private void Start()
    {
        var global = GlobalVars.getGlobalVars();
        source = GameObject.FindWithTag(tag)?.GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
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


    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        source = GameObject.FindWithTag(tag)?.GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        if (source.mute == true)
        {
            source.mute = false;
            global.setVar(tag + "Audio", 0);
        }
        else
        {
            source.mute = true;
            global.setVar(tag + "Audio", 1);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
