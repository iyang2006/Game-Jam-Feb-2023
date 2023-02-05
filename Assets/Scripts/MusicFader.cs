using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    public float fadeDuration = 3f;
    public float initialVolume = 0f;
    public float finalVolume = 1f;
    private float fadeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = initialVolume;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float currentVolume = ((fadeTime / fadeDuration) * (finalVolume - initialVolume)) + initialVolume;
            gameObject.GetComponent<AudioSource>().volume = currentVolume;
        }

    }
}
