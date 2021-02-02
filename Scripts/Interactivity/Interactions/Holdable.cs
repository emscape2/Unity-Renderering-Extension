using UnityEngine;

public class Holdable : Interaction
{

    public override bool? TryInteract(GameObject gameObject)
    {

        if (Input.GetMouseButton(0) && MouseBehavior.MouseOver(Input.mousePosition, gameObject) )
            return true;
        else
            return false;
    }

}
