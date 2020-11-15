using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public UnityEngine.Renderer toDecativate;
    public AnimateMoveMenuConsequence toSlumber;
    public override void Disengage()
    {
        if (toToggle != null)
            toToggle.SetActive(!toToggle.activeSelf);
        if (toDecativate != null)
            toDecativate.enabled = !toDecativate.enabled;
        if (toSlumber != null)
            toSlumber.slumbering = !toSlumber.slumbering;
    }

    public override void Engage()
    {
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
