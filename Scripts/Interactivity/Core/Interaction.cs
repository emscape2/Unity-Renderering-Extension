using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour, IInteraction
{
    public bool engaged;
    public virtual bool? TryInteract(GameObject gameObject)
    {
        return false;
    }
}
