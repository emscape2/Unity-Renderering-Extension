using UnityEngine;

public class EveryFrameRepeater : Interaction
{


    bool enabled = false;


    public override bool? TryInteract(GameObject gameObject)
    {
        return enabled = !enabled;
    }



}
