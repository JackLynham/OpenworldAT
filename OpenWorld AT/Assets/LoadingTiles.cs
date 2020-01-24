using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> Hobonobru = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            Hobonobru.Add((GameObject)Instantiate(prefab));
        }
        Debug.Log(Hobonobru.Count);
    }

    void UnloadChunks()
    {
        Vector3 playerPos = this.gameObject.transform.position;
        for (int i = 0; i < Hobonobru.Count; i++)
        {
            Vector3 tilePos = Hobonobru[i].gameObject.transform.position
                   + (tileSize / 2f);  //Want to find the midpoint of the tile not edge                     
            if (Vector3.Distance(tilePos, playerPos) >= maxDist)
            {
                Destroy(Hobonobru[i]);
                Hobonobru.RemoveAt(i);
            }
        }

    }

    private void Update()
    {
        UnloadChunks();
    }
}