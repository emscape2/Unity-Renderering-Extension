using Assets.Scripts.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Process
{
    class ReverterLogicReciever : MonoBehaviour, IActivationPattern, IConsequence
    {
        public List<MonoBehaviour> consequence;

        public List<MonoBehaviour> Consequences { get => consequence; set => consequence = value; }

        public bool CanEngage()
        {
            return true;
        }

        public void Disengage()
        {
            consequence.ForEach(c => ((IConsequence)c).Engage());
        }

        public void Disengage(int i)
        {
            Disengage();
        }

        public void Engage()
        {
            consequence.ForEach(c => ((IConsequence)c).Disengage());
        }

        public void Engage(int i)
        {
            Engage();
        }
    }
}
