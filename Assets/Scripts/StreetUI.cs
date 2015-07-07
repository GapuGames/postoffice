using UnityEngine;
using UnityEngine.Events;	
using System.Collections;

public class StreetUI : MonoBehaviour
{
	//! public members
	public float  m_accelSlide = 0.5f;
	public Camera m_mainCam = null;
	
	//! private members
	private float m_lastTouchX  = 0;
	private int    m_worldLayer = 0;
	private string m_msgName    = "OnHit";

	//! public method
	public void Awake()
	{
		m_worldLayer |= (1 << LayerMask.NameToLayer("World"));
	}

	public void OnBeginDragBackground()
	{
		m_lastTouchX = Input.mousePosition.x;
	}
	
	public void OnDragBackground()
	{
		if (m_mainCam == null) return;
		
		float newTouchX = Input.mousePosition.x;
		float slideDelta = newTouchX - m_lastTouchX;
		m_lastTouchX = newTouchX;

		m_mainCam.transform.position -= Vector3.right * (slideDelta * m_accelSlide);
	}
	
	public void OnClickBackground()
	{
		if (m_mainCam == null) return;
		Debug.Log("OnClickBackground");

		Ray          ray     = m_mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] results = Physics.RaycastAll( ray, Mathf.Infinity, m_worldLayer );

		if (results.Length == 0) return;

		Vector3    originPos   = ray.origin;
		RaycastHit closestHit  = results[0];
		float      closestDist = (closestHit.point - originPos).sqrMagnitude;

		foreach (RaycastHit hit in results)
		{
			float dist = (hit.point - originPos).sqrMagnitude;
			if (dist < closestDist)
			{
				closestDist = dist;
				closestHit  = hit;
			}
		}

		closestHit.collider.SendMessage(m_msgName);
	}

	public void OnEditBtn()
	{
		Building building = GameObject.Find("World/Building").GetComponent<Building>();
		GameObject editUIPrefab = LoadPrefab("Prefab/Unclassified/BuildingEditUI");
		GameObject editUIGO = GameObject.Instantiate(editUIPrefab) as GameObject;
		BuildingEditUI editUI = editUIGO.GetComponent<BuildingEditUI>();
		editUI.Init(m_mainCam, building, GameObject.Find("World"));
	}
	
	private static GameObject LoadPrefab(string path)
	{
		return Resources.Load(path) as GameObject;
	}

	//! private method


}
