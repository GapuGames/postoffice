using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class LayerAssigner : MonoBehaviour
{
	public string m_layer;
	public int    m_order = 0;
	public bool   m_apply = false;
	void Start ()
	{
		if (Application.isPlaying) Component.Destroy(this);
		else Apply();
	}

	void Update()
	{
		if (m_apply)
		{
			m_apply = false;
			Apply();
		}
	}

	void Apply()
	{
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer sprite in sprites)
		{
			sprite.sortingLayerName = m_layer;
			sprite.sortingOrder = m_order;
		}
	}
}
