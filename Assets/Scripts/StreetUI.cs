using UnityEngine;
using System.Collections;

public class StreetUI : MonoBehaviour
{
	//! public members
	public float  m_accelSlide = 0.5f;
	public Camera m_mainCam = null;

	//! public method
	public void OnBeginDrag()
	{
		m_lastTouchX = Input.mousePosition.x;
	}
	
	public void OnDrag()
	{
		if (m_mainCam == null) return;
		
		float newTouchX = Input.mousePosition.x;
		float slideDelta = newTouchX - m_lastTouchX;
		m_lastTouchX = newTouchX;
		
		m_mainCam.transform.position -= Vector3.right * (slideDelta * m_accelSlide);
	}
	
	public void OnClick()
	{
		if (m_mainCam == null) return;
		
		Ray          ray     = m_mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] results = Physics.RaycastAll(ray);

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

		closestHit.collider.SendMessage("OnHit");
	}

	//! private method

	//! private member
	private float m_lastTouchX = 0;


}
