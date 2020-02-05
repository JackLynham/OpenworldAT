using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap, ColourMap, Mesh };
    public DrawMode drawMode;

   public const int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    Queue<MapThreadInfo<Mapdata>> mapThreads = new Queue<MapThreadInfo<Mapdata>>();

    Queue<MapThreadInfo<MeshData>> meshThreads = new Queue<MapThreadInfo<MeshData>>();

    public void DrawMapEditor()
    {
        Mapdata mapdata = GenerateMap();
       //Drawing Maps in Editor 
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapdata.heightMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(mapdata. colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGen.GenerateTerrainMesh( mapdata.heightMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(mapdata.colourMap, mapChunkSize, mapChunkSize));
        }
    }

    public void RequestMapData(Action<Mapdata> callback)
    {
        ThreadStart thread = delegate
        {
            MapDataThread(callback);
        };

        new Thread(thread).Start();
    }


    void MapDataThread(Action<Mapdata>callback)
    {
        Mapdata mapdata = GenerateMap();
        lock (mapThreads)
        {
            mapThreads.Enqueue(new MapThreadInfo<Mapdata>(callback, mapdata));

        }
    }


    public void RequestMeshData(Mapdata mapdata, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDatathread(mapdata, callback);
        };
    }

    void MeshDatathread(Mapdata mapdata, Action<MeshData> callback)
    {

        MeshData mesh = MeshGen.GenerateTerrainMesh(mapdata.heightMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail);
        lock(meshThreads)
        {
            meshThreads.Enqueue(new MapThreadInfo<MeshData>(callback, mesh));
        }
    }


    private void Update()
    {
        if(mapThreads.Count >0)
        {
            for (int i = 0; i < mapThreads.Count; i++)
            {
                MapThreadInfo<Mapdata> threadInfo = mapThreads.Dequeue();
                threadInfo.callback(threadInfo.paramater);
            }
        }

        if(meshThreads.Count > 0)
        {
            for (int i = 0; i < meshThreads.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshThreads.Dequeue();
                threadInfo.callback(threadInfo.paramater);
            }
        }


    }


    Mapdata GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        return new Mapdata(noiseMap, colourMap);
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }


    struct MapThreadInfo<t>
    {
        public readonly Action<t> callback;
        public readonly t paramater;

        public MapThreadInfo(Action<t> callback, t paramater)
        {
            this.callback = callback;
            this.paramater = paramater;

        }
    }
}



[System.Serializable]  //This is for When Creating Terrain in Editor 
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

public struct Mapdata
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public Mapdata (float[,] heightMap, Color[]colourMap )
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }

   
}

