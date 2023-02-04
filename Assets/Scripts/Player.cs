using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public int waterPoints = 0;
    [HideInInspector] public float pointsMultiplier = 1;
    [HideInInspector] public float depth = 0;
    public GameObject gameOverScreen;
    public bool dead = false;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HELLO");
        //Debug.Log(collision.otherCollider.tag);
        Debug.Log(collision.collider.tag);


        if (collision.collider.tag == "Rock")
        {
            Debug.Log("DEATH");
            gameOverScreen.GetComponent<SceneControl>().gameOver();
            dead = true;
            //SceneControl.gameOver();
            //Debug.Log("DEATH");
            //control.gameOver();
        }
    }



}