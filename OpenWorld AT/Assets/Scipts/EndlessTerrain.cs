using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EndlessTerrain : MonoBehaviour
{

    public const float maxViewDst = 450;
    public Transform viewer;
    public Material mapMaterial;

    public static Vector2 viewerPos;
    static MapGenerator mapGenerator;
    public int chunkSize;
    int chunksVisible;

    public int chunkID;
    public int currentChunkX;
    public int currentChunkY;
    public  Vector2 viewedChunk;
    public int xOffset;
    public int yOffset;

   // public TerrainChunk terrainChunk;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
   public List<TerrainChunk> terrainChunksVisible = new List<TerrainChunk>();

    void Start()  /*Find the Map make Sure chunk size is not to Big, Calculate Chunk visibliity */
    {
        mapGenerator = FindObjectOfType<MapGenerator>();
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunksVisible = Mathf.RoundToInt(maxViewDst / chunkSize);
    }

    void Update() /*Update the Viewers /Players Position */
    {
        viewerPos = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
      
    }

    //public void SaveData()
    //{
    //    SaveSystem.SaveData(this);
    //}

    public void LoadData()
    {
        DataManager data = SaveSystem.LoadData();

        viewedChunk.x = data.coord[0];
        viewedChunk.y = data.coord[1];

        chunkSize = data.size;
        chunkID = data.ID;
    }

    void UpdateVisibleChunks() /*If the Chunk Isnt visible set it to false */
    {

        for (int i = 0; i < terrainChunksVisible.Count; i++)
        {
            terrainChunksVisible[i].SetVisible(false);
            //SaveData();
            
        }

        terrainChunksVisible.Clear();
        
        /* Works out What chunk the player is in X and Y*/
         currentChunkX = Mathf.RoundToInt(viewerPos.x / chunkSize);
         currentChunkY = Mathf.RoundToInt(viewerPos.y / chunkSize);

        for ( yOffset = -chunksVisible; yOffset <= chunksVisible; yOffset++)
        {
            for ( xOffset = -chunksVisible; xOffset <= chunksVisible; xOffset++)
            {
                /* Determined whats chunk is being seen, if the chunk is inside the list call update chunks to update all chunks #
                 add viewed chunk to dictionairy , else add a new chunk to the list.*/
                 viewedChunk = new Vector2(currentChunkX + xOffset, currentChunkY + yOffset);
                
                if (terrainChunkDictionary.ContainsKey(viewedChunk))
                {
                    terrainChunkDictionary[viewedChunk].UpdateTerrainChunk();
                    if (terrainChunkDictionary[viewedChunk].IsVisible()) ;
                    {
                        terrainChunksVisible.Add(terrainChunkDictionary[viewedChunk]);
                    }
                }
                else
                {
                    chunkID++;
                    terrainChunkDictionary.Add(viewedChunk, new TerrainChunk(viewedChunk, chunkSize, transform, mapMaterial , chunkID));
                    
                }

            }
        }
    }


    public class TerrainChunk :MonoBehaviour
    {

        GameObject meshObject;
        Vector2 position;
        Bounds bounds;
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        public bool visible;
        public bool active;

        // EndlessTerrain endless;
       

        

        public TerrainChunk(Vector2 coord, int size, Transform parent, Material material,  int chunkID)
        {
            EndlessTerrain endless = gameObject.GetComponent<EndlessTerrain>();
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            //filePath = Application.streamingAssetsPath + "/ChunkData.json";//jsonString = File.ReadAllText(filePath); //Test data = JsonUtility.FromJson<Test>(jsonString); //string newData = JsonUtility.ToJson(data); //Debug.Log(newData);
            
            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>( );
            meshRenderer.material = material;
            meshObject.transform.position = positionV3;
            meshObject.transform.parent = parent;
            
          
            SetVisible(false);

            mapGenerator.RequestMapData(OnMapDataReceived);
        }

        void OnMapDataReceived(MapData mapData)
        {
            mapGenerator.RequestMeshData(mapData, OnMeshDataReceived);
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            meshFilter.mesh = meshData.CreateMesh();
           
        }


        public void UpdateTerrainChunk()
        {
            float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPos));
            visible = viewerDstFromNearestEdge <= maxViewDst;
            SetVisible(visible);
           
        }

        public void SetVisible(bool visible)
        {
             IsVisible();

            if (!visible)
            {
                NotVisible();
            }
            else if (visible)
            {
                // Instantiate 
                active = true;

            }
        }

        public bool IsVisible()
        {
            //return true; 
            return meshObject.activeSelf;
           return meshObject;
           
            
        }

        public void NotVisible()
        {
            if(active)
            {
             //   endless.terrainChunksVisible.Remove(meshObject.GetComponent<TerrainChunk>());
               //Destroy(meshObject);
            }
           

        }


    }

  [System.Serializable]
    public class Test
    {
        public float coord;
        int size;
        int seed;

    }
}