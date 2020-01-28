using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, 
        float scale, int octaves, float persitance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random rand = new System.Random(seed);
        Vector2[] OctaveOfsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rand.Next(-100000, 100000) +offset.x;
            float offsetY = rand.Next(-100000, 100000)+ offset.y;
            OctaveOfsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frrquency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float testx = (x-halfWidth) / scale * frrquency 
                        +OctaveOfsets[i].x;

                    float testy = (y-halfHeight) / scale * frrquency 
                        + OctaveOfsets[i].y;

                    float perlin = Mathf.PerlinNoise(testx, testy) * 2 - 1;
                    noiseHeight += perlin * amplitude;

                    amplitude *= persitance;
                    frrquency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }

        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp
                    (minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }

        }
        return noiseMap;
    }
}
