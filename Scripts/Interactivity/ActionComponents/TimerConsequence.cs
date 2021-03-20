using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerConsequence : Consequence
{
    public float timeRemaining;
    private float initialTimeRemainder;

    //public bool timerIsRunning = false;
    public TextMeshPro timeText;
    public float delay;
    public float remainingDelay;
    public Color color;
    private void OnEnable()
    {
        if (initialTimeRemainder < 0.5f)
            initialTimeRemainder = timeRemaining;
        else
            timeRemaining = initialTimeRemainder;
        var globalVars = GlobalVars.getGlobalVars();
        var speed = globalVars.getVar("SPEED");
        globalVars.setVar("Pause", 1);
        switch (speed)
        {
            case 0:
                delay = 0.01f;
                break;
            case 1:
                delay = 0.01f;
                break;
            case 2:
                delay = 0.01f;
                break;
        }
        remainingDelay = 0.01f;//delay;
        globalVars.setVar("delayGlobal", (int)(delay*1000));
    }




    void FixedUpdate()
    {
        var position = -FindObjectOfType<SinusoidRendererComponent>()?.transform?.position;
            var globalVars = GlobalVars.getGlobalVars();
        
        if ( remainingDelay >= 0)
        {
            if (globalVars.getVar("Pause") == 0)
            {
                remainingDelay -= Time.deltaTime;
            }
            globalVars.setVar("delayGlobal", (int)(remainingDelay * 1000));
            timeRemaining = initialTimeRemainder;
            
            timeText.text = $"<#{ColorUtility.ToHtmlStringRGB(color)}> {(int)(remainingDelay + 1) }</color>";
            
        }
        else
        {
            globalVars.setVar("delayGlobal", -1);

            if (timeRemaining > 0)
            {
                timeRemaining = initialTimeRemainder - position?.x ?? 0f;

                timeText.text = DisplayTime(timeRemaining);
            }

            else if (globalVars.getVar("Pause") == 0)
            {
                globalVars.setVar("Pause", 1);
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                //timerIsRunning = false;
            }
        }
        if (position == null || globalVars.getVar("Pause") == 1)
        {
            timeRemaining = initialTimeRemainder - position?.x ?? 0f;
            timeText.text = $"<#{ColorUtility.ToHtmlStringRGB(color)}> {DisplayTime(timeRemaining)}</color>";


            if (remainingDelay <= 0)
            {
                globalVars.setVar("delayGlobal", (int)(remainingDelay * 1000));

                //remainingDelay = 3.99f;
            }
        }
    }


    string DisplayTime(float timeToDisplay)

    {

        timeToDisplay ++;

        int minutes = ((int)timeToDisplay / 60);
        int seconds = ((int)timeToDisplay % 60);

        return string.Format("{0}:{1:00}", minutes, seconds);

    }

    public override void Engage()
    {
        // do nothing
    }

    public override void Disengage()
    {
        OnEnable(); //force reset the counter

    }
}
