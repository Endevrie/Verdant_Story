using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(ObjectSnap))]
public class ObjectSnapEditor : Editor {
	
	//private MouseSnap mouseSnap;
	
	
	
	public override void OnInspectorGUI ()
	{
		
		
		//Field for inspectorGUI. Testing~~~ 
		//EditorGUILayout.FloatField("Distance", mouseSnap.distance );
		DrawDefaultInspector();
		
	}
	
	void OnSceneGUI()
	{
		
		ObjectSnap mouseSnap = (ObjectSnap)target;
		
		if (mouseSnap == null)
		{
			return;
		}
		
		Handles.matrix = mouseSnap.transform.localToWorldMatrix;
		Handles.color = Color.red;
		Handles.DrawSolidDisc(mouseSnap.transform.position, mouseSnap.transform.position.normalized, mouseSnap.objRayCastRange);
		
		
		
		
	}
}
