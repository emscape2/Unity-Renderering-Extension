using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedRepeaterDelayStart : Interaction
{

    [SerializeField]
    private double nextUp;
    [SerializeField]
    private double nextDown;
    public float headstart;
    public float delay = 3.0f;

    public double realBPM;
    public double ratioUp, ratioDown;
    private bool started;

    private void OnEnable()
    {
        started = false;
    }

    public override bool? TryInteract(GameObject gameObject)
    {
        if (!gameObject.GetComponent<SinusoidRendererComponent>()._started)
            return null;
        
        
            var sin = gameObject.GetComponent<SinusoidRendererComponent>();
            realBPM = sin?.realbpm ?? realBPM;
        double secsforLoop = (60.0 / realBPM);

        if (!started)
        {

            if (Mathf.Abs(gameObject.transform.position.x) < delay)
            {
                return null;
            }
            started = true;
            ratioUp = /*ratioUp != 0 ? ratioUp :*/ sin?.ratioUp ?? ratioUp;//todo:  abstractify
            ratioDown = /*ratioDown != 0 ? ratioDown :*/ sin?.ratioDown ?? ratioDown;
            double totalRatio = ratioDown + ratioUp;
            nextUp = -secsforLoop;

            if (gameObject.GetComponent<RokenSinusoidRendererComponent>() != null)
            {
                nextUp = -delay + secsforLoop;
                nextUp  -= (ratioDown / totalRatio) * secsforLoop;
            }
        
            nextUp += headstart ;
            nextDown = 0;
            if (gameObject.GetComponent<RokenSinusoidRendererComponent>() != null)
            {
                nextDown = -delay + secsforLoop;

            }
            else
            { 
                nextDown -=(ratioUp / totalRatio) * secsforLoop;
            }
            nextDown += headstart;
            return false;
        }
        if (gameObject.GetComponent<SinusoidRendererComponent>() != null)
        {
            if (gameObject.transform.position.x <= nextUp)
            {
                Debug.Log($"Disengaged {name} , timestamp: " + (-sin.transform.position.x).ToString(System.Globalization.CultureInfo.InstalledUICulture.DateTimeFormat));
                while (gameObject.transform.position.x <= nextUp)
                    nextUp -= secsforLoop;
                return false; //disengage 
            }
            if (gameObject.transform.position.x <= nextDown)
            {
                Debug.Log($"Engaged {name} , timestamp: " + (-sin.transform.position.x).ToString(System.Globalization.CultureInfo.InstalledUICulture.DateTimeFormat));
                while (gameObject.transform.position.x <= nextDown)
                    nextDown -= secsforLoop;
                return true; //engage
            }
            if (nextUp > nextDown)
                return false;
        }
        return true;
    }



}
