using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RotataConsequence : Consequence
{
    public bool toDisengage;
    public override bool CanEngage()
    {
        return true;
    }

    public override void Disengage()
    {
        if (toDisengage)
            engaged = false;
        Rotata();

    }

    public override void Engage()
    {
        Rotata();
        engaged = !engaged;
    }

    void Update()
    {
        Rotata();
    }
    private void Rotata()
    {
        if (engaged)
            transform.Rotate(Time.deltaTime * new Vector3(Random.Range(-12.0f, 22.0f), Random.Range(-11.0f, 21.5f), Random.Range(1.0f, 2.0f)));
    }
}
