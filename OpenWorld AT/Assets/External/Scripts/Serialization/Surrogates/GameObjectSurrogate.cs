using System.Runtime.Serialization;
using UnityEngine;

sealed class GameObjectSurrogate : ISerializationSurrogate
{
	
	// Method called to serialize a Vector3 object
	public void GetObjectData(System.Object obj,
	                          SerializationInfo info, StreamingContext context)
    {
		
		GameObject go = (GameObject) obj;
	}
	
	// Method called to deserialize a Vector3 object
	public object SetObjectData(object obj,
	                                   SerializationInfo info, StreamingContext context,
	                                   ISurrogateSelector selector)
    {
		
		GameObject go = (GameObject) obj;
		return obj;
	}
}