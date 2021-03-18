using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnPauseRestartScene : Consequence
{
    public LoadSceneAsyncAsChildConsequence reloader;
    public int scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        if (global.getVar("Pause") == 1)
        {
            global.setVar("Pause", 0);
        }
        else
        {
            
        }
        StartCoroutine(reloadScene());
    }

    public IEnumerator reloadScene()
    {
        var task = SceneManager.UnloadSceneAsync(reloader.Scene);
        yield return new WaitUntil(() => task.isDone);
        reloader.loading = false;
        reloader.Engage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
