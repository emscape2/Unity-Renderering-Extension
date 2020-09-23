using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPosition : MonoBehaviour
{

    double fakeBpm;
    float amplitude;
    float width;
    double totalLength;
    double detail;
    Vector2[] points;
    public Color colorLit;
    public Color colorUnlit;
    Color CurrentColor;
    
    // Start is called before the first frame update
    void Start()
    {

        fakeBpm = transform.parent.GetComponent<SinusoidRendererComponent>().fakeBpm * 0.1f;     ;
        amplitude   = transform.parent.GetComponent<SinusoidRendererComponent>().amplitude  ;
        width       = transform.parent.GetComponent<SinusoidRendererComponent>().width   ;
        totalLength = transform.parent.GetComponent<SinusoidRendererComponent>().totalLength     ;
        detail      = transform.parent.GetComponent<SinusoidRendererComponent>().detail     ;
        points      = transform.parent.GetComponent<SinusoidRendererComponent>().points;
        CurrentColor = colorUnlit;



    }

    // Update is called once per frame
    void Update()
    {

        var parentMesh = transform.parent.GetComponent<MeshRenderer>();
        var parentmeshTimeManipulation = (1.0f - (0.95f * Convert.ToInt32(!parentMesh.enabled)));
        var totalTime = Time.timeSinceLevelLoad* parentmeshTimeManipulation;
        float yPosLast = transform.localPosition.y;
        var yScaleLast = transform.localScale;
        double x = totalTime;
        double xPos = totalTime * fakeBpm * detail / 60.0;

        //todo: quick search implementeren
        Vector2 yLast = points[(int)xPos];
        Vector2 y = points[(int)xPos + 1];
        Vector2 yNext = points[(int)xPos + 2];
        for (int i = 0; i < points.Length-1; i++)
        {
            var current = points[i];
            if (current.x > x && (int)i > 0)
            {
                yLast = points[Mathf.Min(points.Length, i - 1)];
                y = points[i];
                yNext = points[i + 1];
                xPos = (x - yLast.x) / (y.x - yLast.x);
                
                break;
            }
        }

        var currentPosition = InterpolateY(x, (xPos- (int)xPos),  yLast, y);
        ///todo: kleuren berekenen aan de hand van de velocity hieronder
        transform.localPosition = new Vector3(parentMesh.enabled ? currentPosition.x  : (currentPosition.x - transform.parent.position.x)- (Camera.main.aspect * Camera.main.orthographicSize), currentPosition.y, transform.localPosition.z);
       Color color = colorUnlit;
        //{
        color = new Color(1f,1f,1f) /*+
            colorUnlit * 3.6f*( 
                Mathf.Abs
                    (
                        transform.localPosition.y*1.2f - yPosLast)
                    )
            +
            colorLit * 2.16f*(Mathf.Max(0, 1.1f*yPosLast - transform.localPosition.y));
        color *= Mathf.Max(0.1f,
                     Mathf.Pow(
                        Mathf.Abs                            
                        (
                        transform.localPosition.y- yPosLast
                       )
                    , 0.1f));*/

            ;
        // }
        this.GetComponent<MeshRenderer>().material.color = color;
        double velocityLast = (y - yLast).normalized.y;
        double velocityNext = (yNext - y).normalized.y;
        double velocityCurrent = xPos * velocityNext + (1.0 - xPos) * velocityLast;
        //transform.localScale = (float)(Math.Max(1-velocityCurrent,0.5) * 4 * width) * Vector3.one;
        transform.localScale = (float)width*2.0f //(float)(0.5*((( 0.5*transform.localPosition.y )/ amplitude ) +0.5)+ 2*width)            
                               *  Vector3.one;
        
            
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">X Position on graph</param>
    /// <param name="xLastMult">Percentage of the current point to be added to the last point</param>
    /// <param name="last">Last point</param>
    /// <param name="current">Current point</param>
    /// <returns></returns>
    Vector2 InterpolateY (double x, double xLastMult, Vector2 last, Vector2 current)
    {

        var mult = xLastMult;
        var resultY = 
            (last * (float)(1.0f-mult))
            + 
            (current * (float)(mult));
        //Debug.Log( $"Current: {current}. Last: {last}.  X: {x}. Mult: {mult}*{(current - last) }. Outcome: {result}.");
        return resultY;

    }
}
