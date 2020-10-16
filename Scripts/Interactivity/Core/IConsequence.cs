using Assets.Scripts.Interactivity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsequence: IGUIllaume
{
    void Disengage();
    void Engage();
    bool CanEngage();
}
