using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactivity.Core
{
    public interface IActivationPattern: IGUIllaume
    {
       

        void Engage(int i);
        void Disengage(int i);
    }
}
