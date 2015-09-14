using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif 

[ExecuteInEditMode]
public class SortingNode : MonoBehaviour
{
#if UNITY_EDITOR
	void Start()
	{
		foreach (Transform tf in transform) Resort(tf);
	}

	void Update ()
	{
		if (Selection.activeGameObject == null) return;
		foreach (Transform tf in Selection.transforms)
		{
			Resort(tf);
		}
	}

	private void Resort(Transform tf)
	{
		Transform parent = tf.parent;
		if (parent == null) return;
		if (parent.GetComponent<SortingNode>() == null) return;
		Vector3 pos = tf.position;
		pos.z = pos.y;
		tf.position = pos;
	}

#else 
	void Start()
	{
		Component.Destroy(this);
	}
#endif 
}
