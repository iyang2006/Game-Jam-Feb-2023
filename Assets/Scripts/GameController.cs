using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // one dirt chunk at the start of the game
    public GameObject DirtChunk;
    public List<GameObject> DirtChunks;

    private float speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = DirtChunks.Count - 1; i >= 0; i--)
        {
            GameObject chunk = DirtChunks[i];
            float y_pos = chunk.transform.position.y;
            if (y_pos > 7) {
                // Destroy the old chunk
                Destroy(chunk);
                DirtChunks.Remove(chunk);

                // Instantiate a new chunk
                GameObject newChunk = Instantiate(DirtChunk, new Vector3(0, y_pos - 14, 0), Quaternion.identity);
                DirtChunks.Add(newChunk);
            } else {
                chunk.transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }
}
