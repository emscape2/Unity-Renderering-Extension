using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShiftXByX : MonoBehaviour
{
    public float screenpercentage;
    // Start is called before the first frame update
    public void Start()
    {
        transform.localPosition = new Vector3(screenpercentage, 0, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
