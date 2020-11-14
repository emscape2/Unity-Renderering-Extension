using System;
using UnityEngine;

public class RokenSinusoidRenderComponent : SinusoidRendererComponent
{
    protected override List<Vector2> GetPointsAfterEmielsZak()
    {

        var totalRatio = ratioUp + ratioDown;
        var Omhoog = Upwards_Graph(totalRatio);
        var Omlaag = Downwards_Graph(totalRatio);
        List<Vector2> pointsafterEmielsZak = new List<Vector2>();

        pointsafterEmielsZak.Add(Omlaag[0]);
        for (double i = 0; i < (totalLength * realbpm); i += 1)
        {
            for (int j = 0; j < Omlaag.Count; j++)
            {
                pointsafterEmielsZak.Add(new Vector2(((Omlaag[j].x +
                        (float)(i * 60.0 / realbpm)))
                    , Omlaag[j].y));
            }

            for (int j = 0; j < Omhoog.Count; j++)
            {
                pointsafterEmielsZak.Add(new Vector2((Omhoog[j].x + (
                    (float)(
                    (i + (ratioDown / totalRatio))
                    * 60.0 / realbpm)))
                    , Omhoog[j].y));
            }
        }
        return pointsafterEmielsZak;
    }
}