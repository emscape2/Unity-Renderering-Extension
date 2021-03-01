using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle_TWO_Audio_volumes : Consequence
{
    public List<string> tags;

    private void Start()
    {
        foreach(var tag1 in tags)
            MuteOnStartUp(tag1);

    }

    private void MuteOnStartUp(string tag)
    {
        var global = GlobalVars.getGlobalVars();
        AudioSource source;

        source = GameObject.FindWithTag(tag)?.GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        var waarde = global.getVar(tag + "Audio");
        if (waarde == 1)
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
        foreach (var tag1 in tags)
        {
            var source = GameObject.FindWithTag(tag1)?.GetComponent<AudioSource>();

            ProcessAudioChanges(source);
        }

       

    }

    private void ProcessAudioChanges( AudioSource source)
    {
        var global = GlobalVars.getGlobalVars();
        if (source == null)
        {
            return;
        }
        if (global.getVar(source.tag + "Audio") == 1)
        {
            source.mute = false;
            global.setVar(source.tag + "Audio", 0);
        }
        else
        {
            source.mute = true;
            global.setVar(source.tag + "Audio", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
