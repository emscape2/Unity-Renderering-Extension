using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotataConsequence : Consequence
{
    public override bool CanEngage()
    {
        return true;
    }

    public override void Disengage()
    {
        Rotata();
    }

    public override void Engage()
    {
        Rotata();
    }


    private void Rotata()
    {
        var random = new Random();
        transform.Rotate(Time.deltaTime * new Vector3(Random.Range(-3.0f, 2.0f), Random.Range(-1.0f, 1.5f), Random.Range(1.0f, 2.0f)));
    }
}
