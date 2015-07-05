using UnityEngine;
using System.Collections;

public class BuildingEditor : MonoBehaviour {

	//! public members & methods
	public GameObject m_tileRoard = null;
	public Camera     m_eventCam  = null;
	public GameObject m_building  = null;

	public void OnBeginDrag()
	{

	}
	
	public void OnDrag()
	{
		Ray ray = m_eventCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] results = Physics.RaycastAll(ray, Mathf.Infinity, m_tileMask);
		if (results.Length == 0) return;

		RaycastHit result = results[0];
		if (result.collider == m_lastTile) return;
		
		Debug.Log("test1");

		m_lastTile = result.collider;
		m_building.transform.position = m_lastTile.transform.position;
	}
	
	public void OnPointerClick()
	{
	}

	//! private members & methods
	private Collider m_lastTile  = null;
	private int      m_tileMask = 0;

	// Use this for initialization
	private void Awake()
	{
		m_tileMask |= (1 << LayerMask.NameToLayer("Tile"));
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}

}
