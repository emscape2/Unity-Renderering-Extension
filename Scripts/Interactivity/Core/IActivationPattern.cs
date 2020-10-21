using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public interface IActivationPattern: IGUIllaume
    {



        List<MonoBehaviour> Consequences { get; set; }

        void Engage(int i);
        void Disengage(int i);
    }
