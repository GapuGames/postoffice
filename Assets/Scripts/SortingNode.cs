using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class SortingNode : MonoBehaviour
{
	// Update is called once per frame
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
}
