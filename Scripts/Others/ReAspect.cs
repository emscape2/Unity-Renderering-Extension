using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReAspect : MonoBehaviour
{
    public float multipliah = 1.0f;
    public bool tomultipliah;

    void Start()
    {
        var aspect = Camera.main.aspect;
        var height = Camera.main.orthographicSize * 2.0f * multipliah;
        if (tomultipliah) transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y * aspect, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
