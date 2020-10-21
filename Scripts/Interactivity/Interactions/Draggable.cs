using UnityEngine;

public class Draggable : Interaction
{
    bool isDown;
    float timeDown, timeUp;

    public override bool? TryInteract(GameObject gameObject)
    {

        if ((MouseBehavior.MouseOver(Input.mousePosition, gameObject)|| isDown) && Input.GetMouseButton(0))
            return HandleDown();
        else
            return HandleUp();
    }

    private bool HandleDown()
    {
        if (!isDown)
        {
            isDown = true;
            timeDown = 0.0f;
        }
        else
        {
            timeDown += Time.deltaTime;
        }
        timeUp = 0.0f;
        if (timeDown > 0.4f)
        {
            return true;
        }
        return false;
    }

    private bool HandleUp()
    {
        timeUp += Time.deltaTime;
        if (timeUp > 0.22f)
        {
            isDown = false;
            timeDown = 0.0f;
            timeUp = 0;
        }
        return false;
    }

}
