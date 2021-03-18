﻿using Assets.Coding.Renderer;
using Assets.Scripts.Interactivity.ActionComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
[RequireComponent(typeof(MeshRenderer))]
public class SinusoidRendererComponent : MonoBehaviour
{
    public double fakeBpm
    {
        get { return realbpm * (2 * Math.PI); }
        set { realbpm = value / (2 * Math.PI); }
    }
    public float amplitude;
    public float width;
    public double totalLength;
    public Color color;
    public bool setColor;
    public bool optimized;
    public double detail;
    public Vector2[] points;
    public double ratioUp;
    public double ratioDown;
    public double realbpm;
    public bool startUp;
    public bool getSPEED;
    internal bool _started;
    protected int iterations;
    // Start is called before the first frame update
    void Start()
    {
        if (getSPEED)
        {
            var speed = GlobalVars.getGlobalVars().getVar("SPEED");
            switch (speed)
            {
                case 0:
                    realbpm = 6.5;
                    break;
                case 1:
                    realbpm = 6;
                    break;
                case 2:
                    realbpm = 5.5;
                    break;
            }
        }

        List<Vector2> pointsafterEmielsZak = GetPointsAfterEmielsZak();


        points = pointsafterEmielsZak.ToArray();
        MeshData meshData = new MeshData(
            pointsafterEmielsZak,
            width, !optimized);
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = meshData.vertices.ToArray();
        mesh.uv = meshData.newUv.ToArray();
        mesh.RecalculateBounds();

        mesh.triangles = meshData.triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = Resources.Load<Material>("Materials/colouredUnlit");
        if (setColor)
        meshRenderer.material.SetColor("_EmissionColor", color);

        if (!_started)
            _started = true;
    }

    protected virtual List<Vector2> GetPointsAfterEmielsZak()
    {
        iterations = 0;
        var totalRatio = ratioUp + ratioDown;
        var Omhoog = Upwards_Graph(totalRatio);
        var Omlaag = Downwards_Graph(totalRatio);
        List<Vector2> pointsafterEmielsZak = new List<Vector2>();
        if (startUp)
        {
            var lastOmh = Omlaag.Last();
            pointsafterEmielsZak.Add(Omhoog[0]);
            for(double i = 0; i < (totalLength * realbpm); i += 1)
            {
                iterations++;
                pointsafterEmielsZak.Add(new Vector2(((Omhoog[0].x +
                           (float)(i * 60.0 / realbpm)))
                       , -amplitude));
                for (int j = 1; j < Omhoog.Count; j++)
                {
                    pointsafterEmielsZak.Add(new Vector2(((Omhoog[j].x +
                            (float)(i * 60.0 / realbpm)))
                        , Omhoog[j].y));
                }

                for (int j = 0; j < Omlaag.Count; j++)
                {
                    pointsafterEmielsZak.Add(new Vector2((Omlaag[j].x + (
                        (float)(
                        (i + (ratioUp / totalRatio))
                        * 60.0 / realbpm)))
                        , Omlaag[j].y));
                }
            }
        }
        else
        {
            var lastOmh = Omhoog.Last();

            pointsafterEmielsZak.Add(Omlaag[0]);
            for (double i = 0; i < (totalLength * realbpm); i += 1)
            {
                iterations++;
                pointsafterEmielsZak.Add(new Vector2(((Omlaag[0].x +
                           (float)(i * 60.0 / realbpm)))
                       , amplitude));
                for (int j = 1; j < Omlaag.Count; j++)
                {
                    pointsafterEmielsZak.Add(new Vector2(((Omlaag[j].x +
                            (float)(i * 60.0 / realbpm)))
                        , Omlaag[j].y));
                }

                for (int j = 0; j < Omhoog.Count; j++)
                {
                    pointsafterEmielsZak.Add(new Vector2((Omhoog[j].x + (
                        (float)(
                        (i + (ratioDown / totalRatio))
                        * 60.0 / realbpm)))
                        , Omhoog[j].y));
                }
            }
        }
        return pointsafterEmielsZak;
    }

    private void OnEnable()
    {

        detail += 1;
    }




    // Update is called once per frame
    void Update()
    {
        var global = GlobalVars.getGlobalVars(); 
        if (global.getVar("Pause") == 0 && global.getVar("delayGlobal") <= 0)
        {
            transform.position -= new Vector3(Time.deltaTime, 0);
        }
        
    }

    public List<Vector2> Downwards_Graph(double totalRatio)
    {
        var pointss = new Sinusoid().Points(realbpm / (ratioDown / totalRatio), amplitude, detail, 0);
        return pointss.ToList();

    }

    public List<Vector2> Upwards_Graph(double totalRatio)
    {
        var pointss = new Sinusoid().Points(realbpm / (ratioUp / totalRatio), amplitude, detail, 0.5);
        return pointss.ToList();

    }

}