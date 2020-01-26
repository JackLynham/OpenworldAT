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

    public List<GameObject> tileList = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            tileList.Add((GameObject)Instantiate(prefab));
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
        }

    }

    private void Update()
    {
        UnloadChunks();
    }
}