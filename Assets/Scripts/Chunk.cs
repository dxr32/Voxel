using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    Block[,,] chunkMap;

    GameObject chunkObj;

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> UVs = new List<Vector2>();

    Mesh chunkMesh;

    public Material chunkMaterial;

    public Chunk(Vector3 position, Material mat)
    {
        chunkObj = new GameObject("Chunk");

        chunkObj.transform.position = position;
        chunkMaterial = mat;
        
        MakeChunk();
    }

    void MakeChunk()
    {
        chunkMap = new Block[World.chunkSize, World.chunkSize, World.chunkSize];
        
        GenerateVirtualMap();
    }

    void GenerateVirtualMap()
    {
        for (int x = 0; x < World.chunkSize; x++)
            for (int y = 0; y < World.chunkSize; y++)
                for (int z = 0; z < World.chunkSize; z++)
                {
                    int offset = 5 * World.chunkSize;

                    int worldX = (int)(x + chunkObj.transform.position.x);
                    int worldY = (int)((y + offset) + chunkObj.transform.position.y);
                    int worldZ = (int)(z + chunkObj.transform.position.z);

                    if(worldY <= Noise.GenerateStoneHeight(worldX, worldZ))
                    {
                        Block stoneBlock = DB_Blocks.GetBlockByName("Stone");
                        chunkMap[x, y, z] = stoneBlock;
                    }

                    else if(worldY < Noise.GenerateHeight(worldX, worldZ))
                    {
                        Block dirtBlock = DB_Blocks.GetBlockByName("Dirt");
                        chunkMap[x, y, z] = dirtBlock;
                    }
                    else if (worldY == Noise.GenerateHeight(worldX, worldZ))
                    {
                        Block grassBlock = DB_Blocks.GetBlockByName("Grass");
                        chunkMap[x, y, z] = grassBlock;
                    }
                    else
                    {
                        Block airBlock = DB_Blocks.GetBlockByName("Air");
                        chunkMap[x, y, z] = airBlock;
                    }
                }
    }
    

    public void GenerateBlocksMap()
    {
        for(int x = 0; x < World.chunkSize; x++)
            for(int y = 0; y < World.chunkSize; y++)
                for(int z = 0; z < World.chunkSize; z++)
                {
                    if(chunkMap[x, y, z] != DB_Blocks.GetBlockByName("Air"))
                    {
                        Block_Cube newCube = new Block_Cube(this, new Vector3(x, y, z), chunkMap[x, y, z]);
                    }
                }
        GeneratePhysicalChunk();
    }

    int ConvertIndexToLocal(int i)
    {
        if (i == -1)
            i = World.chunkSize - 1;
        else if (i == World.chunkSize)
            i = 0;
        return i;
    }

    public bool BlockExistsAtPos(Vector3 position)
    {
        int x = (int)position.x;
        int y = (int)position.y;
        int z = (int)position.z;

        if (x < 0 || x >= World.chunkSize || y < 0 || y >= World.chunkSize || z < 0 || z >= World.chunkSize)
        {
            Chunk neighbourChunk = World.GetChunkAtPos(chunkObj.transform.position + position);

            x = ConvertIndexToLocal(x);
            y = ConvertIndexToLocal(y);
            z = ConvertIndexToLocal(z);

            if (neighbourChunk != null)
                return neighbourChunk.BlockExistsAtPos(new Vector3(x, y, z));
            else
                return false;
        }
        else if (!chunkMap[x, y, z].isTransparent)
            return false;
        else
            return true;
    }

    public void GeneratePhysicalChunk()
    {
        chunkMesh = new Mesh();

        MeshFilter mf = chunkObj.AddComponent<MeshFilter>();
        MeshRenderer mr = chunkObj.AddComponent<MeshRenderer>();
        MeshCollider mc = chunkObj.AddComponent<MeshCollider>();

        chunkMesh.vertices = vertices.ToArray();
        chunkMesh.triangles = triangles.ToArray();
        chunkMesh.uv = UVs.ToArray();

        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();

        mf.mesh = chunkMesh;
        mr.material = chunkMaterial;

        mc.sharedMesh = chunkMesh;
    }
}
