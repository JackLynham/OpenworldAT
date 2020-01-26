using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadingTiles : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefab2;
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

    Vector3 test2;
    Vector3 test;
    public Meshgenerator mesh;
    void Start()
    {
        prefab.SetActive(true);
        prefab.transform.position = new Vector3(0, 0, 0);
        prefab2.transform.position = new Vector3(0, 0, 0);
        //for (int i = 0; i < 16; i++)
        //{
        //    prefab.transform.position += new Vector3(0+ mesh.xSize, 0, 0);

        //    if (i == 4 || i == 8 || i == 12)
        //    {
        //        prefab.transform.position += new Vector3(0 - mesh.xSize*4, 0, +mesh.zSize);

        //    }

        //    //  SaveData();

        //}

            tileList.Add((GameObject)Instantiate(prefab));
            tileList.Add((GameObject)Instantiate(prefab2));

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
                //Destroy(tileList[i]);
                //tileList.RemoveAt(i);

                prefab.SetActive(false);
            
               
            }
            if (Vector3.Distance(tilePos, playerPos) >= maxDist)
            {
                if(!prefab.activeInHierarchy)
                {
                    // tileList.Add((GameObject)Instantiate(prefab));
                    prefab.SetActive(true);
                }

                if (!prefab2.activeInHierarchy)
                {
                    //  tileList.Add((GameObject)Instantiate(prefab2));
                    prefab.SetActive(true);
                }
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

    void SaveData()
    {
         test = prefab.transform.position;
         test2 = prefab2.transform.position;
        postionsList.Add(test);
        postionsList.Add(test2);
    }

    void LoadData()
    {

    }




}