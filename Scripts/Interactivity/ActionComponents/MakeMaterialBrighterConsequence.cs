﻿using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaterialBrighterConsequence : Consequence
{
    [SerializeField]
    public Material ToBrighten;
    public SpriteRenderer sprite;
    //<Sprite> ToBrighten;
    public Color Lit;
    public Color Unlit;
    public bool onlyEngage;
    public override void Disengage()
    {
        engaged = false;

        if (!onlyEngage)
        {
            if (ToBrighten != null)
                ToBrighten.color = Unlit;
            if (sprite != null)
                sprite.color = Unlit;
        }
    }

    public override void Engage()
    {
        engaged = true;
        if (ToBrighten != null)
            ToBrighten.color = Lit;
        if (sprite != null)
            sprite.color = Lit;
    }

    public override bool CanEngage()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
