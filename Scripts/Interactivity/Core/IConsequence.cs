using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsequence
{
    void Disengage();
    void Engage();
    bool CanEngage();
}
