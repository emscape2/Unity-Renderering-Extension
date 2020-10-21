﻿using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class ExplicitLogicReciever : MonoBehaviour, IInteraction, IConsequence
    {
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

        public void Disengage(int i)
        {
            Disengage();
        }

        public void Engage()
        {
            if (disengaged)
            {
                disengaged = false;
                engaged = true;
            }
        }

        public void Engage(int i)
        {
            Engage();
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
