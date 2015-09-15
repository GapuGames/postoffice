using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpriteToImage : MonoBehaviour
{
	//! public, protected or everything can be used outside of this



	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}
	#endregion
}

[CustomEditor(typeof(SpriteToImage))]
public class SpriteToImageEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		if (!GUILayout.Button("Start convert")) return;

		SpriteToImage sti = target as SpriteToImage;
		GameObject    go  = sti.gameObject;


		SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer i in renderers)
		{
			if (!i.enabled) continue;

			Image image = i.gameObject.AddComponent<Image>();
			image.sprite = i.sprite;
			Component.DestroyImmediate(i);

			RectTransform rtf = image.transform as RectTransform;
			Vector2 size  = rtf.sizeDelta;
			size.x *= rtf.localScale.x;
			size.y *= rtf.localScale.y;
			rtf.sizeDelta = size;
			rtf.localScale = Vector3.one;
		}

		Action<Transform> childrenArranger = null;
		childrenArranger = (parent) =>
		{
			List<Transform> children = new List<Transform>();
			foreach (Transform child in parent) children.Add(child);
			children.Sort((l,r)=>{return r.localPosition.z.CompareTo(l.localPosition.z);});

			int index = 0;
			foreach (Transform child in children)
			{
				Vector3 pos = child.localPosition;
				pos.z = 0;
				child.localPosition = pos;
				child.SetSiblingIndex(index++);
				childrenArranger(child);
			}
		};

		childrenArranger(go.transform);

		go.AddComponent<Canvas>();

	}
}