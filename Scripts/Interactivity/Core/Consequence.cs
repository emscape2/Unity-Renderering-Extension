using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consequence : MonoBehaviour, IConsequence
{
    //[SerializeField]
    protected bool engaged;
    public virtual void Engage()
    {
        Debug.Log("Engaged");
        engaged = true;
    }
    public virtual void Disengage()
    {
        Debug.Log("DisEngage");
        engaged = false;
    }
    public virtual bool CanEngage()
    {
        Debug.Log("CanEngage");
        return true;

    }

}
