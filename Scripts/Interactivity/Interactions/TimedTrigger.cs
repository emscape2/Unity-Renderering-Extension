using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTrigger : Interaction
{
    bool triggered;
    public bool disengage;
    public float timedDelay;
    private bool started;

    public override bool? TryInteract(GameObject gameObject)
    {
        if (this.gameObject.transform.position.x > -0.01)
        {
            started = false;
        }
        
        if (!started)
        {
            if (this.gameObject.transform.position.x > -timedDelay)
            {
                return null;
            }
            started = true;
        }

        if (!triggered)
        {
            triggered = true;
            return true;
        }
        if (disengage) return false;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
