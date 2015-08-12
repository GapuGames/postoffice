using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public bool       m_moving = false;
	public float      m_speed  = 2.0f;
	public float      m_degree = 0;
	public Controller m_ctrler = null;

	private Rigidbody m_rigid = null;
	private Animation m_anim  = null;

	private void Start()
	{
		m_rigid = GetComponent<Rigidbody>();
		m_anim  = GetComponent<Animation>();

		m_ctrler.m_beginAct = BeginMove;
		m_ctrler.m_endAct   = EndMove;
		m_ctrler.m_dragAct  = Move;
		m_ctrler.m_clickAct = Click;
	}

	public void BeginMove()
	{
	}
	
	public void EndMove()
	{
		m_rigid.velocity = Vector2.zero;
	}
	
	public void Move(float radian)
	{
		Vector2 vec2 = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
		m_rigid.velocity = vec2 * m_speed;
	}
	
	public void Click()
	{
		m_anim.Play("Test", PlayMode.StopAll);
	}

}
