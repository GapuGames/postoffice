using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileRoard : MonoBehaviour {

	//! public members & methods
	public GameObject m_tilePrefab       = null;
	public uint       m_initTileCount    = 1;
	public float      m_spaceBetweenTile = 1;

	//! private members & methods
	private void ExpandTile(bool expandHead)
	{
		Vector3 edgePos = Vector3.zero;
		if (transform.childCount > 0)
		{
			int childIndex = expandHead? 0:(transform.childCount - 1);
			Transform edgeTile = transform.GetChild(childIndex);
			edgePos = edgeTile.localPosition;
		}
		Vector3 direction = expandHead? Vector3.left : Vector3.right;
		
		GameObject tileGO = GameObject.Instantiate(m_tilePrefab) as GameObject;
		tileGO.transform.SetParent(transform, false);
		tileGO.transform.localPosition = edgePos + (direction * m_spaceBetweenTile);

		if (expandHead) tileGO.transform.SetAsFirstSibling();
		else            tileGO.transform.SetAsLastSibling(); 
	}

	// Use this for initialization
	void Start ()
	{
		uint count = m_initTileCount;
		while (count-- > 0)
		{
			ExpandTile((count%2) == 0);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.A)) ExpandTile(false);
		if (Input.GetKey(KeyCode.D)) ExpandTile(true);
	}
}
