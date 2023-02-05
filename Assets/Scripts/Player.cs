using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public int waterPoints = 0;
    [HideInInspector] public float pointsMultiplier = 1;
    [HideInInspector] public float depth = 0;

    [HideInInspector] public bool dead = false;

    public AudioSource bonkSound;
    public AudioSource waterSound;
    public AudioSource rootBeerSound;

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
        Debug.Log("Collision");
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Rock")
        {
            dead = true;
            bonkSound.Play();
        }
        else if (collision.collider.tag == "Water")
        {
            collision.gameObject.SetActive(false);
            waterPoints += (int) Math.Ceiling(pointsMultiplier);
            waterSound.Play();
            //Debug.Log("POINTS: " + waterPoints + "=================================================");
        }
        else if (collision.collider.tag == "RootBeer")
        {
            collision.gameObject.SetActive(false);
            pointsMultiplier = (float) Math.Ceiling(pointsMultiplier * 1.5f);
            rootBeerSound.Play();
            //Debug.Log("POINTS: " + waterPoints + "=================================================");
        }
    }
}
