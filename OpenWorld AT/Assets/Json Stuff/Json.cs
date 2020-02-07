using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Json : MonoBehaviour
{
    private void Awake()
    {
        SaveObject save = new SaveObject
        {
            goldAmount = 5
        };
      string json= JsonUtility.ToJson(save);

        Debug.Log(json);
      SaveObject LoadedSaveObject=  JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(LoadedSaveObject.goldAmount);
    }

    private class SaveObject
    {
        public int goldAmount;
    }

}

