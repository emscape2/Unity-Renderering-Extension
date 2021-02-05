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

    [MenuItem("GUIllaume/X. Temp Emissionary", priority = 5)]
    static void FixMe()
    {
        var components = FindObjectsOfType<MakeMaterialLookActivatedConsequence>();

        foreach (var comp in components)
        {
            comp.ColorName = "_Emission";
        }

        


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
