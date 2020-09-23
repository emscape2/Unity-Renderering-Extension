using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using UnityEngine.Windows;


[RequireComponent(typeof(HorizontalLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class ButtonRowPivotScript : MonoBehaviour
{
    HorizontalLayoutGroup layout;
    RectTransform rectTransform;
    float xStart; 
    public int value ;
    // Start is called before the first frame update
    void Start()
    {
        layout = GetComponent<HorizontalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        Vector3 worldPoint = new Vector3();
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Vector2.zero, Camera.main, out worldPoint);
        //var coord = RectTransformUtility.ScreenPointToRay( Camera.main, Vector2.zero);
        //rectTransform.anchorMin = worldPoint;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, new Vector2(Camera.main.pixelWidth-1, 0), Camera.main, out worldPoint);
        //rectTransform.anchorMax= new Vector2(worldPoint.x, rectTransform.anchorMax.y);
        //rectTransform.ForceUpdateRectTransforms();
        xStart = rectTransform.localPosition.x;
        value = -6;
    }


    public void alterLayoutLeft(int value)
    {
        rectTransform.anchorMin += new Vector2(0.02f, 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( value < 20 &&   Time.realtimeSinceStartup - (float)value > 8.0f  )
        {
            value++;
            alterLayoutLeft(value);
        }
        //alterLayoutLeft(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono), rectTransform.anchorMin)); 
    }
}
