using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Blocks : MonoBehaviour
{
    public static List<Block> blockList = new List<Block>();

    void Start()
    {
        blockList.Add(new Block());
        blockList.Add(new Block("Dirt", true, false, new Vector2(1, 0)));
        blockList.Add(new Block("Grass", true, false, new Vector2(1, 1)));
        blockList.Add(new Block("Stone", true, false, new Vector2(0, 1)));
    }

    public void GenerateDB()
    {
        blockList.Add(new Block());
    }

    public static Block GetBlockByName(string name)
    {
        for(int i = 0; i < blockList.Count; i++)
        {
            if (blockList[i].name == name)
                return blockList[i];
        }
        return null;
    }
}
