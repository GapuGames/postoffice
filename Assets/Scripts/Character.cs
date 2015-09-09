using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public Rigidbody2D m_body  = null;
	public Animator    m_anim = null;
	public Puppet2D_GlobalControl m_model = null;

	public void MoveBy(Vector2 direction, float speed)
	{
		m_body.velocity = direction * speed;

		if (speed > 0.0f) m_model.flip = direction.x < 0.0f;
		m_anim.SetBool("moving", (speed > 0.0f));
	}



}
