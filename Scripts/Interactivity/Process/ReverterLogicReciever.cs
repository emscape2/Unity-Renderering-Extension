using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class ReverterLogicReciever : MonoBehaviour, IConsequence
    {
        public MonoBehaviour consequence;
        public bool CanEngage()
        {
            return true;
        }

        public void Disengage()
        {
            ((IConsequence)consequence)?.Engage();
        }

        public void Engage()
        {
            ((IConsequence)consequence)?.Disengage();
        }
    }
}
