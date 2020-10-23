using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[InteractionFlow(SideOption.Right)]
public interface IConsequence: IGUIllaume
{
    void Disengage();
    void Engage();
    bool CanEngage();
}
