using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Material terrainMaterial;

    public static int worldSize = 20;
    public static int columnHeight = 50;
    public static int chunkSize = 4;

    public static Dictionary<Vector3, Chunk> chunkList;

    void Start()
    {
        chunkList = new Dictionary<Vector3, Chunk>();
        
        for(int x = 0; x < worldSize * chunkSize; x += chunkSize)
            for(int z = 0; z < worldSize * chunkSize; z += chunkSize)
            {
                GenerateColumn(x, z);
            }
        foreach(KeyValuePair<Vector3, Chunk> c in chunkList)
        {
            c.Value.GenerateBlocksMap();
        }
    }

    void GenerateColumn(int x, int z)
    {
        for(int y = 0; y < columnHeight * chunkSize; y += chunkSize)
        {
            CreateChunk(new Vector3(x, y, z));
        }
    }

    public static Chunk GetChunkAtPos(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        x = x / chunkSize;
        y = y / chunkSize;
        z = z / chunkSize;

        x = Mathf.FloorToInt(x);
        y = Mathf.FloorToInt(y);
        z = Mathf.FloorToInt(z);

        x = x * chunkSize;
        y = y * chunkSize;
        z = z * chunkSize;

        Vector3 chunkPos = new Vector3(x, y, z);

        Chunk foundChunk;

        if (chunkList.TryGetValue(chunkPos, out foundChunk))
            return foundChunk;
        else
            return null;
    }

    void CreateChunk(Vector3 position)
    {
        Chunk newChunk = new Chunk(position, terrainMaterial);

        chunkList.Add(position, newChunk);
    }
}
