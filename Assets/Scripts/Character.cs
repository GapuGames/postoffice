using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character : MonoBehaviour
{
	/// public field
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void Stop()
	{
		m_rigid.velocity = Vector2.zero;
	}

	public void Move(float radian)
	{
		Vector2 vec2 = Vector2.zero;
		vec2.x = Mathf.Cos(radian);
		vec2.y = Mathf.Sin(radian);
		m_rigid.velocity = vec2 * m_speed;
	}

	public uint ApplyDamage(uint amount)
	{
		m_nowHP = (amount >= m_nowHP)? 0:(m_nowHP - amount);
		return m_nowHP;
	}

	public uint ApplyHeal(uint amount)
	{
		m_nowHP = (m_maxHP <= m_nowHP)? m_maxHP:(m_nowHP + amount);
		return m_nowHP;
	}

	public uint HearthPoint
	{
		get { return m_nowHP; }
	}

	public float HearthRate
	{
		get { return (float)m_nowHP/(float)m_maxHP; }
	}

	/// private field
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	[SerializeField] private float m_speed  = 2.0f;
	[SerializeField] private uint  m_maxHP  = 0;
	[SerializeField] private Image m_hpUI   = null;
	private uint        m_nowHP  = 0;
	private Rigidbody2D m_rigid  = null;

	private void Awake()
	{
		m_rigid = GetComponent<Rigidbody2D>();
		m_nowHP = m_maxHP;
	}
	
	private void FixedUpdate()
	{
		Vector3 position = transform.position;
		Vector3 scale    = transform.localScale;
		position.z = position.y;
		if (m_rigid.velocity.x != 0.0f) scale.x = (m_rigid.velocity.x < 0)? -1 : +1;
		transform.position   = position;
		transform.localScale = scale;

		if (m_hpUI.fillAmount != HearthRate) m_hpUI.fillAmount = HearthRate;
	}
}
