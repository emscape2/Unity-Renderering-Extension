using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    string Name { get;  }
    bool? TryInteract(GameObject gameObject);
    
}

