using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    public Door Target { get { return target as Door; }}

	public override void OnInspectorGUI()
	{
        
        base.OnInspectorGUI();

        var toggle = GUILayout.Toggle(Target.closed, "Closed");
        if (toggle != Target.closed)
        {
            Target.ChangeState(toggle);
        }
	}
}
