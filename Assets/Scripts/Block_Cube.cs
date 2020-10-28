using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Cube
{
    public Chunk owner;
    public Vector3 position;
    Block block;

    public Block_Cube(Chunk o, Vector3 pos, Block _block)
    {
        owner = o;
        position = pos;
        block = _block;

        GenerateCube();
    }

    void GenerateCube()
    {
        if (owner.BlockExistsAtPos(new Vector3(position.x, position.y, position.z + 1)) == false)
            CreateCubeSide("front");
        if (owner.BlockExistsAtPos(new Vector3(position.x, position.y, position.z - 1)) == false)
            CreateCubeSide("back");
        if (owner.BlockExistsAtPos(new Vector3(position.x, position.y + 1, position.z)) == false)
            CreateCubeSide("top");
        if (owner.BlockExistsAtPos(new Vector3(position.x, position.y - 1, position.z)) == false)
            CreateCubeSide("bottom");
        if (owner.BlockExistsAtPos(new Vector3(position.x + 1, position.y, position.z)) == false)
            CreateCubeSide("left");
        if (owner.BlockExistsAtPos(new Vector3(position.x - 1, position.y, position.z)) == false)
            CreateCubeSide("right");
    }

    void CreateCubeSide(string side)
    {
        owner.triangles.Add(0 + owner.vertices.Count);
        owner.triangles.Add(1 + owner.vertices.Count);
        owner.triangles.Add(2 + owner.vertices.Count);

        owner.triangles.Add(0 + owner.vertices.Count);
        owner.triangles.Add(2 + owner.vertices.Count);
        owner.triangles.Add(3 + owner.vertices.Count);

        float textureOffset = 1f / 2f;

        Vector2 texturePos;

        if (block.multiTexture)
        {
            if (side == "top")
                texturePos = block.textureUp;
            else if (side == "bottom")
                texturePos = block.textureBot;
            else
                texturePos = block.textureSide;
        }
        else
            texturePos = block.texture;

        owner.UVs.Add(new Vector2((textureOffset * texturePos.x) + textureOffset, textureOffset * texturePos.y));
        owner.UVs.Add(new Vector2((textureOffset * texturePos.x) + textureOffset, (textureOffset * texturePos.y) + textureOffset));
        owner.UVs.Add(new Vector2(textureOffset * texturePos.x, (textureOffset * texturePos.y) + textureOffset));
        owner.UVs.Add(new Vector2(textureOffset * texturePos.x, textureOffset * texturePos.y));

        Vector3 V0 = new Vector3(0.5f, -0.5f, 0.5f);
        Vector3 V1 = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 V2 = new Vector3(-0.5f, 0.5f, 0.5f);
        Vector3 V3 = new Vector3(-0.5f, -0.5f, 0.5f);
        Vector3 V4 = new Vector3(-0.5f, -0.5f, -0.5f);
        Vector3 V5 = new Vector3(-0.5f, 0.5f, -0.5f);
        Vector3 V6 = new Vector3(0.5f, 0.5f, -0.5f);
        Vector3 V7 = new Vector3(0.5f, -0.5f, -0.5f);

        switch (side)
        {
            case "front":
                owner.vertices.Add(V0 + position);
                owner.vertices.Add(V1 + position);
                owner.vertices.Add(V2 + position);
                owner.vertices.Add(V3 + position);
            break;

            case "back":
                owner.vertices.Add(V4 + position);
                owner.vertices.Add(V5 + position);
                owner.vertices.Add(V6 + position);
                owner.vertices.Add(V7 + position);
            break;

            case "top":
                owner.vertices.Add(V1 + position);
                owner.vertices.Add(V6 + position);
                owner.vertices.Add(V5 + position);
                owner.vertices.Add(V2 + position);
            break;

            case "bottom":
                owner.vertices.Add(V7 + position);
                owner.vertices.Add(V0 + position);
                owner.vertices.Add(V3 + position);
                owner.vertices.Add(V4 + position);
            break;

            case "right":
                owner.vertices.Add(V3 + position);
                owner.vertices.Add(V2 + position);
                owner.vertices.Add(V5 + position);
                owner.vertices.Add(V4 + position);
            break;

            case "left":
                owner.vertices.Add(V7 + position);
                owner.vertices.Add(V6 + position);
                owner.vertices.Add(V1 + position);
                owner.vertices.Add(V0 + position);
            break;
        }
    }
}
