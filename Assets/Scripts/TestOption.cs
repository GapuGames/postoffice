using UnityEngine;
using System.Collections;

public class TestOption : MonoBehaviour
{
	public GameObject  m_target = null;
	public FieldCamera m_camera = null;

	public void DettachCamera()
	{
		if (m_camera.m_target != null) m_camera.m_target = null;
		else m_camera.m_target = m_target;
	}
}
