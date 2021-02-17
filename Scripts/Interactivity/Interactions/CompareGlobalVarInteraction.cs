using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Interactions
{
    public class CompareGlobalVarInteraction : Interaction
    {
        public int LargerThanVar, SmallerThanVar;
        public bool CompareLargerThanVar, CompareSmallerThanVar;
        public string varName;
        public override bool? TryInteract(GameObject gameObject)
        {
            var globalvar = GlobalVars.getGlobalVars().getVar(varName);
            return ((globalvar > (CompareLargerThanVar ? LargerThanVar : int.MinValue)) 
                && (globalvar < (CompareSmallerThanVar ? SmallerThanVar : int.MaxValue)));
        }
    }
}
