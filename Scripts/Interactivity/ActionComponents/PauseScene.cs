using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : Consequence
{
    AudioSource source;
    public string tagger;

    private void Start()
    {

    }

    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        global.setVar("Pause",  Convert.ToInt32((global.getVar("Pause")==0)));
        source = GameObject.FindWithTag(tagger).GetComponent<AudioSource>();
        if (source.isPlaying)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
