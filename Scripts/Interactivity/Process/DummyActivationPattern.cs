using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DummyActivationPattern : MonoBehaviour, IActivationPattern
{
    public List<MonoBehaviour> Consequences { get => new List<MonoBehaviour>(); set => new List<MonoBehaviour>();  }

    public void Disengage(int i)
    {
    }

    public void Engage(int i)
    {
    }
}

