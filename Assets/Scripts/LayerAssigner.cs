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
	public bool   m_applyLayer = false;
	public bool   m_applyOrder = false;
	
	void Update()
	{
		if (m_applyLayer)
		{
			m_applyLayer = false;
			ApplyLayer();
		}
		
		if (m_applyOrder)
		{
			m_applyOrder = false;
			ApplyOrder();
		}
	}
	
	void ApplyLayer()
	{
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer sprite in sprites)
		{
			sprite.sortingLayerName = m_layer;
		}
	}
	
	void ApplyOrder()
	{
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer sprite in sprites)
		{
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
