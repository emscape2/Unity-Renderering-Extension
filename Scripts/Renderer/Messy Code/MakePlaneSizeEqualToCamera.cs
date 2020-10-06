using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlaneSizeEqualToCamera : MonoBehaviour
{

    public float multipliah = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var aspect = Camera.main.aspect;
        var height = Camera.main.orthographicSize * 2.0f * multipliah ;
        transform.localScale = new Vector3(height * aspect, height, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
