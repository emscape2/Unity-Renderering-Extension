using Assets.Scripts.Interactivity.ActionComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class CheckMuted : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
        var global = GlobalVars.getGlobalVars();
        var source = GetComponent<AudioSource>();
        if (source == null)
        {
            return;
        }
        var waarde = global.getVar(tag + "Audio");
        if (waarde == 0)
        {
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
