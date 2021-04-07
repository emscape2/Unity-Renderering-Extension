using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageConsequence : Consequence
{
    public Sprite PauseImage;
    public Sprite PlayImage;
    public GameObject gameobject;
    private SpriteRenderer renderer;
    private Sprite Used;

    public override void Disengage()
    {
        renderer = gameobject.GetComponent<SpriteRenderer>();
        Used = renderer.sprite;


        
        
        if (GlobalVars.getGlobalVars().getVar("Pause") == 1)//Used == PauseImage)
            gameobject.GetComponent<SpriteRenderer>().sprite = PlayImage;
        else
            gameobject.GetComponent<SpriteRenderer>().sprite = PauseImage;
    }
}
