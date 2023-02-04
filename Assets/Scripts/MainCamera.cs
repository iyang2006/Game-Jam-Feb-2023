using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public SpriteRenderer DirtBackground;

    void Start()
    {
        // make the camera the same size as the dirt background
        // float orthoSize = DirtBackground.bounds.size.x * Screen.height / Screen.width * 0.5f;
        // Camera.main.orthographicSize = orthoSize;
    }
}
