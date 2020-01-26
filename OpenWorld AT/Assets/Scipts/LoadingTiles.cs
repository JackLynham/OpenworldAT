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

    float postion;
    public List<GameObject> tileList = new List<GameObject>();
    private List<Vector3> postionsList = new List<Vector3>();

    public Meshgenerator mesh;
    void Start()
    {
        prefab.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 16; i++)
        {
            prefab.transform.position += new Vector3(0+ mesh.xSize, 0, 0);

            if (i == 4 || i == 8 || i == 12)
            {
                prefab.transform.position += new Vector3(0 - mesh.xSize*4, 0, +mesh.zSize);

            }

            tileList.Add((GameObject)Instantiate(prefab));
          //  SaveData();
          
        }


        Debug.Log(tileList.Count);
    }

    void UnloadChunks()
    {
        Vector3 playerPos = this.gameObject.transform.position;
        for (int i = 0; i < tileList.Count; i++)
        {
            Vector3 tilePos = tileList[i].gameObject.transform.position
                   + (tileSize / 2f);  //Want to find the midpoint of the tile not edge                     
            if (Vector3.Distance(tilePos, playerPos) >= maxDist)
            {
                Destroy(tileList[i]);
                tileList.RemoveAt(i);
            }

            //if (Vector3.Distance(tilePos, playerPos) <= maxDist && !prefab.activeInHierarchy)
            //{
                
            //    tileList.Add((GameObject)Instantiate(prefab));
            //    prefab.transform.position = postionsList[0];
            //    postionsList.Remove(postionsList[0]);
            //}
        }

    }

    private void Update()
    {
        UnloadChunks();
        //LoadData();
    }

    //void SaveData()
    //{
    //   Vector3 test = prefab.transform.position;
    //    postionsList.Add(test);
    //}

    //void LoadData()
    //{

    //}




}