using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMoveFluidly :Consequence
{
    public Vector3 Direction;
     Vector3 Movement;
    public float linearSpeed;
     Vector3 origin;
    public bool activated;

public override void Disengage()
{
        engaged = false;
        activated = !activated;
}

public override void Engage()
{
        engaged = true;
}

public override bool CanEngage()
{
    return true;
}

// Start is called before the first frame update
void Start()
    {
        RectTransform rectTransform = transform as RectTransform;
        origin = rectTransform.position;
        Movement = origin + Direction;
        activated = false;

    }


    // Update is called once per frame
    void Update()
{
        RectTransform rectTransform = transform as RectTransform;
        var time = Time.deltaTime;
        if (activated )
        {
            float distance = Vector2.Distance(rectTransform.position, Movement);
            if (distance > linearSpeed * time*2.0f)
            {
                Vector2 dir =((Vector2) (Movement - rectTransform.position)).normalized;
                dir *= 0.5f + (1.8f * distance / Direction.magnitude);

                rectTransform.position += (Vector3)dir * time* linearSpeed; //new Vector3(dir.x * linearSpeed, dir.y * linearSpeed, 0.0f);
            }
            else
                rectTransform.position = Movement;
        }
        else
        {

            float distance = Vector2.Distance(rectTransform.position, origin);
            if (distance > linearSpeed * time * 2.0f)
            {
                Vector2 dir = ((Vector2)(origin -rectTransform.position)).normalized;
                dir *= 0.5f + (1.8f * distance / Direction.magnitude);
                rectTransform.position += (Vector3)dir * time*linearSpeed;
            }
            else
                rectTransform.position = origin; 
        }
}
}


