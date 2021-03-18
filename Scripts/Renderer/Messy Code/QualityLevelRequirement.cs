using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class QualityLevelRequirement : MonoBehaviour
{
    public List<MonoBehaviour> toDisable;
    public List<Transform> objectToDisable; 
#pragma warning disable CS0618 // Type or member is obsolete
    public QualityLevel min;
#pragma warning restore CS0618 // Type or member is obsolete
                              // Start is called before the first frame update
    void OnEnable()
    {
#pragma warning disable CS0618 // Type or member is obsolete
        if (QualitySettings.currentLevel < min)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            foreach(var component in toDisable)
            {
                component.enabled = false;
            }

            foreach (var component in objectToDisable)
            {
                component.gameObject.SetActive(false);
            }
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
