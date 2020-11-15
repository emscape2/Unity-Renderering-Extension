using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosX : MonoBehaviour
{
    public float BeeldTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(BeeldTransform,0, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
