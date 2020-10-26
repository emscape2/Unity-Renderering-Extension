using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMoveFluidly :Consequence
{
    public Vector3 Direction;
     Vector3 Movement;
        [SerializeField]
    public float linearSpeed;
        [SerializeField]
    public float exponent;
        [SerializeField]
    public float ratio;
    Vector3 origin;
        [NonSerialized]
    public bool activated;
    public bool returnOnDisengage;
public override void Disengage()
{
        if (returnOnDisengage)
        {
            activated = false;
            engaged = true;
        }
        else
        {
            
            engaged = false;
        }
}

public override void Engage()
{
        activated = !activated;

        engaged = true;
}

public override bool CanEngage()
{
    return true;
}

// Start is called before the first frame update
void Start()
    {
        Transform rectTransform = transform;
        origin = rectTransform.position;
        Movement = origin + Direction;
        activated = false;

    }


    // Update is called once per frame
    void Update()
{
        Transform rectTransform = transform ;
        var time = Time.deltaTime;
        if (activated )
        {
            float distance = Vector2.Distance(rectTransform.position, Movement);
            if (distance > linearSpeed * time*2.0f)
            {
                Vector2 dir =((Vector2) (Movement - rectTransform.position)).normalized;
                dir *= 0.5f + Mathf.Pow(1.8f * distance / Direction.magnitude,exponent);
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
                dir *= 0.5f + Mathf.Pow(1.8f * distance / Direction.magnitude,exponent) *ratio;
                rectTransform.position += (Vector3)dir * time*linearSpeed;
            }
            else
                rectTransform.position = origin; 
        }
}
}


