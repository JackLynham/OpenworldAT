using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManger : MonoBehaviour
{
    public GameObject meshes;
    GameObject[,] MapSize = new GameObject[5, 5];


    IEnumerator DetectObjsAround(GameObject[,] objs, int x, int y, int distance)
    {
        int maxX = x + distance;
        int minX = x - distance;
        int maxY = y + distance;
        int minY = y - distance;

        if (minX < 0)
        {
            minX = 0;
        }

        if (maxX > 4)
        {
            maxX = 4;
        }

        if (minY < 0)
        {
            minY = 0;
        }

        if (maxY > 4)
        {
            maxY = 4;
        }

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (x != i || y != j)
                {
                    yield return new WaitForSeconds(0.2f);
                    objs[i, j].transform.position = new Vector3(i, j, 1);
                }
                else
                {
                    objs[i, j].transform.position = new Vector3(i, j, 1);
                }
            }
        }
    }
}