
using System.Collections.Generic;

[System.Serializable]
public class ObjectComponent //The ObjectComponent class holds all data of a gameobject's component. 
                             //The Dictionary holds the actual data of a component; A field's name as key and the corresponding value (object) as value.
{
	public string componentName;
	public Dictionary<string,object> fields;
}