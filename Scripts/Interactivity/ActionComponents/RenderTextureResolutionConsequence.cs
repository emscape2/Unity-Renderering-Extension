using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureResolutionConsequence : Consequence
{
    public List<RenderTexture> texturesToManage;
    public override void Disengage()//werkt niet als in gebruik
    {
        foreach (var t in texturesToManage)
        {
            if (t.IsCreated())
                continue;
            t.antiAliasing = 1;
            t.format = RenderTextureFormat.ARGBHalf;
            

            if (!t.useDynamicScale && t.height >= 769)
            {
                t.width /= 2;
                t.height /= 2;
            }


        } ;
    }

    public override void Engage()
    {
        foreach (var t in texturesToManage)
        {
            if (t.IsCreated())
                continue;
            t.antiAliasing = 2;

#if UNITY_IOS || UNITY_ANDROID

            if (!t.useDynamicScale && t.height <= 1080)
            {
                t.width *= 2;
                t.height *= 2;
            }
#else
            if (  t.height <= 2160)
            {
                t.width *= 2;
                t.height *= 2;

                t.antiAliasing = 4;
            }
#endif
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var t in texturesToManage)
        {
            if (t.IsCreated())
                continue;
            t.width = Screen.width;
            t.height = Screen.height;


        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}