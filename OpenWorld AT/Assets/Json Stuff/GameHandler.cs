using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameHandler : MonoBehaviour
{
    
    void Start()
    {
       Debug.Log("GameHandler.start");

        //PlayerData playerData = new PlayerData
        //{
        //    postion = new Vector3(5, 0),
        //    health = 80
        //};

        // string json=JsonUtility.ToJson(playerData);

        string json = File.ReadAllText(Application.dataPath + "/test.json"); 
        PlayerData loadedData = JsonUtility.FromJson<PlayerData>(json);

        // File.WriteAllText(Application.dataPath + "MyTest.json", json);




        Debug.Log("postion: " + loadedData.postion);
        Debug.Log("postion: " + loadedData.health);
    } 

    private class PlayerData
    {
        public Vector3 postion;
        public int health;
    }
    
}
