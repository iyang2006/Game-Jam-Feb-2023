using System.Linq;
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

    LineRenderer lineRenderer;
    List<Vector3> linePoints;
    const float LineLength = 3;
    const int LineCount = 100;
    const float LineGap = LineLength / LineCount;
    float lineSpawnTime;
    float timeSinceLastLineChange;

    // one dirt chunk at the start of the game
    public GameObject dirtChunkPrefab;
    public List<GameObject> dirtChunks;


    //
    public GameObject rockPrefab;
    public GameObject squareRockPrefab;
    public GameObject waterPrefab;
    public GameObject rootBeerPrefab;
    List<GameObject[]> rocks;

    int emptyPos;

    float speed = 1.5f;

    float spawnChance = 0.10f;
    float maxRocks = 3;

    const int TileSize = 7;
    Vector2 rockSpawnPos;

    float timeBetweenSpawns;
    float timeSinceLastSpawn;

    public float rockSpawnChance = 0.15f;
    public float waterSpawnChance = 0.05f;
    public float rootBeerSpawnChance = 0.03f;

    float levelTime;
    int level = 1;
    float levelUpInterval = 10;

    void Start()
    {
        sceneControl = gameOverScreen.GetComponent<SceneControl>();
        playerScript = player.GetComponent<Player>();
        Debug.Log(playerScript);
        gameEnded = false;

        lineRenderer = player.GetComponent<LineRenderer>();

        rocks = new List<GameObject[]>();

        rockSpawnPos = new Vector2(-3, -4);
        timeBetweenSpawns = 1f / speed;
        timeSinceLastSpawn = 0.0f;

        // Initialize line renderer positions with 10 points over 3 units
        lineSpawnTime = LineGap / speed;
        timeSinceLastLineChange = 0;

        lineRenderer.positionCount = LineCount;
        linePoints = new List<Vector3>();
        for (int i = 0; i < LineCount; i++)
        {
            linePoints.Add(new Vector3(0, player.transform.position.y + i * LineGap, 0));
        }
        lineRenderer.SetPositions(linePoints.ToArray());

        levelTime = 0;

        emptyPos = -1;

        SpawnRockRow();
    }

    bool[] ShuffleArray(bool[] array)
    {
        System.Random rnd = new System.Random();
        bool[] shuffledArray = array.OrderBy(a => rnd.Next()).ToArray();
        return shuffledArray;
    }

    void SpawnRockRow()
    {
        // Spawn a row of rocks, with percentage chance of a rock being spawned

        bool[] addedItem = new bool[TileSize];

        int rocksSpawned = 0;
        for (int i = 0; i < TileSize; i++)
        {
            //bool addedItem = false;
            float chance = Random.Range(0.0f, 1.0f);
            if (chance < spawnChance)
            {
                addedItem[i] = true;
                rocksSpawned++;
                if (rocksSpawned >= maxRocks)
                {
                    addedItem = ShuffleArray(addedItem);
                    break;
                }
            }
            else {
                addedItem[i] = false;
            }
        }

        GameObject[] row = new GameObject[TileSize];
        for (int i = 0; i < TileSize; i++) {
            bool spawn = addedItem[i];
            if (spawn) {
                Vector3 pos = new Vector3(rockSpawnPos.x + i, rockSpawnPos.y, 0);
                GameObject rock = Instantiate(rockPrefab, pos, Quaternion.identity);
                row[i] = rock;
            }
        }

        // make sure there is always a gap in the rocks
        if (rocks.Count > 0)
        {
            if (emptyPos == -1)
            {
                emptyPos = Random.Range(0, TileSize);
            }

            if (emptyPos == TileSize - 1)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    emptyPos--;
                }
            }
            else if (emptyPos == 0)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    emptyPos++;
                }
            }
            else
            {
                int choice = Random.Range(0, 3);
                if (choice == 0)
                {
                    emptyPos--;
                }
                else if (choice == 1)
                {
                    emptyPos++;
                }
            }

            if (row[emptyPos] != null)
            {
                Destroy(row[emptyPos]);
                row[emptyPos] = null;
            }
        }

        bool addRow = false;
        for (int i = 0; i < TileSize; i++)
        {
            if (row[i] != null)
            {
                addRow = true;
                break;
            }
        }

        for (int i = 0; i < TileSize; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if ((chance < waterSpawnChance) && (row[i] == null))
            {
                Vector3 pos = new Vector3(rockSpawnPos.x + i, rockSpawnPos.y, 0);
                GameObject water = Instantiate(waterPrefab, pos, Quaternion.identity);
                row[i] = water;
                addRow = true;
            }
        }

        for (int i = 0; i < TileSize; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if ((chance < rootBeerSpawnChance) && (row[i] == null))
            {
                Vector3 pos = new Vector3(rockSpawnPos.x + i, rockSpawnPos.y, 0);
                GameObject rootBeer = Instantiate(rootBeerPrefab, pos, Quaternion.identity);
                row[i] = rootBeer;
                addRow = true;
            }
        }

        if (addRow)
        {
            rocks.Add(row);
        }
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

        levelTime += Time.deltaTime;
        // levels up every 10 seconds
        if (levelTime > levelUpInterval)
        {
            level++;
            levelTime -= levelUpInterval;

            if (speed < 3)
                speed += 0.5f;

            timeBetweenSpawns = 1f / speed;
            lineSpawnTime = LineGap / speed;

            if (spawnChance < 0.7f)
                spawnChance += 0.05f;

            if (maxRocks < 5.5f)
                maxRocks += 0.5f;
        }

        timeSinceLastSpawn += Time.deltaTime;

        Vector3 posChange = Vector3.up * speed * Time.deltaTime;

        // edit the line renderer
        timeSinceLastLineChange += Time.deltaTime;
        if (timeSinceLastLineChange > lineSpawnTime)
        {
            timeSinceLastLineChange -= lineSpawnTime;

            // Debug.Log("line change");
            for (int i = 0; i < linePoints.Count; i++)
            {
                linePoints[i] += LineGap * Vector3.up;
                // Debug.Log(linePoints[i]);
            }
            linePoints.RemoveAt(linePoints.Count - 1);
            linePoints.Insert(0, new Vector3(player.transform.position.x, player.transform.position.y, 0));
            lineRenderer.SetPositions(linePoints.ToArray());
        }

        // Spawn a new row of rocks if it's time
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnRockRow();
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
