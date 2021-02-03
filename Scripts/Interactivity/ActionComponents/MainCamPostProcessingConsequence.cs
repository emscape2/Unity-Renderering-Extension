using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainCamPostProcessingConsequence : Consequence
{
    PostProcessLayer layer;
    PostProcessVolume volume;

    public override void Disengage()
    {
        layer.antialiasingMode  = layer.antialiasingMode > 0 ? layer.antialiasingMode-- : 0;
        volume.enabled = (layer.antialiasingMode > PostProcessLayer.Antialiasing.FastApproximateAntialiasing);
    }

    public override void Engage()
    {

        layer.antialiasingMode = (layer.antialiasingMode < PostProcessLayer.Antialiasing.FastApproximateAntialiasing )? layer.antialiasingMode++ : PostProcessLayer.Antialiasing.TemporalAntialiasing);
        volume.enabled = (layer.antialiasingMode > PostProcessLayer.Antialiasing.FastApproximateAntialiasing);
    }

    // Start is called before the first frame update
    void Start()
    {
        layer = Camera.main.GetComponent<PostProcessLayer>();
        volume = Camera.main.GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
