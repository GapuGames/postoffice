using UnityEngine;
using UnityEditor;
using System.Collections;

public class CustomUtility : MonoBehaviour
{
	public GameObject targetObject;
	public int layerID;
}

[CustomEditor(typeof(CustomUtility))]
public class CustomUtilityEditor : Editor
{
	string layerName = string.Empty;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		CustomUtility customUtility = target as CustomUtility;
		GameObject go = customUtility.targetObject;

		{
			SpriteRenderer renderer = go.GetComponentInChildren<SpriteRenderer>();

			layerName = EditorGUILayout.TextField("layer name", layerName);
			if (GUILayout.Button("Display layer id"))
			{
				Debug.Log(LayerMask.NameToLayer(layerName));
				Debug.Log(renderer.sortingLayerName);
				Debug.Log(renderer.sortingLayerID);
			}
		}

		if (GUILayout.Button("Set layer ID"))
		{
			SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer i in renderers)
			{
				i.sortingLayerID = customUtility.layerID;
			}
		}

		if (GUILayout.Button("Z<->Order"))
		{
			SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer i in renderers)
			{
				Vector3 pos = i.transform.localPosition;

				float z = -pos.z;
				pos.z = i.sortingOrder;
				i.sortingOrder = (int)z;

				i.transform.localPosition = pos;
			}
		}
	}
	
}