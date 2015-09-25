using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GapuEditor : EditorWindow
{
	[MenuItem("Window/Gapu")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow<GapuEditor>();
	}

	private void OnGUI()
	{
	}
}
