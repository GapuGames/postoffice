using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character : MonoBehaviour
{
	public int  HP   { get { return m_hp; } }
	public bool Flip { get { return (m_model != null)? m_model.flip : false; } }

	public void SetInfo(CharacterInfo info)
	{
		if ((m_info = info) == null) return;
		m_hp   = info.maxHP;
	}

	public void MoveBy(Vector2 direction, float power)
	{
		if (m_info == null) return;

		m_rigid.velocity = direction * power * m_info.moveSpeed;

		if (power > 0.0f) m_model.flip = direction.x < 0.0f;
		m_anim.SetBool(AnimParam.move, (power > 0.0f));
		m_anim.SetFloat(AnimParam.speed, power);
	}

	public void ApplyDamage(int amount)
	{
		if (amount <= 0) return;

		MoveBy(Vector2.zero, 0);

		if (m_hp > 0 && (m_hp - amount) <= 0)
		{
			m_anim.SetBool(AnimParam.die, true);
			if (m_brain != null) m_brain.Dead();
		}
		else m_anim.SetTrigger(AnimParam.damage);

		if ((m_hp -= amount) < 0) m_hp = 0;
		if (m_hpBar != null) m_hpBar.fillAmount = (float)m_hp/(float)m_info.maxHP;
	}

	public void StartAttack()
	{
		m_anim.SetBool(AnimParam.attack, true);
	}

	public void StopAttack()
	{
		m_anim.SetBool(AnimParam.attack, false);
	}
	
	//! private members, callbacks or anythings that you don't need to worry about
	#region
	[SerializeField]
	private CharacterInfo  m_info  = null;
	private CharacterBrain m_brain = null;
	private Rigidbody2D    m_rigid = null;
	private Animator       m_anim  = null;
	private ActiveSkill    m_skill = null;
	private Image          m_hpBar = null;
	private Puppet2D_GlobalControl m_model = null;

	private int m_hp = 2;

	public class AnimParam
	{
		public static readonly int move   = Animator.StringToHash("move");
		public static readonly int damage = Animator.StringToHash("damage");
		public static readonly int die    = Animator.StringToHash("die");
		public static readonly int speed  = Animator.StringToHash("speed");
		public static readonly int attack = Animator.StringToHash("attack");
	}

	private void CastSkill()
	{
		if (m_skill != null) m_skill.Activate(this);
	}

	private void Awake()
	{
		m_anim  = GetComponent<Animator>();
		m_rigid = gameObject.AddComponent<Rigidbody2D>();
		m_model = m_anim.GetComponentInChildren<Puppet2D_GlobalControl>();
		m_hpBar = transform.FindComponent<Image>("HP");

		if (tag != "Player") (m_brain = gameObject.AddComponent<DummyBrain>()).character = this;

		m_rigid.gravityScale   = 0.0f;
		m_rigid.freezeRotation = true;

		if (m_info.activeSkill != null)
		{
			m_skill = (GameObject.Instantiate(m_info.activeSkill.gameObject) as GameObject).GetComponent<ActiveSkill>();
		}
	}

	private void Start()
	{
		if (m_brain != null) m_brain.Born();
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
