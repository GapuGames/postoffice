using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public GameObject m_weaponPart = null;
	//! public, protected or everything can be used outside of this
	public void LoadCharacter(CharacterInfo info)
	{
		if (info == null) return;
		m_body.LoadBody(info);
		if (m_weaponPart != null && m_body.m_weapon != null)
		{
			GameObject inst = GameObject.Instantiate(m_weaponPart) as GameObject;
			inst.transform.SetParent(m_body.m_weapon, false);
		}
	}

	public void Reload()
	{
		LoadCharacter(m_info);
	}
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region

	[SerializeField]
	CharacterInfo  m_info = null;
	CharacterBody  m_body = null;

	private void Awake()
	{
		m_body = gameObject.GetComponent<CharacterBody>();
		if (m_body == null) m_body = gameObject.AddComponent<CharacterBody>();
	}

	private void Start ()
	{
		LoadCharacter(m_info);
		SetSpriteLayer("MiddleGround");
	}

	public void SetSpriteLayer(string layerName)
	{
		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer i in renderers) i.sortingLayerName = layerName;
	}
	#endregion
}
