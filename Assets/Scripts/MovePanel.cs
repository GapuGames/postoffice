using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MovePanel : MonoBehaviour
{
	public  Transform m_ball = null;
	public  UnityAction<Vector2, float> m_movingCallback = null;
	public  float     m_maxDistance = 25.0f;
	private Vector2   m_originPosition;
	
	public void OnDrag(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		if (!ped.dragging) return;
		if (!ped.IsPointerMoving()) return;
		
		Vector2 delta    = ped.position - m_originPosition;
		Vector2 normal   = delta.normalized;
		float   distance = delta.magnitude;
		float   length   = (distance > m_maxDistance)? m_maxDistance : distance;
		
		m_ball.position  = m_originPosition + (normal * length);
		
		if (m_movingCallback != null) m_movingCallback(normal, length / m_maxDistance);
	}
	
	public void OnEndDrag()
	{
		m_ball.position = m_originPosition;
		if (m_movingCallback != null) m_movingCallback(Vector2.zero, 0);
	}
	
	private void Start ()
	{
		m_originPosition = m_ball.position;
	}	
}
