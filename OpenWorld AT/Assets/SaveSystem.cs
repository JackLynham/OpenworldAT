using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData (Temp temp)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        //DataManager data = new DataManager(endless);
        DataManager data = new DataManager(temp);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static DataManager LoadData()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           DataManager data = formatter.Deserialize(stream) as DataManager;
            stream.Close();

            return data;
        } 
        else
        {
            Debug.LogError("Save File not found " + path);
            return null; 
        }
    }
}
