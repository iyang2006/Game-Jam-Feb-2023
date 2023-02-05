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

    [HideInInspector] public bool dead;

    public AudioSource bonkSound;
    public AudioSource waterSound;
    public AudioSource rootBeerSound;
    public AudioSource rootCanalSound;

    float timerDuration = 8f;
    float timerTime = 0f;
    bool timerActive = false;

    float fadeTime = 2f;
    float fadeDuration = 2f;
    public GameObject mainCamera;

    bool fadingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        waterPoints = 0;
        pointsMultiplier = 1;
        depth = 0;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {

            if (fadeTime < fadeDuration)
            {
                fadeTime += Time.deltaTime;
                float currentVolume = ((fadeTime / fadeDuration) * (1.5f - 1f)) + 1f;
                mainCamera.GetComponent<AudioSource>().pitch = currentVolume;
            }

            timerTime -= Time.deltaTime;
            if (timerTime <= 2)
            {
                if (!fadingOut)
                {
                    fadeTime = 0;
                    fadingOut = true;
                }
                fadeTime += Time.deltaTime;
                float currentVolume = ((fadeTime / fadeDuration) * (1f - 1.5f)) + 1.5f;
                mainCamera.GetComponent<AudioSource>().pitch = currentVolume;
            }
            if (timerTime <= 0)
            {
                mainCamera.GetComponent<AudioSource>().pitch = 1f;
                timerActive = false;
                fadingOut = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision");
        // Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Rock")
        {
            if (!timerActive)
            {
                dead = true;
                bonkSound.Play();
            }
            else
            {
                collision.gameObject.SetActive(false);
                rootCanalSound.Play();
            }
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
        else if (collision.collider.tag == "RootCanal")
        {
            collision.gameObject.SetActive(false);
            rootBeerSound.Play();

            timerTime = timerDuration;
            timerActive = true;
            fadeTime = 0;

            //Debug.Log("POINTS: " + waterPoints + "=================================================");
        }
    }
}
