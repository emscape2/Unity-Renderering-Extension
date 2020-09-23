using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


namespace Assets.Coding.Renderer
{
     public class Sinusoid : I_Graph

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="realBPM">amount of cycles per minute</param>
        /// <param name="amplitude">vertical size of the sinusoid</param>
        /// <param name="detail">amount of detail measured in iterations per cycle </param>
        /// <param name="start">startpositie van de sinusoïde </param>
        /// <returns></returns>
        public override List<Vector2> Points(double realBPM, float amplitude, double detail, double start)
        {
            realBPM = realBPM / 60.0f;
            var result = new List<Vector2>();

            for (int i = 0; i < detail; i ++)
            {
                double pos = ((double)i)/detail;
                result.Add(new Vector2((float)((pos) / realBPM), amplitude * Mathf.Cos((float)((pos + (2*start))*Math.PI))));
            }
            return result;
        }

    }
}
