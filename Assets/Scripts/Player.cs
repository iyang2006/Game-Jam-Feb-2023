using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int WaterPoints
    {
        get { return WaterPoints; }
        set { WaterPoints = value; }
    }

    public float PointsMultiplier
    { 
        get { return PointsMultiplier; }
        set { PointsMultiplier = value; }
    }

    public float Depth
    {
        get { return Depth; }
        set { Depth = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        WaterPoints = 0;
        PointsMultiplier = 1;
        Depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}