using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleOnPageNumberConsequence : Consequence
{
    public UnityEngine.GameObject toToggle;
    public TMP_Text textcomponent;
    public int maxPages;
    private int CurrentPage;
    public bool delay;
    public bool PageUp_bool;

    public override void Disengage()
    {
        CurrentPage = textcomponent.pageToDisplay;
        if (PageUp_bool)
        {
            if (CurrentPage != maxPages)
            {
                toToggle.SetActive(true);
            }
            else
            {
                toToggle.SetActive(false);
            }
        }
        else
        {
            if (CurrentPage != 1)
            {
                toToggle.SetActive(true);
            }

            else
            {
                toToggle.SetActive(false);
            }
        }
    }
}

