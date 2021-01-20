using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class MouseBehavior
{

    public static bool MouseOver(Vector2 pos, GameObject gameObject)
    {
        // var rect = rectSurface();
        var guillaumeCam = Camera.allCameras.Where(c => c.gameObject.layer == 29).FirstOrDefault();//eerste guillaume camera 
        if (guillaumeCam == null)
        {
            Debug.LogError("no carmerá attaché");
            return false;
        }
        else
        {
            // var ray = (Vector2)rect.InverseTransformPoint(guillaumeCam.ScreenPointToRay(pos).origin);
            var ray = guillaumeCam.ScreenPointToRay(pos);
            if (RectSurface(ray.origin, gameObject, guillaumeCam))
            {
                Debug.LogWarning("Clicked: " + gameObject.name);
                return true;
            }
        }

        return false;
    }

    private static bool RectSurface(Vector2 pos, GameObject gameObject, Camera GuillaumeCam)
    {

        RectTransform PotentialComponent1;
        if (!gameObject.TryGetComponent<RectTransform>(out PotentialComponent1))
        {

            SpriteRenderer PotentialComponent2;

            if (!gameObject.TryGetComponent<SpriteRenderer>(out PotentialComponent2))
            {
                MeshFilter collider;

                if (!gameObject.TryGetComponent<MeshFilter>(out collider))
                {
                    Debug.LogError(gameObject.name + ": no collada attaché");
                    return false;
                }
                else
                    return Intersects(pos, collider.mesh.bounds);

            }
            else
            {
                var bonds = PotentialComponent2.bounds;
                var answer = Intersects(pos, PotentialComponent2.bounds);

                return answer;
            }
        }
        else //(PotentialComponent1 != null)
        {
            gameObject.transform.DetachChildren();
            var name = gameObject.name;
            gameObject.SetActive(false);
            //throw new Exception("Guillaume Error: " + name + ": RectTransforms are incompatible with Guillaume. Use proper components or get Rect (it's funny cuz you can't easily do that).");

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
        if (pos.x > xMin && pos.x < xMax && pos.y > yMin && pos.y < yMax)
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
        Quaternion rotation = Quaternion.identity;
        Vector3 pivotCor = Vector3.zero;
        if (PotentialComponent1 == null)
        {
            SpriteRenderer PotentialComponent2 = gameObject.GetComponent<SpriteRenderer>();
            if (PotentialComponent2 == null)
            {
                Mesh collider = gameObject.GetComponent<MeshFilter>().mesh;
                {
                    pos = collider.bounds.center; //- collider.bounds.extents;
                    rotation = gameObject.transform.rotation;
                    scale = collider.bounds.extents * 2.0f;
                }
            }
            else
            {

                pos = PotentialComponent2.bounds.center;
                rotation = PotentialComponent2.transform.rotation;
                scale = PotentialComponent2.bounds.extents * 2.0f;
            }
        }
        else
        {
            Debug.LogError("Fuck off and die.");
            return;
        }

        GameObject rect = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Quad"), pos, rotation, null);
        rect.transform.localScale = scale;
        rect.transform.name = "Debugging Quad, please take care.";


    }
}