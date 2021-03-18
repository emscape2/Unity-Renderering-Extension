using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : Consequence
{
    AudioSource source;
    public List<string> tagger;

    private void Start()
    {

    }

    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        global.setVar("Pause",  Convert.ToInt32((global.getVar("Pause")==0)));
		foreach(var tag in tagger)
		{
            foreach (var sourced in GameObject.FindGameObjectsWithTag(tag))
            {
                var source = sourced.GetComponent<AudioSource>();
                if (source.isPlaying)
                {
                    source.Pause();
                }
                else
                {
                    source.UnPause();
                } 
            }
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
