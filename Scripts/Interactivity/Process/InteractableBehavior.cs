﻿using Assets.Scripts.Core;
using Assets.Scripts.Interactivity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableBehavior : MonoBehaviour, IConsequence
{
    public List<MonoBehaviour> consequences; //IConsequence
    public Interaction interaction;
    [SerializeField]
    bool engaged;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var consequence in consequences)
        {
            if ((IConsequence)(consequence) == null)
            {
                try
                {
                    consequence.Invoke("CanEngage", 0);//try if implemements members anyways
                }
                catch
                {
                    Debug.LogError($"Invalid consequence {consequence.name} in {this.name}");
                }
            }
        }
    }
    public void Engage()
    {
        foreach (var consequence in consequences)
        {
            (consequence as IConsequence).Engage();
        }
    }

    public void Disengage()
    {
        foreach (var consequence in consequences)
        {
            (consequence as IConsequence).Disengage();
        }
    }

    public bool CanEngage()
    {
        return true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //debug masks 
        if (Input.GetKeyDown(KeyCode.U))
        {
            MouseBehavior.InstantiateDrawRect(gameObject);
        }
        if (interaction == null)
            return;
        switch (interaction.TryInteract(gameObject))
        {
            case (true):
                engaged = true;
                Engage();
                break;
            case (false):
                engaged = false;
                Disengage();
                break;
            case (null):
                break;
        }
    }
}

