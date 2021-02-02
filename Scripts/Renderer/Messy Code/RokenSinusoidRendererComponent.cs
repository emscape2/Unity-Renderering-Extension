using System;
using System.Collections.Generic;
using UnityEngine;

public class RokenSinusoidRendererComponent : SinusoidRendererComponent
{
    public float secWait = 2.0f;
    public float ampScale = 1.66f;
    protected override List<Vector2> GetPointsAfterEmielsZak()
    {

        var totalRatio = ratioUp + ratioDown;
        var Omhoog = Upwards_Graph(totalRatio);
        //var OmlaagInit = Downwards_Graph(totalRatio);//als deze voor eerste uitadem gebruikt wordt nmoet de standaard routine aangepast worden 
        var Omlaag = Downwards_Graph(totalRatio);
        var mr = ampScale;
        var amplitudeL = mr*amplitude;
        List<Vector2> pointsafterEmielsZak = new List<Vector2>();
        //eerst diep uitademen

        pointsafterEmielsZak.Add(new Vector2(Omhoog[0].x,amplitudeL));
        pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.20f), amplitudeL));
        pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.25f), amplitudeL));
        pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.25f), amplitudeL));
        pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.20f), amplitudeL));
        foreach ( var point in Omlaag)
        {
            pointsafterEmielsZak.Add(new Vector2(point.x + secWait, point.y *mr));
        }


        // standaard routine 3 diepe teugen
        for (double i = 0; i < 3; i+=1)
        {

            for (int j = 0; j < Omhoog.Count; j++)
            {
                pointsafterEmielsZak.Add(new Vector2((float)((i + 1.0)*secWait)+ (Omhoog[j].x + (
                    (float)(
                    (i + (ratioDown / totalRatio))
                    * 60.0 / realbpm)))
                    , Omhoog[j].y *mr ));
            }
            pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.20f), amplitudeL));
            pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.25f), amplitudeL));
            pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.25f), amplitudeL));
            pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x + (secWait * 0.20f), amplitudeL));
            for (int j = 0; j < Omlaag.Count; j++)
            {
                pointsafterEmielsZak.Add(new Vector2((float)(
                    ((2.0 + i )* secWait)  //the amount of waiting
                    +
                    Omlaag[j].x  // current displacement of omlaag

                    +
                    ((1.0 + i) * 60.0 / realbpm)//total laps elapsed in larger for loop, +1 is because the first breath ensures an extra round trip of the sinewave

                    ), Omlaag[j].y *mr ));

                
            }
        }
        pointsafterEmielsZak.Add(new Vector2(pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x+0.25f, -amplitudeL));

        float endX = pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x+ 9.5f;

        foreach (var point in Omhoog)
        {
            pointsafterEmielsZak.Add(new Vector2(point.x + endX, point.y * (1.0f + ((Math.Sign(point.y)-1.0f) * -0.25f* mr))));
        }
        endX = pointsafterEmielsZak[pointsafterEmielsZak.Count - 1].x+0.25f;
        var normal = base.GetPointsAfterEmielsZak();
        
           for (int j = 0; j < normal.Count; j++)
            {
                var point = normal[j];
                normal[j] = new Vector2(point.x + endX, point.y);
            }
        
        pointsafterEmielsZak.AddRange(normal);
        return pointsafterEmielsZak;
    }
}

/* pointsafterEmielsZak.Add(new Vector2((float)(
                    1.0 + (ratioDown / totalRatio) //pre forloop
                    +
                    1.0 + (ratioUp / totalRatio) //inside current larger loop
                    +
                    Omlaag[j].x  //inside current mini loop
                    - 1.0 + i //afwijking

                    +
                    ((1.0 + i) * 60.0 / realbpm)//total laps elapsed in larger for loop

                    ), Omlaag[j].y ));*/