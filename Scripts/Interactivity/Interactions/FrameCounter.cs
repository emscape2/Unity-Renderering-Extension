using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCounter : Interaction
{
    private List<float> timings = new List<float>();
    private int frames;
    private float elapsed;
    private int iterations;
 
    private void Start()
        {
        frames = 0;
        elapsed = 0;
        }

    public override bool? TryInteract(GameObject gameObject)
    {
        ++frames;
        elapsed += Time.deltaTime;

        if (elapsed >= 1.0f)
        {
            elapsed -= 1.0f;
            timings.Add(frames);
            
            if (frames < 20)
            {
                iterations += 1;
                if (iterations == 3)
                {
                    iterations = -5;
                    timings.Clear();
                    frames = 0;
                    return true;
                }
            }
            else
            {
                iterations = 0;
                timings.Clear();
            }
            frames = 0;
        }
        return false;
    }
}
