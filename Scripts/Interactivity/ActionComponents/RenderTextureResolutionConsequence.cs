using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureResolutionConsequence : Consequence
{
    public List<RenderTexture> texturesToManage;
    public override void Disengage()//werkt niet als in gebruik
    {
        texturesToManage.ForEach(t =>
       {
           t.antiAliasing = 1;
           t.format = RenderTextureFormat.ARGBHalf;
           if (t.height >= 768)
           {
               t.width /= 2;
               t.height /= 2;
           }


       });
    }

    public override void Engage()
    {
        texturesToManage.ForEach(t =>
        {
            t.antiAliasing = 2;
            if (t.height <= 2160)
            {
                t.width *= 2;
                t.height *= 2;
            }


        });
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}