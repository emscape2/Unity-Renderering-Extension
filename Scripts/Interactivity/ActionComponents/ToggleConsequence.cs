using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public UnityEngine.Renderer toDecativate;
    public AnimateMoveMenuConsequence toSlumber;
    public bool toggled = true, activated = true;
    public override void Disengage()
    {
        if (toToggle != null)
        {

            toToggle.SetActive(!toggled);
            toggled = toToggle.activeSelf;


        }
        if (toDecativate != null)
        {

            toDecativate.enabled = !activated;
            activated = toDecativate.enabled;

        }
        if (toSlumber != null)
            toSlumber.slumbering = !toSlumber.slumbering;
    }

    public override void Engage()
    {
        if (toToggle != null)
        {
            toggled = toToggle.activeSelf;

            toToggle.SetActive(false);
        }
        if (toDecativate != null)
        {
            activated = toDecativate.enabled;

            toDecativate.enabled = false;
        }
    }
    public override bool CanEngage()
    {
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (toToggle != null)
        {
            toggled = toToggle.activeSelf;
        }
        if(toDecativate != null)
        {
            activated = toDecativate.enabled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
