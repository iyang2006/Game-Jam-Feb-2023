using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // one dirt chunk at the start of the game
    public GameObject DirtChunk;
    public List<GameObject> DirtChunks;

    public GameObject Rock;
    List<GameObject[]> rocks;

    float speed = 2f;

    const int TileSize = 7;
    Vector2 rockSpawnPos;

    float timeBetweenSpawns;
    float timeSinceLastSpawn;

    void Start()
    {
        rocks = new List<GameObject[]>();

        rockSpawnPos = new Vector2(-3, -4);
        // timeBetweenSpawns = 1.0f / speed;
        timeBetweenSpawns = 1f / speed;
        timeSinceLastSpawn = 0.0f;

        // Instantiate the first rock row
        SpawnRockRow(0.2f);
    }

    void SpawnRockRow(float spawnChance)
    {
        // Spawn a row of rocks, with percentage chance of a rock being spawned
        GameObject[] row = new GameObject[TileSize];
        for (int i = 0; i < TileSize; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if (chance < spawnChance)
            {
                Vector3 pos = new Vector3(rockSpawnPos.x + i, rockSpawnPos.y, 0);
                GameObject rock = Instantiate(Rock, pos, Quaternion.identity);
                row[i] = rock;
            }
        }
        rocks.Add(row);
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        Vector3 posChange = Vector3.up * speed * Time.deltaTime;

        // Spawn a new row of rocks if it's time
        if (timeSinceLastSpawn > timeBetweenSpawns)
        {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnRockRow(0.2f);
        }

        // Remove first row of rocks if it's off the screen
        if (rocks.Count > 0)
        {
            for (int i = 0; i < rocks[0].Length; i++)
            {
                GameObject rock = rocks[0][i];
                if (rock != null)
                {
                    if (rock.transform.position.y > TileSize)
                    {
                        Destroy(rock);
                    }
                }
            }
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

        // Handle the dirt chunks
        for (int i = DirtChunks.Count - 1; i >= 0; i--)
        {
            // move the chunk up
            GameObject chunk = DirtChunks[i];
            chunk.transform.Translate(posChange);

            // check if the chuck should be destroyed
            float y_pos = chunk.transform.position.y;
            if (y_pos > TileSize)
            {
                // Destroy the old chunk
                Destroy(chunk);
                DirtChunks.Remove(chunk);

                // Instantiate a new chunk
                Vector2 center = new Vector2(0, y_pos - TileSize * 2);
                GameObject newChunk = Instantiate(DirtChunk, new Vector3(center.x, center.y, 0), Quaternion.identity);
                DirtChunks.Add(newChunk);
            }
        }
    }
}
