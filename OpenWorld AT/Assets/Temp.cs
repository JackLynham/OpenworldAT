using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public Vector3 pos;
    public int size;
    public int ID;
    GameObject plane;
    public void SavePlayer()
    {
        SaveSystem.SaveData(this);
    }


    private void Start()
    {
       // plane = new GameObject();
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = new Vector3(10, 0, 10);

    }
    public void LoadData()
    {
        DataManager data = SaveSystem.LoadData();
        pos.x = data.coord[0];
        pos.y = data.coord[1];
        pos.z = data.coord[2];
        plane.transform.position = pos;

        size = data.size;
        ID = data.ID;

    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Destroy(plane);
        }

        if (Input.GetKeyDown("k"))
        {
            plane = GameObject.CreatePrimitive(PrimitiveType.Plane);



             LoadData();
        }


    }

} 
