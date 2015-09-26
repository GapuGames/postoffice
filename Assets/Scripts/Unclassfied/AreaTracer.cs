using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AreaTracer : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public float     m_radius = 3;
	public Transform m_target = null;
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
#if UNITY_EDITOR
	private void OnGUI ()
	{
		Vector3 center = transform.position;
		Vector3 length = Vector3.up * 20;
		Vector3 top    = center + length;
		Vector3 bottom = center - length;
		Vector3 half   = Vector3.right*m_radius;

		Debug.DrawLine(top + half, bottom + half);
		Debug.DrawLine(top - half, bottom - half);
	}
#endif
	
	// Update is called once per frame
	private void LateUpdate ()
	{
		if (m_target == null) return;

		float deltaX = m_target.position.x - transform.position.x;
		if (m_radius*m_radius > deltaX*deltaX) return;

		float gapX   = deltaX + ((deltaX < 0)? +m_radius : -m_radius);
		transform.position += Vector3.right*gapX;
	}
	#endregion
}
