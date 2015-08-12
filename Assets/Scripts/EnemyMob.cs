using UnityEngine;
using System.Collections;

public class EnemyMob : MonoBehaviour
{
	public float      m_speed = 0.5f;
	private Rigidbody m_rigid = null;

	private void Start()
	{
		m_rigid = GetComponent<Rigidbody>();
		Turn();
	}
	
	private void FixedUpdate()
	{
		Vector3 pos = transform.position;
		pos.z = pos.y;
		transform.position = pos;
	}

	private void Turn()
	{
		float radian = Random.value * Mathf.PI * 2;
		Vector2 vec2 = Vector2.zero;
		vec2.x = Mathf.Cos(radian);
		vec2.y = Mathf.Sin(radian);
		m_rigid.velocity = vec2 * m_speed;
		Invoke("Turn", Random.Range(1.0f, 3.0f));
	}
}
