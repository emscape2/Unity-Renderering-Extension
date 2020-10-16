using Assets.Scripts.Interactivity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction: IGUIllaume
{
    string Name { get;  }
    bool? TryInteract(GameObject gameObject);
    
}

