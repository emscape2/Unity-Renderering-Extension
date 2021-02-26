using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakeMaterialLookActivatedConsequence : Consequence
{
    [SerializeField]
    public Material ToBrighten;
    //<Sprite> ToBrighten;
    [SerializeField]
    public Color Lit;
    [SerializeField]
    public Color Unlit;
    private Color baseC;
    public bool onlyEngage;
    public string ColorName = "_Emission";
    [NonSerialized]
    public bool instantiate;
    public Texture2D iconHover;
    public override void Disengage()
    {
        engaged = false;

        if (!onlyEngage)
        {
            var textM = gameObject.GetComponent<TextMeshPro>();
            if (textM != null)
            {
                textM.color = Unlit;
            }
            else
            {
                if (ToBrighten != null)
                    ToBrighten.SetColor(ColorName, Unlit);
            }
            
            
                
            //if (sprite != null)
            //    sprite.color = Unlit;
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void FindMaterial()
    {
        if (ToBrighten == null)
        {
            var rend = gameObject.GetComponents<Renderer>();
            var image = gameObject.GetComponents<Graphic>();

            if (!instantiate)
            {
                ToBrighten = rend.FirstOrDefault()?.material ?? image.FirstOrDefault().material;
            }
            else
            {
                foreach (var instantiator in gameObject.GetComponents<MakeMaterialLookActivatedConsequence>())
                {
                    instantiator.instantiate = false;
                }
                if (rend.Any())
                {
                    ToBrighten = Instantiate(rend.First()?.material);
                    rend.First().material = ToBrighten;
                }
                else if (image.Any())
                {
                    ToBrighten = Instantiate(image.First()?.material);
                    image.First().material = ToBrighten;
                }
            }
        }

    }

    public override void Engage()
    {
        engaged = true;
        var textM = gameObject.GetComponent<TextMeshPro>();
        if (textM != null)
        {
            textM.color = Lit;
        }
        else
        {
            if (ToBrighten != null)
            {
                ToBrighten.SetColor(ColorName, Lit);
                if (DateTime.Today.Day == 25 && DateTime.Today.Month == 12)
                {
                    if (ToBrighten.HasProperty("_Shininess"))
                    {
                        ToBrighten.SetFloat("_Shiniess", 0.9f);
                    }
                }

            }
            if (iconHover != null)
            {
                Cursor.SetCursor(iconHover, Vector2.zero, CursorMode.Auto);
            }
        }


        // if (sprite != null)
        //   sprite.color = Lit;
    }

    public override bool CanEngage()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DateTime.Today.Day == 25 && DateTime.Today.Month == 12)
        {
         
                Unlit = new Color(Mathf.Sqrt(Unlit.r), Unlit.g - 0.1f, Unlit.b, Unlit.a * 1.2f);
         
        }
        FindMaterial();
        ToBrighten.SetColor(ColorName, Unlit);
        //Lit = Lit;
        //Unlit = baseC;// - (Unlit * baseC);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
