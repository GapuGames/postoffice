using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public int  HP   { get { return m_hp; } }
	public bool Flip { get { return (m_model != null)? m_model.flip : false; } }

	public void SetInfo(CharacterInfo info)
	{
		m_info = info;
	}

	public void MoveBy(Vector2 direction, float power)
	{
		if (m_info == null) return;

		m_rigid.velocity = direction * power * m_info.moveSpeed;

		if (power > 0.0f) m_model.flip = direction.x < 0.0f;
		m_anim.SetBool("moving", (power > 0.0f));
		m_anim.SetFloat("speed", power);
	}

	public void ApplyDamage(int amount)
	{
		if (amount <= 0) return;
		m_anim.SetTrigger("damaged");
		if ((m_hp -= amount) < 0) m_hp = 0;
	}

	public void StartAttack()
	{
		m_anim.SetBool("attacking", true);

		AnimatorStateInfo state = m_anim.GetCurrentAnimatorStateInfo(0);;
		Debug.Log(state.length);
		InvokeRepeating("SetAttackType", 0, state.length);
	}

	public void StopAttack()
	{
		m_anim.SetBool("attacking", false);
	}
	
	//! private members, callbacks or anythings that you don't need to worry about
	#region
	[SerializeField]
	private CharacterInfo  m_info  = null;
	private CharacterBrain m_brain = null;
	private Rigidbody2D    m_rigid = null;
	private Animator       m_anim  = null;
	private ActiveSkill    m_skill = null;
	private Puppet2D_GlobalControl m_model = null;

	private int m_hp = 0;

	private void SetAttackType()
	{
		m_anim.SetInteger("attackType", Random.Range (1, 3));
	}

	private void CastSkill()
	{
		if (m_skill != null) m_skill.Activate(this);
	}

	private void Awake()
	{
		m_anim   = GetComponent<Animator>();
		m_rigid  = gameObject.AddComponent<Rigidbody2D>();
		m_brain  = gameObject.AddComponent<DummyBrain>();
		m_model  = m_anim.GetComponentInChildren<Puppet2D_GlobalControl>();

		m_rigid.gravityScale   = 0.0f;
		m_rigid.freezeRotation = true;

		if (m_info.activeSkill != null)
		{
			m_skill = (GameObject.Instantiate(m_info.activeSkill.gameObject) as GameObject).GetComponent<ActiveSkill>();
		}
	}

	private void FixedUpdate()
	{
		if (m_anim != null)
		{
			Vector3 pos = m_anim.transform.localPosition;
			pos.z = pos.y;
			m_anim.transform.localPosition = pos;
		}
	}
	#endregion

}
