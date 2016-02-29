using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ControlLayer : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public Character character   = null;
	public Camera    fieldCamera = null;

	public void OnLayerClicked(BaseEventData bed)
	{
		PointerEventData ped = bed as PointerEventData;
		Vector3 worldPt = fieldCamera.ScreenToWorldPoint(new Vector3(ped.position.x, ped.position.y, fieldCamera.nearClipPlane));

		MoveTo(worldPt);
	}

	public void MoveTo(Vector3 destination)
	{
		m_destination = destination;
		Vector2 direction = (destination - character.transform.position);
		direction.Normalize();
		character.MoveBy(direction, 1);
	}



	//! private, callback or anything don’t be considered to be used outside of this
	#region
	Vector3 m_destination;
	private void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
		float dist = (character.transform.position - m_destination).sqrMagnitude;
		if (dist < 100.0f)
			character.MoveBy(Vector2.zero, 0);
		else
			MoveTo(m_destination);
	}
	#endregion
}
