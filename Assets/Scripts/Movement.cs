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
        //Debug.Log(position.y);

        mousePosition = Input.mousePosition;
        //Debug.Log(mousePosition.x + ", " + mousePosition.y + "  |  " + Screen.width);

        float halfScreen = Screen.width / 2;
        float rootPosition = (mousePosition.x - halfScreen) * (10 / halfScreen);
        if (rootPosition < -10)
        {
            gameObject.transform.position = new Vector3(-10, position.y, position.z);
        }
        else if (rootPosition > 10)
        {
            gameObject.transform.position = new Vector3(10, position.y, position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(rootPosition, position.y, position.z);
        }
        
        //Debug.Log("(" + mousePosition.x + " - " + "(" + Screen.width + "/" + 2 + ")) * (" + 10 + "/ (" + Screen.width + "/ (" + 2 + "))");
        //Debug.Log(mousePosition.x + "  |  " + rootPosition + "  |  " + Screen.width);
    }
}
