using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessTerrain : MonoBehaviour
{

    public const float maxViewDst = 450;
    public Transform viewer;
    public Material mapMaterial;
    static MapGenerator mapGenerator;
    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisible;

 

    Dictionary<Vector2, TerrainChunk> ChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisible = new List<TerrainChunk>();

    void Start()
    {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisible = Mathf.RoundToInt(maxViewDst / chunkSize);
        mapGenerator = FindObjectOfType<MapGenerator>();
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {

        for (int i = 0; i < terrainChunksVisible.Count; i++)
        {
            terrainChunksVisible[i].SetVisible(false);
        }

        terrainChunksVisible.Clear();

        int currentChunkX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunksVisible; yOffset <= chunksVisible; yOffset++)
        {
            for (int xOffset = -chunksVisible; xOffset <= chunksVisible; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkX + xOffset, currentChunkY + yOffset);

                if (ChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    ChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    if (ChunkDictionary[viewedChunkCoord].IsVisible())
                    {
                        terrainChunksVisible.Add(ChunkDictionary[viewedChunkCoord]);
                    }
                }
                else
                {
                    ChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform,mapMaterial));
                }

            }
        }
    }

    public class TerrainChunk
    {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;
        MeshRenderer MeshRenderer;
        MeshFilter MeshFilter;

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject(" TerrainChunk");
            MeshRenderer = meshObject.AddComponent<MeshRenderer>();
            MeshFilter = meshObject.AddComponent<MeshFilter>();
            MeshRenderer.material = material;

            meshObject.transform.position = positionV3;
            meshObject.transform.parent = parent;
            SetVisible(false);

            mapGenerator.RequestMapData(OnMapDataRecived);
        }

        void OnMapDataRecived(Mapdata mapdata)
        {
            mapGenerator.RequestMeshData(mapdata, OnMeshData);
        }
        void OnMeshData (MeshData meshdata)
        {
            MeshFilter.mesh = meshdata.CreateMesh();
        }

        public void UpdateTerrainChunk()
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDstFromNearestEdge <= maxViewDst;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }

    }
}
