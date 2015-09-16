using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text;
using System.Collections;

public class GapuEditor : EditorWindow
{
	[MenuItem("Window/Gapu _h")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow<GapuEditor>();
	}

	private static string filePath = Application.dataPath+"/Utilities/Data/Snapshot";
	private void OnGUI()
	{
		if (GUILayout.Button("Save position"))
		{
			StringBuilder snapShot = new StringBuilder();
			Transform[] allTF = Object.FindObjectsOfType<Transform>();
			foreach (Transform tf in allTF)
			{
				snapShot.AppendFormat("{0}:{1}\n", tf.GetPath(), tf.localPosition);
			}
			System.IO.File.WriteAllText(filePath, snapShot.ToString());
		}

		if (GUILayout.Button("Load position"))
		{
			string[] lines = System.IO.File.ReadAllLines(filePath);
			foreach (string line in lines)
			{
				string[] splited = line.Split(':');
				GameObject go = GameObject.Find(splited[0]);
				if (go == null)
				{
					Debug.Log(string.Format("there is no '{0}'", splited[0]));
					continue;
				}
				go.transform.localPosition = Util.ParseVector3(splited[1]);
			}
		}

		if (GUILayout.Button("Convert Prop"))
		{
			foreach (GameObject go in Selection.gameObjects)
			{
				ConvertProperty(go);
			}
		}
	}

	private void ConvertProperty(GameObject go)
	{
		go.gameObject.AddComponent<RectTransform>();
		foreach (Transform i in go.transform)
		{
			RectTransform  rtf      = i.gameObject.AddComponent<RectTransform>();
			SpriteRenderer renderer = rtf.GetComponent<SpriteRenderer>();
			Image          image    = rtf.gameObject.AddComponent<Image>();
			
			image.sprite         = renderer.sprite;
			image.preserveAspect = true;
			rtf.sizeDelta        = Vector2.one;
			
			Component.DestroyImmediate(renderer, true);
		}

		return;
		if (go.transform.childCount == 1)
		{
			Image          parentImg = go.gameObject.AddComponent<Image>();
			Image          childImg  = go.transform.GetChild(0).GetComponent<Image>();
			RectTransform  rtf       = go.gameObject.GetComponent<RectTransform>();
			
			parentImg.sprite         = childImg.sprite;
			parentImg.preserveAspect = true;
			rtf.sizeDelta            = Vector2.one;
			
			GameObject.DestroyImmediate(childImg.gameObject, true);
		}
	}
}
