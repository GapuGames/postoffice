using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GapuEditor : EditorWindow
{
	[MenuItem("Window/Gapu _h")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow<GapuEditor>();
	}

	CharacterInfo m_info;
	Object        m_resource;
	private void OnGUI()
	{
		m_info = EditorGUILayout.ObjectField("Character Info", m_info, typeof(CharacterInfo), false) as CharacterInfo;
		if (GUILayout.Button("New Character") && m_info != null)
		{
			Character chr = m_info.Create ();
		}
		
		m_resource = EditorGUILayout.ObjectField("Resource", m_resource, typeof(Object), false);
		if (GUILayout.Button("Display path") && m_resource != null) Debug.Log(AssetDatabase.GetAssetPath(m_resource));
	}
}
