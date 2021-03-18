using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePerformance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_STANDALONE_OSX
        QualitySettings.vSyncCount = 30;
        


#endif 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
