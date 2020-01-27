using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise 
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persitance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if(scale <= 0)
        {
            scale = 0.0001f;
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frrquency = 1;


                for (int i = 0; i < octaves; i++)
                {
                   float testx = x/scale;
                    float testy = y / scale;

                    float perlin = Mathf.PerlinNoise(testx, testy);
                    noiseMap[x, y] = perlin;
                }
              
            }
        }
        return noiseMap;
    }
}
