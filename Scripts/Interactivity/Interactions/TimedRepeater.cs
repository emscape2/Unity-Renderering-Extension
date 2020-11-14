using UnityEngine;

public class TimedRepeater : Interaction
{

    private double nextUp;
    private double nextDown;
    public float headstart;
    [SerializeField]
    public double realBPM;
    public double ratioUp, ratioDown;
    private bool started;

    private void OnEnable()
    {
        started = false;
    }




    public override bool? TryInteract(GameObject gameObject)
    {
        double realbpm = realBPM;// gameObject.GetComponent<SinusoidRendererComponent>()?.realbpm ?? 12.0;
        double secsforLoop = (60.0 / realbpm);

        if (!started)
        {
            started = true;
            ratioUp = ratioUp != 0 ? ratioUp :  gameObject.GetComponent<SinusoidRendererComponent>()?.ratioUp ?? 1.0;//todo:  abstractify
            ratioDown = ratioDown != 0 ? ratioDown : gameObject.GetComponent<SinusoidRendererComponent>()?.ratioDown ?? 1.0;
            double totalRatio = ratioDown + ratioUp;
            nextUp = -(ratioDown / totalRatio) * secsforLoop;
            nextUp += headstart;
            nextDown = -secsforLoop;
            nextDown += headstart;
            return false;
        }
        if (gameObject.GetComponent<SinusoidRendererComponent>() != null)
        {
            if (gameObject.transform.position.x <= nextUp)
            {
                nextUp -= secsforLoop;
                return true; //engage 
            }
            if (gameObject.transform.position.x <= nextDown)
            {
                nextDown -= secsforLoop;
                return false; //disengage
            }
            if (nextUp > nextDown)
                return true;
        }
        else
        {
            if (Time.realtimeSinceStartup <= nextUp)
            {
                nextUp -= secsforLoop;
                return true; //engage 
            }
            if (Time.realtimeSinceStartup <= nextDown)
            {
                nextDown -= secsforLoop;
                return false; //disengage
            }
            if (nextUp > nextDown)
                return true;
        }
        return false;
    }



}
