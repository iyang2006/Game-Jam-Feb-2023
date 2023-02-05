using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //MUSIC TESTING
    public MainCamera mainCamera;
    public float fadeDuration = 1.5f;
    public float initialVolume = 1f;
    public float finalVolume = 0.5f;
    private float fadeTime = 0f;
    //MUSIC TESTING




    public GameObject gameOverScreen;
    SceneControl sceneControl;
    public GameObject player;
    Player playerScript;
    bool gameEnded;

    // one dirt chunk at the start of the game
    public GameObject dirtChunkPrefab;
    public List<GameObject> dirtChunks;

    public GameObject rockPrefab;
    List<GameObject[]> rocks;

    float speed = 3f;

    const int TileSize = 7;
    Vector2 rockSpawnPos;

    float timeBetweenSpawns;
    float timeSinceLastSpawn;

    void Start()
    {
        sceneControl = gameOverScreen.GetComponent<SceneControl>();
        playerScript = player.GetComponent<Player>();
        gameEnded = false;

        rocks = new List<GameObject[]>();

        rockSpawnPos = new Vector2(-3, -4);
        timeBetweenSpawns = 1f / speed;
        timeSinceLastSpawn = 0.0f;

        // Instantiate the first rock row
        SpawnRockRow(0.15f);
    }

    void SpawnRockRow(float spawnChance)
    {
        // Spawn a row of rocks, with percentage chance of a rock being spawned
        GameObject[] row = new GameObject[TileSize];
        bool addRow = false;
        for (int i = 0; i < TileSize; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if (chance < spawnChance)
            {
                Vector3 pos = new Vector3(rockSpawnPos.x + i, rockSpawnPos.y, 0);
                GameObject rock = Instantiate(rockPrefab, pos, Quaternion.identity);
                row[i] = rock;
                addRow = true;
            }
        }
        if (addRow)
        {
            rocks.Add(row);
        }
    }

    public void PlayAgain()
    {

    }

    void Update()
    {
        if (playerScript.dead)
        {

            //MUSIC TESTING
            if (fadeTime < fadeDuration)
            {
                fadeTime += Time.deltaTime;
                float currentVolume = ((fadeTime / fadeDuration) * (finalVolume - initialVolume)) + initialVolume;
                mainCamera.GetComponent<AudioSource>().pitch = currentVolume;

                float currentBolume = ((fadeTime / fadeDuration) * (0.1f - 1f)) + 1f;
                mainCamera.GetComponent<AudioSource>().volume = currentBolume;
            }
            else
            {
                //mainCamera.GetComponent <AudioSource>().Stop();
                mainCamera.GetComponent<AudioSource>().volume = 0.05f;
                mainCamera.GetComponent<AudioSource>().pitch = 0.5f;
            }
            //MUSIC TESTING


            if (!gameEnded)
            {
                gameEnded = true;
                sceneControl.GameOver();
            }
        }

        if (gameEnded)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;

        Vector3 posChange = Vector3.up * speed * Time.deltaTime;

        // Spawn a new row of rocks if it's time
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnRockRow(0.15f);
        }

        // Move all the rocks up
        for (int i = 0; i < rocks.Count; i++)
        {
            GameObject[] row = rocks[i];
            for (int j = 0; j < row.Length; j++)
            {
                GameObject rock = row[j];
                if (rock != null)
                {
                    rock.transform.Translate(posChange);
                }
            }
        }

        // Remove first row of rocks if it's off the screen
        bool removeRow = false;
        if (rocks.Count > 0)
        {
            for (int i = 0; i < rocks[0].Length; i++)
            {
                GameObject rock = rocks[0][i];
                if (rock != null)
                {
                    if (rock.transform.position.y > 4)
                    {
                        removeRow = true;
                        Destroy(rock);
                    }
                }
            }
        }
        if (removeRow)
        {
            rocks.RemoveAt(0);
        }

        // Handle the dirt chunks
        for (int i = dirtChunks.Count - 1; i >= 0; i--)
        {
            // move the chunk up
            GameObject chunk = dirtChunks[i];
            chunk.transform.Translate(posChange);

            // check if the chuck should be destroyed
            float y_pos = chunk.transform.position.y;
            if (y_pos > TileSize)
            {
                // Destroy the old chunk
                Destroy(chunk);
                dirtChunks.Remove(chunk);

                // Instantiate a new chunk
                Vector2 center = new Vector2(0, y_pos - TileSize * 2);
                GameObject newChunk = Instantiate(dirtChunkPrefab, new Vector3(center.x, center.y, 0), Quaternion.identity);
                dirtChunks.Add(newChunk);
            }
        }
    }
}
