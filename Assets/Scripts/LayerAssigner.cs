using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LayerAssigner : MonoBehaviour
{
#if UNITY_EDITOR
	public string m_layer;
	public int    m_order = 0;
	public bool   m_apply = false;

	void Start ()
	{
		Apply();
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
#else 
	void Start ()
	{
		Component.Destroy(this);
	}
#endif
}
