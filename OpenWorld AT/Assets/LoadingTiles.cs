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

    private GameObject testObj;

    public List <GameObject> Hobonobru = new List<GameObject>();
    public int ChunkNum;
    
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i);
            Hobonobru.Add((GameObject)Instantiate(prefab));
        }
        Debug.Log(Hobonobru.Count);
      // UnloadChunks();
    }

    void UnloadChunks()
    {

        Vector3 playerPos = this.gameObject.transform.position;
        //foreach (GameObject tile in Hobonobru)

        //{

        //    Vector3 tilePos = tile.gameObject.transform.position
        //        + (tileSize / 2f);  //Want to find the midpoint of the tile not edge                     

        //    float xDist = Mathf.Abs(tilePos.x - playerPos.x);
        //    float zDist = Mathf.Abs(tilePos.z - playerPos.z);

        //    // We dont want all tiles to be used so if Above the max dist is disabled. 
        //    if (xDist + zDist > maxDist)
        //    {
        //        if (prefab.gameObject.tag == "chunks")
        //        {
        //            Hobonobru.Remove(tile);

        //        }

        //    }
        //    else
        //    {
        //        // tile.SetActive(true);
        //    }
        //}

        for (int i = 0; i <= Hobonobru.Count; i++)
        {
            Vector3 tilePos = Hobonobru[i].gameObject.transform.position
                   + (tileSize / 2f);  //Want to find the midpoint of the tile not edge                     

            float xDist = Mathf.Abs(tilePos.x - playerPos.x);
            float zDist = Mathf.Abs(tilePos.z - playerPos.z);

            // We dont want all tiles to be used so if Above the max dist is disabled. 
            if (xDist + zDist > maxDist)
            {
                if (prefab.gameObject.tag == "chunks")
                {
                    Hobonobru.Remove(Hobonobru[i]);

                }

            }
            else
            {
                // tile.SetActive(true);
            }
        }

    }

    private void Update()
    {
    
          UnloadChunks();
        //Vector3 playerPos = this.gameObject.transform.position;
        //Vector3 tilePos = testObj.gameObject.transform.position;

        //float maxDist = Vector3.Distance(other.position, transform.position);
        //{

        //}

       
    }
}
