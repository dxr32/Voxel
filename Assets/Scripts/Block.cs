using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public string name;

    public bool isSolid;
    public bool isTransparent;
    public bool multiTexture;

    public Vector2 texture;
    public Vector2 textureUp;
    public Vector2 textureSide;
    public Vector2 textureBot;

    public Block(string _name, bool _solid, bool _transparent, Vector2 _texture)
    {
        name = _name;
        isSolid = _solid;
        isTransparent = _transparent;
        texture = _texture;
    }

    public Block(string _name, bool _solid, bool _transparent, Vector2 _textureUp, Vector2 _textureSide, Vector2 _textureBot)
    {
        name = _name;
        isSolid = _solid;
        isTransparent = _transparent;
        textureUp = _textureUp;
        textureSide = _textureSide;
        textureBot = _textureBot;

        multiTexture = true;
    }

    public Block()
    {
        name = "Air";
        isSolid = false;
        isTransparent = false;
    }
}
