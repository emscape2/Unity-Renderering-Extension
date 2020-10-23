using UnityEngine;

[InteractionFlow(SideOption.Left)]
public interface IInteraction: IGUIllaume
{
    string Name { get;  }
    bool? TryInteract(GameObject gameObject);
    
}

