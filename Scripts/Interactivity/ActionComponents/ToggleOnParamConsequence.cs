using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnParamConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public int Value;
    public string Param;
    public bool delay;

    public override void Disengage()
    {
        var global = GlobalVars.getGlobalVars();
        var waardeSpeed = global.getVar(Param);
        if (waardeSpeed == Value)
        {
            toToggle.SetActive(true);
        }
        else
        {
            toToggle.SetActive(false);
        }
    }
}