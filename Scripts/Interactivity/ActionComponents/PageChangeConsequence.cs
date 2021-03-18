using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageChangeConsequence : Consequence
{
    public TMP_Text textcomponent;
    public int maxPages;
    public bool PageUp_bool;
    private int CurrentPage;


    public override void Disengage()
    {
        CurrentPage = textcomponent.pageToDisplay;
        if (PageUp_bool)
        {
            if (CurrentPage != maxPages)
            {
                CurrentPage += 1;
                textcomponent.pageToDisplay = CurrentPage;
            }
        }
        else
        {
            if (CurrentPage != 1)
            {
                CurrentPage -= 1;
                textcomponent.pageToDisplay = CurrentPage;
            }
        }


    }

}
