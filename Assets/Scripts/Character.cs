using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public enum State
	{
		Idle    = 0,
		Move    = 1,
		Attacke = 2,
		Damage  = 3,
		Die     = 4,
	}

	public void LoadCharacter(CharacterInfo info)
	{
		if (info == null) return;
		
		GameObject modelInstance = GameObject.Instantiate(info.modelPrefab);
		m_body  = modelInstance.AddComponent<Rigidbody2D>();
		m_anim  = modelInstance.GetComponent<Animator>();
		m_model = modelInstance.GetComponentInChildren<Puppet2D_GlobalControl>();
		m_info  = info;
		
		m_body.gravityScale   = 0.0f;
		m_body.freezeRotation = true;
		modelInstance.transform.SetParent(transform, false);

		Debug.Log(info.facePath);
	}

	public void MoveBy(Vector2 direction, float power)
	{
		if (m_info == null) return;

		m_body.velocity = direction * power * m_info.moveSpeed;

		if (power > 0.0f) m_model.flip = direction.x < 0.0f;
		m_anim.SetBool("moving", (power > 0.0f));
	}

	//! private members, callbacks or anythings that you don't need to worry about
	#region
	
	[SerializeField]
	private CharacterInfo m_info = null;
	private Rigidbody2D   m_body = null;
	private Animator      m_anim = null;
	private Puppet2D_GlobalControl m_model = null;

	private void Start()
	{
		LoadCharacter(m_info);
		SetSpriteLayer("MiddleGround");
	}

	public void SetSpriteLayer(string layerName)
	{
		SpriteRenderer[] rendererList = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer renderer in rendererList)
		{
			renderer.sortingLayerName = layerName;
		}
	}
	#endregion

}
