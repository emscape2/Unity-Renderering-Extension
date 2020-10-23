using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[InteractionFlow(SideOption.BothSides)]
    public interface IActivationPattern: IGUIllaume
    {


        [SerializeField]
        [LeftInteraction(typeof(IConsequence))]
        List<MonoBehaviour> Consequences { get; set; }

        void Engage(int i);
        void Disengage(int i);
    }
