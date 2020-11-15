using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class OnloadTrigger : Interaction
{
    bool triggered;
    public override bool? TryInteract(GameObject gameObject)
    {
        if (!triggered)
        {
            triggered = true;
            return true;
        }
        return true;
    }
}