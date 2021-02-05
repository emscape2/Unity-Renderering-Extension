 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityAdjusterConsequence : Consequence
{

    public override void Disengage()
    {
        QualitySettings.DecreaseLevel();
    }

    
    public override void Engage()
    {
        QualitySettings.IncreaseLevel();
    }
}
