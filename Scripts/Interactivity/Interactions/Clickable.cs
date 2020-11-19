using System;
using System.Threading;
using UnityEngine;

public class Clickable : Interaction
{
    bool mouseDownLast, clicked;
    public static DateTime tijd;

    public override bool? TryInteract(GameObject gameObject)
    {
        if (tijd == null || DateTime.Now >= tijd)
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
                    {
                        tijd = DateTime.Now.AddMilliseconds(500);
                        return true;
                    }
                        
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
        else
        {
            return false;
        }
        





    }
}