﻿using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
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

	string m_path = Application.dataPath + "/Utilities/Data/Snapshot";
	IEnumerator m_step = null;
	string m_layoutName = "";

	private void OnGUI()
	{
		if (GUILayout.Button("Convert scene step1"))
		{
			if (m_step == null) m_step = ConvertScene();
			if (!m_step.MoveNext()) m_step = null;
		}
		if (m_step != null) GUILayout.Label(m_step.Current as string);

		if (GUILayout.Button("Store positions"))
		{
			GameObject[]  goList = GameObject.FindObjectsOfType<GameObject>();
			StringBuilder sb     = new StringBuilder();
			foreach (GameObject go in goList) sb.AppendLine(go.transform.localPosition.ToString());

			System.IO.File.WriteAllText(m_path, sb.ToString());
		}

		if (GUILayout.Button("Restore positions"))
		{
			GameObject[]  goList = GameObject.FindObjectsOfType<GameObject>();
			string[] posList = System.IO.File.ReadAllLines(m_path);
			for (int i = 0 ; i < goList.Length ; ++i)
			{
				goList[i].transform.localPosition = Util.ParseVector3(posList[i]);
			}
		}

		GUILayout.BeginHorizontal();
		m_layoutName = GUILayout.TextField(m_layoutName, GUILayout.MinWidth(100));
		if (GUILayout.Button("Set canvas layout"))
		{
			System.Action<GameObject> converter = (go) => 
			{
				Canvas[] canvasList = go.GetComponentsInChildren<Canvas>();
				foreach (Canvas canvas in canvasList) canvas.sortingLayerName = m_layoutName;
			};

			foreach (GameObject go in Selection.gameObjects) converter(go);
		}
		GUILayout.EndHorizontal();

		if (GUILayout.Button("Assign y->z in canvas"))
		{
			System.Action<GameObject> func = (go) => 
			{
				Canvas[] canvasList = go.GetComponentsInChildren<Canvas>();
				foreach (Canvas canvas in canvasList)
				{
					Vector3 pos = canvas.transform.localPosition;
					pos.z = pos.y;
					canvas.transform.localPosition = pos;;
				}
			};
			
			foreach (GameObject go in Selection.gameObjects) func(go);
		}
	}

	private IEnumerator ConvertScene()
	{
		yield return "ready for converting";

		GameObject[] goList   = GameObject.FindObjectsOfType<GameObject>();
		GameObject[] propList = Resources.LoadAll<GameObject>("Prefab/Prop");
		Vector3[]    posList  = new Vector3[goList.Length];
		
		//! save positions
		for (int i = 0 ; i < goList.Length ; ++i)
		{
			posList[i] = goList[i].transform.localPosition;
		}

		//! convert props
		foreach (GameObject obj in propList) ConvertProps( obj );

		yield return "step1 done, push again please";

		//! load positions
		for (int i = 0 ; i < goList.Length ; ++i)
		{
			goList[i].transform.localPosition = posList[i];
		}
		
		yield return "step2 done, push again for last";

		//! restruct props
		foreach (GameObject obj in propList) RestructProps( obj );
	}

	private void ConvertProps(GameObject go)
	{
		go.gameObject.AddComponent<RectTransform>();
		Canvas canvas = go.AddComponent<Canvas>();
		canvas.overrideSorting = true;
		foreach (Transform i in go.transform)
		{
			RectTransform  rtf      = i.gameObject.AddComponent<RectTransform>();
			SpriteRenderer renderer = rtf.GetComponent<SpriteRenderer>();
			Image          image    = rtf.gameObject.AddComponent<Image>();
			
			image.sprite         = renderer.sprite;
			image.preserveAspect = true;

			Component.DestroyImmediate(renderer, true);
		}
	}
	
	private void RestructProps(GameObject go)
	{
		if (go.transform.childCount == 1)
		{
			Image          parentImg = go.gameObject.AddComponent<Image>();
			Image          childImg  = go.transform.GetChild(0).GetComponent<Image>();
			RectTransform  rtf       = go.gameObject.GetComponent<RectTransform>();
			
			parentImg.sprite         = childImg.sprite;
			parentImg.preserveAspect = true;
			parentImg.SetNativeSize();
			rtf.sizeDelta = rtf.sizeDelta/parentImg.sprite.pixelsPerUnit;
			
			GameObject.DestroyImmediate(childImg.gameObject, true);
		}
	}
}
