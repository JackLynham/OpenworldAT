using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class Test
{
    public int level;
    public float timeElapsed;
    public string playerName;

 void Start()
    {
        Test myObject = new Test();
        myObject.level = 1;
        myObject.timeElapsed = 47.5f;
        myObject.playerName = "Dr Charles Francis";

        string json = JsonUtility.ToJson(myObject);

        myObject = JsonUtility.FromJson<Test>(json);
    }



}
