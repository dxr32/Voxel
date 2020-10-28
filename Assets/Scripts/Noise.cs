using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    static int maxHeight = 150;
    static float smooth = 0.01f;
    static int octaves = 2;
    static float persistance = 0.5f;

    static float Map(float newmin, float newmax, float origmin, float origmax, float value)
    {
        return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origmin, origmax, value));
    }

    public static int GenerateStoneHeight(float x, float z)
    {
        float height = Map(0, maxHeight, 0, 1, fBM(x * smooth, z * smooth, octaves, persistance));
        return (int)height - 6;
    }

    public static int GenerateHeight(float x, float z)
    {
        float height = Map(0, maxHeight, 0, 1, fBM(x * smooth, z * smooth, octaves, persistance));
        return (int)height;
    }

    static float fBM(float x, float z, int oct, float pers)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for(int i = 0; i < oct; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

            maxValue += amplitude;

            amplitude *= pers;
            frequency *= 2;
        }
        return total / maxValue;
    }
}
