using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Interactions
{
    public class BooleanGlobalVarInteration : Interaction
    {
        public string varName;
        public bool on;

        public override bool? TryInteract(GameObject gameObject)
        {
            var globalvar = GlobalVars.getGlobalVars().getVar(varName);
            var lastOn = on;
            on = (globalvar != 0);
            var changed = on != lastOn;
            return changed;
        }
    }
}
