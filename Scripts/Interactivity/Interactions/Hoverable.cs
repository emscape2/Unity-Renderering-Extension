﻿using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverable : Interaction
{

    public override bool? TryInteract(GameObject gameObject)
    {

        if (MouseBehavior.MouseOver(Input.mousePosition, gameObject))
            return true;
        else
            return false;
    }

}
