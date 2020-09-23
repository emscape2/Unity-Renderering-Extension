using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Core
{
    class MouseBehavior
    {

        public static bool MouseOver(Vector2 pos, GameObject gameObject)
        {
            // var rect = rectSurface();
            var guillaumeCam = Camera.allCameras.Where(c => c.gameObject.layer == 29).FirstOrDefault();//eerste guillaume camera 
            if (guillaumeCam == null)
            {
                return false;
            }
            else
            {
                // var ray = (Vector2)rect.InverseTransformPoint(guillaumeCam.ScreenPointToRay(pos).origin);
                var ray = guillaumeCam.ScreenPointToRay(pos);
                if (RectSurface(ray, gameObject))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool RectSurface(Ray ray, GameObject gameObject)
        {

            RectTransform PotentialComponent1;
            if (!gameObject.TryGetComponent<RectTransform>(out PotentialComponent1))
            {
                SpriteRenderer PotentialComponent2;
                    
                if (!gameObject.TryGetComponent<SpriteRenderer>(out PotentialComponent2))
                {
                    Collider collider;                   
                    
                        if (!gameObject.TryGetComponent<Collider>(out collider))
                        {
                            Debug.LogWarning("no collada attaché");
                            return false;
                        }
                        else
                            return Intersects(ray.origin, collider.bounds);
                    
                }
                else
                {
                    var answer = Intersects(ray.origin, PotentialComponent2.bounds);

                    return answer;
                }
            }
            else //(PotentialComponent1 != null)
            {
                //var Mask2d = gameObject.GetComponent<UnityEngine.UI.RectMask2D>();

                var rect = PotentialComponent1.rect;

                var pivotCor = ((Vector3)PotentialComponent1.pivot - new Vector3(0.5f, 0.5f, 0));
                pivotCor.Scale(PotentialComponent1.rect.size / PotentialComponent1.localScale);

                var pos = PotentialComponent1.transform.position - pivotCor;// + (0.5f *PotentialComponent1.localScale);

                rect.size =( rect.size * new Vector2(PotentialComponent1.transform.lossyScale.x, PotentialComponent1.transform.lossyScale.y))/ PotentialComponent1.localScale;
                rect.position = (Vector2)pos-0.5f*rect.size;
                
                
                
                var answer = Intersects(ray.origin, rect  );
                return answer;
            }
            return false;
        }

        public static bool Intersects(Vector2 pos, Rect rect)
        {
            return Intersects(pos, rect.x, rect.x + rect.width, rect.y, rect.y + rect.height);
        }

        public static bool Intersects(Vector2 pos, Bounds bounds)
        {
            return Intersects(pos, bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y);
        }

        public static bool Intersects(Vector2 pos, float xMin, float xMax, float yMin, float yMax)
        {
            if ( pos.x > xMin && pos.x < xMax && pos.y > yMin && pos.y < yMax)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// for debug purposes only
        /// </summary>
        /// <param name="gameObject"></param>
        internal static void InstantiateDrawRect(GameObject gameObject)
        {
            RectTransform PotentialComponent1 = gameObject.GetComponent<RectTransform>();//todo:Uitbreiden
            Vector3 pos = Vector3.zero;
            Vector3 scale = Vector3.one;
            quaternion rotation = quaternion.identity;
            Vector3 pivotCor = Vector3.zero;
            if (PotentialComponent1 == null)
            {
                SpriteRenderer PotentialComponent2 = gameObject.GetComponent<SpriteRenderer>();
                if (PotentialComponent2 == null)
                {
                    Collider collider = gameObject.GetComponent<Collider>();
                    {
                        pos = collider.bounds.center; //- collider.bounds.extents;
                        rotation = collider.transform.rotation;
                        scale = collider.bounds.extents*2.0f;
                    }
                }
                else
                {

                        pos = PotentialComponent2.transform.position + (0.5f * PotentialComponent2.transform.lossyScale);
                        rotation = PotentialComponent2.transform.rotation;
                        scale = PotentialComponent2.transform.lossyScale;
                }
            }
            else
            {
                pivotCor = ((Vector3)PotentialComponent1.pivot - new Vector3(0.5f, 0.5f, 0));

                pivotCor.Scale(PotentialComponent1.rect.size/ PotentialComponent1.localScale);
                pos = PotentialComponent1.transform.position-pivotCor;// -  pivotCor;// + (0.5f *PotentialComponent1.localScale);
                rotation = PotentialComponent1.rotation;
                scale = PotentialComponent1.rect.size * PotentialComponent1.lossyScale / PotentialComponent1.localScale//PotentialComponent1.rect.size 
                    ;

            }

            GameObject rect = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Quad"), pos + Vector3.forward, rotation, null);
            rect.transform.localScale =  scale;
            rect.transform.name = "number 1";


        }
    }
}
