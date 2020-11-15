using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimateMoveMenuConsequence : Consequence
{
    public Vector3 Direction;
    Vector3 Movement;
    public float linearSpeed;
    Vector3 origin;
    public bool activated;
    public bool slumbering;
    private bool gearingUp;
    public float exponent;
    public float ratio;
    private float slumberTjiem;
    public override void Disengage()
    {
        engaged = false;
        activated = !activated;
        slumberTjiem = 0;
        gearingUp = true;
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
        gameObject.GetComponent<MakeShiftXByX>()?.Start();
        Transform rectTransform = transform;
        origin = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, Direction.z);
        Movement = origin + new Vector3(Direction.x, Direction.y, 0);
        activated = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (gearingUp)
        {
            gearingUp = false;
            return;
        }
        Transform rectTransform = transform;
        var time = Time.deltaTime;
        
        if (slumbering)
        {
            slumberTjiem += Time.deltaTime;
            if (slumberTjiem< 0.9f)
            {
                time /= 1.0f - slumberTjiem;
            }
        }
        
        if (activated )
        {
            float distance = Vector2.Distance(rectTransform.localPosition, Movement);
            if (distance > linearSpeed * time * 2.0f)
            {
                Vector2 dir = ((Vector2)(Movement - rectTransform.localPosition)).normalized;
                dir *= 0.5f + Mathf.Pow(1.8f * distance / Direction.magnitude, exponent);

                rectTransform.localPosition += (Vector3)dir * time * linearSpeed; //new Vector3(dir.x * linearSpeed, dir.y * linearSpeed, 0.0f);
            }
            else
                rectTransform.localPosition = Movement;
        }
        else 
        {

            float distance = Vector2.Distance(rectTransform.localPosition, origin);
            if (distance > linearSpeed * time * 2.0f * ratio)
            {
                Vector2 dir = ((Vector2)(origin - rectTransform.localPosition)).normalized;
                dir *= 0.5f + Mathf.Pow(1.8f * distance / Direction.magnitude, exponent) * ratio;
                rectTransform.localPosition += (Vector3)dir * time * linearSpeed;
            }
            else
                rectTransform.localPosition = origin;
        }
    }
}


