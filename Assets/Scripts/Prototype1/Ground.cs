using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	//! public members
	public GameObject m_tilePrefab = null;
	public uint       m_initCount  = 10;

	//! private members
	private float m_tileWidth = 0;

	// Use this for initialization
	void Awake ()
	{
		SpriteRenderer renderer = m_tilePrefab.GetComponent<SpriteRenderer>();
		Sprite         sprite   = renderer.sprite;
		
		m_tileWidth = sprite.bounds.extents.x * 2;
		
		for (uint i = 0 ; i < m_initCount ; ++i)
		{
			ExpandTile((i%2) == 0);
		}
	}

	private void ExpandTile(bool rightSide)
	{
		GameObject tileGO   = GameObject.Instantiate(m_tilePrefab) as GameObject;
		Transform  newTile  = tileGO.transform;

		Transform  lastTile = (transform.childCount > 0)? transform.GetChild(0):null;
		if (transform.childCount > 0) lastTile = transform.GetChild(rightSide? (transform.childCount - 1):0);

		newTile.SetParent(transform, false);
		if (rightSide) newTile.SetAsLastSibling(); else newTile.SetAsFirstSibling();

		if (lastTile != null)
		{
			Vector3 position = lastTile.position;
			position.x += rightSide? m_tileWidth:-m_tileWidth;
			newTile.position = position;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
