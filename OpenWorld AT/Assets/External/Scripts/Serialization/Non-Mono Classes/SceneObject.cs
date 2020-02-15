
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SceneObject//This class hold all the data of a GameObject in the scene which has an ObjectIdentifier component. 
{
	public string name;
	public string prefabName; //The values from the OI component are mirrored here, along with misc. 
	public string id;
	public string idParent;

	public bool active;
	public Vector3 position;
	public Vector3 localScale;
	public Quaternion rotation;

	public List<ObjectComponent> objectComponents = new List<ObjectComponent>();
}



