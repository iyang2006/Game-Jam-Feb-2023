using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBackground : MonoBehaviour
{
    Material material;
    float scrollSpeed = 1f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Scroll the texture based on the scroll speed
        // material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
