using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class OnloadTrigger : Interaction
{
    bool triggered;
    public bool timedDelay;
    public float delaySeconds;
    public override bool? TryInteract(GameObject gameObject)
    {
        
        if (timedDelay)
        {
            delaySeconds -= Time.deltaTime;
            if (delaySeconds <= 0)
            {
                timedDelay = false;
                
            }
            else
            {
                return null;
            }
        }

        if (!triggered)
        {
            triggered = true;
            return true;
        }
        return true;
    }
}