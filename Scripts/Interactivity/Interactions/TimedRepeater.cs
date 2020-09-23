using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Interactions/TimedRepeater")]
public class TimedRepeater : Interaction
{

    private double nextUp;
    private double nextDown;
    public float headstart;
    public double ratioUp, ratioDown;
    private bool started;

    private void OnEnable()
    {
        started = false;
    }




    public override bool TryInteract(GameObject gameObject)
    {
        double realbpm = gameObject.GetComponent<SinusoidRendererComponent>().realbpm;
        double secsforLoop = (60.0 / realbpm);

        if (!started)
        {
            started = true;
            ratioUp = ratioUp != 0 ? ratioUp :  gameObject.GetComponent<SinusoidRendererComponent>().ratioUp;//todo:  abstractify
            ratioDown = ratioDown != 0 ? ratioDown : gameObject.GetComponent<SinusoidRendererComponent>().ratioDown;
            double totalRatio = ratioDown + ratioUp;
            nextUp = -(ratioDown / totalRatio) * secsforLoop;
            nextUp += headstart;
            nextDown = -secsforLoop;
            nextDown += headstart;
            return true;
        }
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
        return false;
    }



}
