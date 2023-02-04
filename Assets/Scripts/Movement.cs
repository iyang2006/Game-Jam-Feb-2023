using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        initialMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x + 0.1f, position.y, position.z);
        //position.x = position.x + 100.0f;
        Debug.Log(position.y);
        //mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition.x + ", " + mousePosition.y);
        //Vector3 position = gameObject.transform.position;
        //position.x = mousePosition.x;
    }
}
