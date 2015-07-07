using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class BuildingEditUI : MonoBehaviour {

	//! public members
	public Camera      m_mainCam   = null;
	public Building    m_building  = null;
	public GameObject  m_worldRoot = null;
	public GameObject  m_tileRoardPrefab = null;

	//! private members
	private float      m_accelSlide   = 0.1f;
	private Collider   m_lastTile     = null;
	private int        m_tileMask     = 0;
	private int        m_edittingMask = 0;
	private bool       m_onMove       = false;
	private float      m_lastTouchX   = 0;
	private GameObject m_tileRoard    = null;
	private Coroutine  m_slideCamCo   = null;

	//! public methods
	//! call this method before "Start" called
	public void Init(Camera mainCam, Building targetBuilding, GameObject worldRoot)
	{
		m_mainCam   = mainCam;
		m_building  = targetBuilding;
		m_worldRoot = worldRoot;
	}

	//! callback for begin of screen dragging
	public void OnBeginDrag()
	{
		//! initialize touch position
		m_lastTouchX = Input.mousePosition.x;

		//! calculate screen touch point to world ray
		Ray ray = m_mainCam.ScreenPointToRay(Input.mousePosition);
		
		//! raycasting to find is ray on editting building
		RaycastHit[] results = Physics.RaycastAll(ray, Mathf.Infinity, m_edittingMask);
		m_onMove = (results.Length != 0);
	}
	
	public void OnDrag()
	{
		if (m_onMove) MoveBuilding();
		else
		{
			float newTouchX = Input.mousePosition.x;
			float slideDelta = newTouchX - m_lastTouchX;
			m_lastTouchX = newTouchX;
			MoveCamera(slideDelta);
		}
	}

	public void OnEndDrag()
	{
		m_onMove = false;
	}
	
	public void OnPointerClick(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		if (ped.dragging) return;

		//! calculate screen touch point to world ray
		Ray ray = m_mainCam.ScreenPointToRay(Input.mousePosition);
		
		//! raycasting to find is ray on editting building
		RaycastHit[] results = Physics.RaycastAll(ray, Mathf.Infinity, m_edittingMask);
		if (results.Length == 0) EndEditting();
	}

	public void OnRightSidebarEnter()
	{
		if (!m_onMove) return;
		m_slideCamCo = StartCoroutine(SlideCamera(-0.5f));
	}

	public void OnLeftSidebarEnter()
	{
		if (!m_onMove) return;
		m_slideCamCo = StartCoroutine(SlideCamera(0.5f));
	}

	public void OnSidebarExit()
	{
		if (m_slideCamCo != null) StopCoroutine(m_slideCamCo);
		m_slideCamCo = null;
	}

	//! private methods

	private IEnumerator SlideCamera(float slideDelta)
	{
		while (true)
		{
			MoveCamera(slideDelta);
			yield return null;
		}
	}

	// Use this for initialization
	private void Awake()
	{
		m_tileMask     |= (1 << LayerMask.NameToLayer("Tile"));
		m_edittingMask |= (1 << LayerMask.NameToLayer("Editting"));
	}

	//! setting up for editting building
	private void Start()
	{
		m_building.gameObject.layer = LayerMask.NameToLayer("Editting");
		m_tileRoard = GameObject.Instantiate(m_tileRoardPrefab) as GameObject;
		m_tileRoard.transform.SetParent(m_worldRoot.transform, false);
	}
	
	//! processign for cleaning up and finishing editting.
	private void EndEditting()
	{
		m_building.gameObject.layer = LayerMask.NameToLayer("Building");
		GameObject.Destroy(m_tileRoard);
		GameObject.Destroy(gameObject);
	}
	
	//! processing for finding new tile and replacing the building
	private void MoveBuilding()
	{
		//! calculate screen touch point to world ray
		Ray ray = m_mainCam.ScreenPointToRay(Input.mousePosition);
		
		//! raycasting over all tile objects
		RaycastHit[] results = Physics.RaycastAll(ray, Mathf.Infinity, m_tileMask);
		if (results.Length == 0) return;
		
		//! get a tile (it doesn't need to be lined up by distance)
		RaycastHit result = results[0];
		
		//! is it new tile?
		if (result.collider == m_lastTile) return;
		
		//! if it's new picked tile, move the building onto new tile and replace the variable to new tile object
		m_lastTile = result.collider;
		m_building.transform.position = m_lastTile.transform.position;
	}

	//! processing for moving camera
	private void MoveCamera(float slideDelta)
	{
		if (m_mainCam == null) return;
		
		m_mainCam.transform.position -= Vector3.right * (slideDelta * m_accelSlide);
	}
}

/* temporary note










 */
