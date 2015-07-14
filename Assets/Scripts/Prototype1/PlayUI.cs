using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;	
using System.Collections;

public class PlayUI : MonoBehaviour
{
	//! public members
	public LayerMask m_hitMask;
	public string    m_msgName  = "OnHit";
	public Camera    m_mainCam  = null;
	
	//! private members

	//! public method
	public void Awake()
	{
	}

	public void OnBeginDragBackground()
	{
	}
	
	public void OnDragBackground()
	{
		if (m_mainCam == null) return;
	}
	
	public void OnClickBackground(BaseEventData bed)
	{
		if (m_mainCam == null) return;

		PointerEventData ped = bed as PointerEventData;
		if (ped.dragging) return;

		Ray          ray     = m_mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] results = Physics.RaycastAll( ray, Mathf.Infinity, m_hitMask );

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
	
	private static GameObject LoadPrefab(string path)
	{
		return Resources.Load(path) as GameObject;
	}

	//! private method


}
