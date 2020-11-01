using UnityEngine;

public class Clickable : Interaction
{
    bool mouseDownLast, clicked;
    public override bool? TryInteract(GameObject gameObject)
    {

        //debug masks 
        if (Input.GetKeyDown(KeyCode.U))
        {
            MouseBehavior.InstantiateDrawRect(gameObject);
        }

        var mouseDownNow = Input.GetMouseButton(0);
        if (mouseDownLast && !mouseDownNow)
        {
            mouseDownLast = mouseDownNow;
            if (clicked)
            {
                clicked = false;
                if (MouseBehavior.MouseOver(Input.mousePosition, gameObject))
                    return true;
            }
            return false;
        }
        else if (!mouseDownLast && mouseDownNow)
        {
            mouseDownLast = mouseDownNow;
            if (MouseBehavior.MouseOver(Input.mousePosition, gameObject))
            {
                clicked = true;
            }
            return false;
        }
        mouseDownLast = mouseDownNow;
        if (!mouseDownLast)
            clicked = false;
        return false;



       
    }
}
