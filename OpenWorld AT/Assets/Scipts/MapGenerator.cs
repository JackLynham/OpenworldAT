using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum Drawmode {NoiseMap , ColourMap, Mesh};
    public Drawmode drawmode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;

    [Range(0,1)]
    public float persistance;
    public float Lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMulti;
    public AnimationCurve meshHeightCurve;

    public TerainType[] Regions;
    public bool update;

 

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed,
            noiseScale,octaves, persistance, Lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentheight = noiseMap[x, y];
                for (int i = 0; i < Regions.Length; i++)
                {
                    if(currentheight<= Regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = Regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if(drawmode == Drawmode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.textureHeightMap(noiseMap));
        }
        else if (drawmode == Drawmode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureColorMap
                (colourMap, mapWidth,mapHeight));
        }
        else if (drawmode == Drawmode.Mesh)
        {
           display.DrawMesh(MeshGen.GenerateTerrainMesh(noiseMap, meshHeightMulti,
               meshHeightCurve), TextureGenerator.TextureColorMap (colourMap, mapWidth, mapHeight));
        }

    }


    private void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (Lacunarity < 1)
        {
            Lacunarity = 1;
        }
        if(octaves < 0)
        {
            Lacunarity = 0;
        }

    }
    [System.Serializable]
    public struct TerainType
    {
        public float height;
        public string name;
        public Color color;
    }
}
