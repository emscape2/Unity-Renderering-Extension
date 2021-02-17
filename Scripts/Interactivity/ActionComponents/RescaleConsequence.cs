using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.ActionComponents
{
    [DisallowMultipleComponent]
    public class RescaleConsequence : Consequence
    {
        bool triggered;
        public override void Engage()
        {
            if (! triggered)
            {
                gameObject.transform.localScale /= 2.0f;
                triggered = true;
            }
        }


    }
}
