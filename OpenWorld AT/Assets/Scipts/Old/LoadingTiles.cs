using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadingTiles : MonoBehaviour
{
    public GameObject prefab;
    private GameObject[] tiles;
    [SerializeField]
    private string tileTag;

    [SerializeField]
    private Vector3 tileSize;

    [SerializeField]
    private int maxDist;

    public int currentChunkID;
    float postion;
    public List<GameObject> tileList = new List<GameObject>();
    private List<Vector3> postionsList = new List<Vector3>();
    public ChunkVals CVals;

    Vector3 test;
   
 
    void Start()
    {
       
        prefab.SetActive(true);
        prefab.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 16; i++)
        {
           
            prefab.transform.position += new Vector3(0 + CVals.xSize, 0, 0);
            CVals.TestFuction();

            if (i == 4 || i == 8 || i == 12)
            {
                prefab.transform.position += new Vector3(0 - CVals.xSize * 4, 0, CVals.zSize);

            }
            tileList.Add((GameObject)Instantiate(prefab));
           
            //  SaveData();

        }

        tileList.Add((GameObject)Instantiate(prefab));
         

        Debug.Log(tileList.Count);
    }

    void UnloadChunks()
    {
        Vector3 playerPos = this.gameObject.transform.position;
        Vector3 tilePos;
        for (int i = 0; i < tileList.Count; i++)
        {
            tilePos = tileList[i].gameObject.transform.position
                   + (tileSize / 2f);  //Want to find the midpoint of the tile not edge   
            
            if (Vector3.Distance(tilePos, playerPos) >= maxDist)
            {
              
              Destroy(tileList[i]);
               tileList.RemoveAt(i);
            }

           
         
        }

    }

    private void Update()
    {
        UnloadChunks();
    
        
    }

    void SaveData()
    {
         test = prefab.transform.position;
      
        postionsList.Add(test);
   
    }

    





}