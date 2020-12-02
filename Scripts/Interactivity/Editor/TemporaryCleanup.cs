using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TemporaryCleanup : Editor
{

    [MenuItem("GUIllaume/X. Temp Cleanup", priority = 5)]
    static void FixMe()
    {
        var colliders = FindObjectsOfType<Collider>();
        colliders.ToList().ForEach(c => c.enabled = false);
        var ppL = FindObjectsOfType<PostProcessLayer>();
        ppL.ToList().ForEach(p => p.enabled = false);
        var ppV = FindObjectsOfType<PostProcessVolume>();
        ppV.ToList().ForEach(p => p.enabled = false);
        


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
