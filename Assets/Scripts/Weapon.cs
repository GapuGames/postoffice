using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[SerializeField] private uint m_damage = 10;
	public void Use()
	{
		gameObject.SetActive(true);
		Invoke("Deactive", 0.1f);
	}

	public void Deactive()
	{
		CancelInvoke("Deactive");
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Enter");
		Deactive();
		other.transform.parent.SendMessage("OnDamaged", m_damage);
	}
}
