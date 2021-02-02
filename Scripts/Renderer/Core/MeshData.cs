using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Coding.Renderer
{
    class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<Vector2> newUv = new List<Vector2>();
        public List<int> triangles = new List<int>();
        public bool makeBezier;
        public bool opticalPositioning = true;
        public MeshData(List<Vector2> meshData, float width, bool optiallyOptimized = true)
        {
            opticalPositioning = optiallyOptimized;
            makeBezier = ! optiallyOptimized;
            var ending = new Vector2(Mathf.Ceil(meshData.Last().x), meshData.First().y);
            meshData.Add(ending);
            meshData.Add(ending + Vector2.right);
            if (makeBezier)
            {
                var meshdatacopy = new List<Vector2>(meshData);
                meshData = setInterval(meshdatacopy.ToArray());

            }
            int i = 0;
            Vector2 lastPoint;
            Vector2 sizeVector;
            float halfWidth = width / 2;
            var lenMinOne = meshData.Count - 1;
            while (i < lenMinOne)
            {
                var notFirst = i > 0;
                var currentPoint = meshData[Math.Min(i, lenMinOne)];

                if (notFirst)
                {
                    lastPoint = meshData[i - 1];
                    var nextPoint = meshData[Math.Min(i + 1, lenMinOne)];
                    sizeVector = AddMeshDataForPoint(i, lastPoint, halfWidth, currentPoint, nextPoint);
                    i++;

                    newUv.Add(new Vector2(0, 0));
                    newUv.Add(new Vector2(1, 1));
                }
                else
                {
                    lastPoint = meshData[0] + Vector2.left;



                    vertices.Add(currentPoint + new Vector2(0, halfWidth));
                    vertices.Add(currentPoint - new Vector2(0, halfWidth));
                    i++;

                    newUv.Add(new Vector2(0, 0));
                    newUv.Add(new Vector2(1, 1));
                }




            }

            var d = 1.0;


        }
        List<Vector2> setInterval(Vector2[] controlPoints)
        {
            var answer = new List<Vector2>();
            var last = controlPoints.First() + Vector2.left ;
            int nodeIndex = 0;
            try
            {
                int numbreaks = 3;
                for (int i = 0; i < controlPoints.Length * numbreaks; i++)
                {
                    float t = (i % (numbreaks * (2))) / ((float)numbreaks * 2.0f);
                    Vector2 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex],
                        controlPoints[nodeIndex + 1], controlPoints[nodeIndex + 2]);
                    if (pixel.x > last.x + 0.02f)
                    {
                        answer.Add(pixel);
                        last = pixel;
                    }
                    if ((i % (2 * numbreaks)) == ((2 * numbreaks) - 1))
                        nodeIndex += 1;
                }
            }
            catch { }
            return answer;
        }

        private Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector2 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;

            return p;
        }

        private Vector2 AddMeshDataForPoint(int i, Vector2 lastPoint, float halfWidth, Vector2 currentPoint, Vector2 nextPoint)
        {
            Vector2 sizeVector;

            var sizeVectorMax = (nextPoint - currentPoint).normalized;
            var sizeVectorMin = (currentPoint - lastPoint).normalized;
            var sizeVectorTurned = (sizeVectorMin + sizeVectorMax) * halfWidth;



            sizeVector = new Vector2(-sizeVectorTurned.y, sizeVectorTurned.x); //90 graden draaien

            Vector2 sizevectorstraigtened = Vector3.zero;
            if (opticalPositioning)
                sizevectorstraigtened = sizeVectorTurned.x != 0.0f ? (sizeVector.x / sizeVectorTurned.x) * sizeVectorTurned : sizeVector;


            var sizevectorDown = sizeVector - (sizevectorstraigtened);
            var sizevectorUp = (Vector2.zero - sizeVector) + (sizevectorstraigtened);


            // schetst directie orthagonaal op het verloop van de grafiek
            //maakt deze 1 lang
            //obvious
            if (sizeVector.y < 0)

            {


                vertices.Add(currentPoint - sizevectorDown);
                vertices.Add(currentPoint - sizevectorUp);
            }
            else
            {


                vertices.Add(currentPoint - sizevectorUp);
                vertices.Add(currentPoint - sizevectorDown);
            }

            int mark1 = (i - 1) * 2;
            int mark2 = (i) * 2;

            triangles.AddRange(new List<int>()
                   {
                        mark1, mark2, mark2+1,
                        mark2+1, mark1+1, mark1
                   });
            return sizeVector;
        }
    }

    /*  
            sizeVector = new Vector2(-sizeVectorTurned.y, sizeVectorTurned.x); //90 graden draaien

            var sizevectorDown = sizeVector - ((sizeVector.x / sizeVectorMin.x) * sizeVectorMin);
            var sizevectorUp = (Vector2.zero - sizeVector) - ((sizeVector.x / sizeVectorMax.x) * sizeVectorMax);


            // schetst directie orthagonaal op het verloop van de grafiek
            //maakt deze 1 lang
            //obvious
            if (sizeVector.y < 0)

            {


                vertices.Add(currentPoint + sizevectorUp);
                vertices.Add(currentPoint + sizevectorDown);
            }
            else
            {


                vertices.Add(currentPoint + sizevectorDown);
                vertices.Add(currentPoint + sizevectorUp);
            }*/
}
