using UnityEngine;
using UnityEngine.Events;	
using System.Collections;

public class StreetUI : MonoBehaviour
{
	//! public members
	public float  m_accelSlide = 0.5f;
	public Camera m_mainCam = null;

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

	public void OnTestBtn()
	{
		UnityAction openNotice = null;
		UnityAction openChoice = null;
		openNotice = ()=> { GameUI.Notice("title test", "desc test", openChoice); };
		openChoice = ()=> { GameUI.Choice("title test", "desc test", openNotice); };
		openNotice();
	}

	//! private method

	//! private member
	private float m_lastTouchX = 0;
	private int    m_worldLayer = 0;
	private string m_msgName   = "OnHit";


}
