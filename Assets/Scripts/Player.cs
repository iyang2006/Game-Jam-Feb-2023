using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int waterPoints = 0;
    public float pointsMultiplier = 1;
    public float depth = 0;

    // Start is called before the first frame update
    void Start()
    {
        waterPoints = 0;
        pointsMultiplier = 1;
        depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}