using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Interactions/EveryFrameRepeater")]
public class EveryFrameRepeater : Interaction
{


    bool enabled = false;


    public override bool TryInteract(GameObject gameObject)
    {
        return enabled = !enabled;
    }



}
