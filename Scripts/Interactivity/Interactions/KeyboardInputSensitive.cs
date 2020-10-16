using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "Interactions/KeyboardInputSensitive")]
public class KeyboardInputSensitive : Interaction
{
    [SerializeField]
    public KeyCode KeyCode; 

    public override bool? TryInteract(GameObject gameObject)
    {

        if (Input.GetKey(KeyCode))
            return engaged = true;
        else if (engaged)
            return engaged = false;
        else
            return null;
    }

}

