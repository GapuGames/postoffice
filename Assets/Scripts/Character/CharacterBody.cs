using UnityEngine;
using System.Collections;

public class CharacterBody : MonoBehaviour
{
	public Transform m_face   = null;
	public Transform m_eyes   = null;
	public Transform m_hair   = null;
	public Transform m_cloth  = null;
	public Transform m_weapon = null;

	public void LoadBody(CharacterInfo info)
	{
		Clear();

		if (info == null) return;

		m_info   = info;
		m_anim   = GameObject.Instantiate(info.modelPrefab).GetComponent<Animator>();
		m_rigid  = m_anim.gameObject.AddComponent<Rigidbody2D>();
		m_brain  = m_anim.gameObject.AddComponent<DummyBrain>();
		m_model  = m_anim.GetComponentInChildren<Puppet2D_GlobalControl>();
		
		m_face   = m_anim.transform.Find(info.facePath);
		m_eyes   = m_anim.transform.Find(info.eyesPath);
		m_hair   = m_anim.transform.Find(info.hairPath);
		m_cloth  = m_anim.transform.Find(info.clothPath);
		m_weapon = m_anim.transform.Find(info.weaponPath);

		m_rigid.gravityScale   = 0.0f;
		m_rigid.freezeRotation = true;
		m_rigid.transform.SetParent(transform, false);
	}

	public void MoveBy(Vector2 direction, float power)
	{
		if (m_info == null) return;

		m_rigid.velocity = direction * power * m_info.moveSpeed;

		if (power > 0.0f) m_model.flip = direction.x < 0.0f;
		m_anim.SetBool("moving", (power > 0.0f));
		m_anim.SetFloat("speed", power);
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

	public void Clear()
	{
		if (m_anim != null) GameObject.Destroy(m_anim.gameObject);
		m_info  = null;
		m_brain = null;
		m_anim  = null;
		m_rigid = null;
		m_model = null;
	}
	
	//! private members, callbacks or anythings that you don't need to worry about
	#region
	private CharacterInfo  m_info  = null;
	private CharacterBrain m_brain = null;
	private Rigidbody2D    m_rigid = null;
	private Animator       m_anim  = null;
	private Puppet2D_GlobalControl m_model = null;

	private void SetAttackType()
	{
		m_anim.SetInteger("attackType", Random.Range (1, 3));
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
