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
            
            if (frames < 16)
            {
                timings.Add(frames);
                iterations += 1;
                if (iterations == 3)
                {
                    iterations = -5;
                    timings.Clear();
                    frames = 0;
                    return false;
                }
            }
            else
            {
                iterations = 0;
                timings.Clear();
            }
            if (frames > 150)
            {
                frames = 0;
                return true;
            }
            frames = 0;
        }
        return null;
    }
}
