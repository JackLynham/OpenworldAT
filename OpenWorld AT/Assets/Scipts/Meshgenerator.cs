using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Meshgenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verticies;
    int[] triangles;

    public ChunkVals Cvals;
    public LoadingTiles player;

 
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        
    }

    private void Update()
    {
       
    }

    void CreateShape()
    {
        verticies = new Vector3 [(Cvals. xSize + 1) * (Cvals.zSize + 1)];
       
        for (int i =0,  z = 0; z <= Cvals.zSize; z++)
        {
            for (int x = 0; x <= Cvals.xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z *.3f) * 2f;
                verticies[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[Cvals.xSize * Cvals.zSize *6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < Cvals.zSize; z++)
        {
            for (int x = 0; x < Cvals.xSize; x++)
            {
                
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + Cvals.xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + Cvals.xSize + 1;
                triangles[tris + 5] = vert + Cvals.xSize + 2;

                vert++;
                tris += 6;
        
                
            }
                vert++;
          
        }
       
     
    }

   void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals(); //Fixes Lighting on the Mesh  
                                            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            player.currentChunkID = Cvals.chunkID;
        }
    }

}
