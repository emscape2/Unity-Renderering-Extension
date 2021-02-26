using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlterMaterialProperty : MonoBehaviour
{
    public string property;
    public float startValue, endValue, speedPerSecond;
    public bool loop;
    private Material mat;
    // Start is called before the first frame update
    void OnEnable()
    {
        mat = GetComponents<Renderer>().First().material;
        mat.SetFloat(property, startValue);
    }

    // Update is called once per frame
    void Update()
    {
        var value = mat.GetFloat(property);
        if (loop)
        {
            if (Mathf.Abs(value - startValue) > Mathf.Abs(startValue - endValue))
            {
                mat.SetFloat(property, startValue);
                return;
            }
        }
            else if (speedPerSecond > 0)
            {
                if (value > endValue)
                    return;

            }
            else if (speedPerSecond < 0)
            {
                if (value < endValue)
                    return;
            }
            mat.SetFloat(property, value + speedPerSecond * Time.deltaTime);
    }
}
