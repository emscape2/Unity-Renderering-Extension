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
        [RightInteraction(typeof(List<IConsequence>), "Consequences")]
        List <MonoBehaviour> Consequences { get; set; }

        void Engage(int i);
        void Disengage(int i);
    }
