using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_TWO_Audio_volumes : Consequence
{
    AudioSource source;
    public new string tag1;
    public new string tag2;

    private void Start()
    {
        MuteOnStartUp(tag1);
        MuteOnStartUp(tag2);

    }

    private void MuteOnStartUp(string tag)
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
        source = GameObject.FindWithTag(tag1)?.GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        if (source.mute == true)
        {
            source.mute = false;
            global.setVar(tag1 + "Audio", 0);
        }
        else
        {
            source.mute = true;
            global.setVar(tag1 + "Audio", 1);
        }
        source = GameObject.FindWithTag(tag2)?.GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        if (source.mute == true)
        {
            source.mute = false;
            global.setVar(tag2 + "Audio", 0);
        }
        else
        {
            source.mute = true;
            global.setVar(tag2+"Audio", 1);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
