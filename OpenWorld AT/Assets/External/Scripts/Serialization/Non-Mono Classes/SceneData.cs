using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveGame
{
	
	public string savegameName = "New SaveGame";
	public List<SceneObject> sceneObjects = new List<SceneObject>();

	public SaveGame()
    {

	}

	public SaveGame(string save, List<SceneObject> list) {
		savegameName = save;
		sceneObjects = list;
	}
}
