using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public UnityEngine.Renderer toDecativate;
    public AnimateMoveMenuConsequence toSlumber;
    public bool slumbering;
    public override void Disengage()
    {
        if (toToggle != null)
            toToggle.SetActive(!toToggle.activeSelf);
        if (toDecativate != null)
            toDecativate.enabled = !toDecativate.enabled;
        if (toSlumber != null)
        {

            toSlumber.slumbering = !slumbering;
            slumbering = toSlumber.slumbering;

        }
    }

    public override void Engage()
    {
        slumbering = toSlumber.slumbering;
        toSlumber.slumbering = true;
    }
    public override bool CanEngage()
    {
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        slumbering = toSlumber.slumbering;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
