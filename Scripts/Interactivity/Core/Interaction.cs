using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    public virtual bool TryInteract(GameObject gameObject)
    {
        return false;
    }
}
