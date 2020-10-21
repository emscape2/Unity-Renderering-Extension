using System.Collections.Generic;
using UnityEngine;

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
