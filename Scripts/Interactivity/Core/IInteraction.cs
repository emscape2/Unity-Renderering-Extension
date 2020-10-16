using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{

    bool? TryInteract(GameObject gameObject);
    
}
