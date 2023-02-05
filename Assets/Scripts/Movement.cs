using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float HalfTileWidth = 3.25f;

    //Vector3 initialMousePosition;
    Vector3 mousePosition;
    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        //initialMousePosition = Input.mousePosition;
        previousPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Player>().dead == false)
        {
            Vector3 position = gameObject.transform.position;
            //Debug.Log(position.y);

            mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition.x + ", " + mousePosition.y + "  |  " + Screen.width);

            float halfScreen = Screen.width / 2;
            float rootPosition = (mousePosition.x - halfScreen) * (HalfTileWidth / halfScreen);
            if (rootPosition < -HalfTileWidth)
            {
                gameObject.transform.position = new Vector3(-HalfTileWidth, position.y, position.z);
            }
            else if (rootPosition > HalfTileWidth)
            {
                gameObject.transform.position = new Vector3(HalfTileWidth, position.y, position.z);
            }
            else
            {
                gameObject.transform.position = new Vector3(rootPosition, position.y, position.z);
            }

            Vector2 previous2DPosition = new Vector2(previousPosition.x, previousPosition.y);
            Vector2 movementDirection = new Vector2((gameObject.transform.position.x - previousPosition.x), gameObject.transform.position.y);

            RaycastHit2D raycastHit = Physics2D.Raycast(previous2DPosition, movementDirection, movementDirection.x);
            // Debug.Log("RayCast fired!");
            if (raycastHit)
            {
                // Debug.Log("Hit somethin'!   |   " + raycastHit.collider.name + "   |   " + raycastHit.collider.tag + "   |   " +
                //     previous2DPosition.x + " : " + gameObject.transform.position.x + "   |   " + movementDirection.x + "   |   " + movementDirection.magnitude);
                if (raycastHit.collider.tag == "Rock")
                {
                    // Debug.Log("We hit a Rock!");
                    gameObject.GetComponent<Player>().dead = true;
                }
            }
            previousPosition = gameObject.transform.position;

            //Debug.Log("(" + mousePosition.x + " - " + "(" + Screen.width + "/" + 2 + ")) * (" + TileWidth + "/ (" + Screen.width + "/ (" + 2 + "))");
            //Debug.Log(mousePosition.x + "  |  " + rootPosition + "  |  " + Screen.width);
        }
    }
}
