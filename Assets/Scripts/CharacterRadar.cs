using UnityEngine;
using System.Collections;

public class CharacterRadar : MonoBehaviour
{
	public GameObject m_reciever = null;
	private void OnTriggerEnter2D(Collider2D other)
	{
		Transform otherParent = other.transform.parent;
		m_reciever.SendMessage("OnHitBoxRadar", otherParent.gameObject);
	}
}
