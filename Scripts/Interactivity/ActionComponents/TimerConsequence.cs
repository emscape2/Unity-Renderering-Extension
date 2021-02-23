using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerConsequence : MonoBehaviour
{
    public float timeRemaining;
    private float initialTimeRemainder;
    public bool timerIsRunning = false;
    public TextMeshPro timeText;
    public float delay;

    private void Start()
    {
        initialTimeRemainder = timeRemaining;
        var speed = GlobalVars.getGlobalVars().getVar("SPEED");
        switch (speed)
        {
            case 0:
                delay = 6.5f;
                break;
            case 1:
                delay = 6;
                break;
            case 2:
                delay = 5.5f;
                break;
        }
    }

    void Update()
    {
        var position = -FindObjectOfType<SinusoidRendererComponent>()?.transform?.position;
        if (delay > position?.x)
        {
            timeRemaining = initialTimeRemainder;
            if (position == null)
            {
                timeText.text = $"Ready?";
            }
            else
            {
                timeText.text = $"{(int)(delay + 1 - position?.x) }";
            }
        }
        else
        {
            if (timeRemaining > 0)
            {
                timeRemaining = initialTimeRemainder - position?.x ?? 0f;

                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    
}
