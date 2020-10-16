using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class RewireLogicReciever : MonoBehaviour, IInteraction, IConsequence
    {
        public MonoBehaviour consequence;
        bool engaged;
        bool disengaged;
        public string Name { get { return gameObject.name; } }


        public bool CanEngage()
        {
            return true;
        }

        public void Disengage()
        {
            disengaged = true;
        }

        public void Engage()
        {
            disengaged = false;
            engaged = true;
        }

        public bool? TryInteract(GameObject gameObject)
        {
            if (engaged)
            {
                engaged = false;
                disengaged = false;
                return true; 
            }
            if (disengaged)
            {
                disengaged = false;
                return false;
            }    
            return null;
        }

    }
}
