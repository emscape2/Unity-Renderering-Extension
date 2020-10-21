using UnityEngine;

public interface IInteraction: IGUIllaume
{
    string Name { get;  }
    bool? TryInteract(GameObject gameObject);
    
}

