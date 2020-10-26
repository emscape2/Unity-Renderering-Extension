using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteraction
{
    [NonSerialized]
    public bool engaged;

    public string Name { get { return gameObject.name; }  }

    public virtual bool? TryInteract(GameObject gameObject)
    {
        return false;
    }
}
