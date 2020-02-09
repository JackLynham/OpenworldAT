using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataManager 
{
   public float [] coord;
   public int size;
   public int ID;

    public DataManager (EndlessTerrain endless )
    {
        coord = new float[2];
        coord[0] = endless.currentChunkX + endless.xOffset;
        coord[1] = endless.currentChunkY + endless.yOffset;

        size = endless.chunkSize;
        ID = endless.chunkID;
       
    }
}
