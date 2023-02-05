using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public GameObject pointsScreen;
    SceneControl sceneControl;
    public GameObject player;
    Player playerScript;
    bool gameEnded;

    // Updating text fields in game
    public Text depthTxt;
    public Text depthOverTxt;
    public Text waterTxt;
    public Text waterOverTxt;
    private double depthDouble = 0;

    // Start is called before the first frame update
    void Start()
    {
        sceneControl = pointsScreen.GetComponent<SceneControl>();
        playerScript = player.GetComponent<Player>();
        gameEnded = false;
        playerScript.waterPoints = 0;
        playerScript.pointsMultiplier = 1;
        playerScript.depth = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (playerScript.dead)
        {
            if (!gameEnded)
            {
                gameEnded = true;
                Debug.Log(sceneControl==null);
                sceneControl.GameDeactivate();
            }
        }

        if (gameEnded)
        {
            return;
        }

        // Increase the depth by one each time a row is spawned
        depthDouble += Time.deltaTime * 8;
        playerScript.depth = (int)depthDouble;
        depthTxt.text = string.Format("{0:#,###0}", playerScript.depth);
        depthOverTxt.text = depthTxt.text;

        // Increase water points by the amount of water collected
        waterTxt.text = string.Format("{0:#,###0}", playerScript.waterPoints);
        waterOverTxt.text = waterTxt.text;
    }
}
