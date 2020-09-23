using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public override void Disengage()
    { 
    }

    public override void Engage()
    {
        toToggle.SetActive(!toToggle.activeSelf);
    }
    public override bool CanEngage()
    {
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
