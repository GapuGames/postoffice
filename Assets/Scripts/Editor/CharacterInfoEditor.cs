using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Text;
using System.Collections;

[CustomEditor(typeof(CharacterInfo))]
public class CharacterInfoEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Create"))
		{
			CharacterInfo info = target as CharacterInfo;
			info.Create();
		}
	}
}