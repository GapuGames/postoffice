using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlPad2 : MonoBehaviour
{
	[SerializeField]
	private Image m_pad;
	private Vector2 m_lastAnchor;

	public UnityAction        m_beginAct = null;
	public UnityAction        m_endAct   = null;
	public UnityAction<float> m_dragAct  = null;
	public UnityAction        m_clickAct = null;

	public void BeginDrag(BaseEventData bed)
	{
		PointerEventData ped     = bed as PointerEventData;
		m_lastAnchor             = ped.position;
		m_pad.transform.position = ped.position;
		m_pad.gameObject.SetActive(true);

		if (m_beginAct != null) m_beginAct();
	}
	
	public void EndDrag(BaseEventData bed)
	{
		m_pad.gameObject.SetActive(false);
		if (m_endAct != null) m_endAct();
	}
	
	public void Drag(BaseEventData bed)
	{
		PointerEventData ped          = bed as PointerEventData;
		Vector2          direction    = (ped.position - m_lastAnchor).normalized;
		float            radian       = Mathf.Atan2(direction.y, direction.x);
		m_pad.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * radian);
		
		if (m_dragAct != null) m_dragAct(radian);
	}

	public void Click(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		if (ped.dragging) return;
		
		if (m_clickAct != null) m_clickAct();
	}
}
