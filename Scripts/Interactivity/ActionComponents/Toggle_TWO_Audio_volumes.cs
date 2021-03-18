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
        GameObject[] _gameObjects;

        _gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (_gameObjects == null)
        {
            return;
        }
        var waarde = global.getVar(tag + "Audio");
        if (waarde == 1)
        {
            foreach(GameObject _gameObject in _gameObjects)
            {
                var source = _gameObject.GetComponent<AudioSource>();
                source.mute = true;
            }
        }
        else
        {
            foreach (GameObject _gameObject in _gameObjects)
            {
                var source = _gameObject.GetComponent<AudioSource>();
                source.mute = false;
            }
        }
    }

    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        foreach (var tag1 in tags)
        {
            var _gameObjects = GameObject.FindGameObjectsWithTag(tag1);
            if (global.getVar(tag1 + "Audio") == 0)
                global.setVar(tag1 + "Audio", 1);
            else
                global.setVar(tag1 + "Audio", 0);

            foreach (GameObject _gameObject in _gameObjects)
            {
                var source = _gameObject.GetComponent<AudioSource>();
                ProcessAudioChanges(source);
            } 
        }


       

    }

    private void ProcessAudioChanges( AudioSource source)
    {
        var global = GlobalVars.getGlobalVars();
        if (source == null)
        {
            return;
        }
        if (global.getVar(source.tag + "Audio") == 0)
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
