using System.Collections.Generic;
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

