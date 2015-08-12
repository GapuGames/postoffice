using UnityEngine;
using System.Collections;

public class FieldCamera : MonoBehaviour
{

	public GameObject m_target = null;
	private void LateUpdate()
	{
		if (m_target == null) return;
		Vector2 pos2 = m_target.transform.position;
		transform.position = pos2;
	}

}
