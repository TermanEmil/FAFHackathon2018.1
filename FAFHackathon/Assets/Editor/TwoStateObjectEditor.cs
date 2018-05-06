using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TwoStateObject))]
public class TwoStateObjectEditor : Editor
{
    public TwoStateObject Target { get { return target as TwoStateObject; }}

	public override void OnInspectorGUI()
	{
        
        base.OnInspectorGUI();

        var toggle = GUILayout.Toggle(Target.active, "Active");
        if (toggle != Target.active)
        {
            Target.CmdSetState(toggle);
        }
	}
}
