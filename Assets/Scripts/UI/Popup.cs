using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour
{
	//! public, protected or everything can be used outside of this

	public void Open()
	{
		GameObject inst = GameObject.Instantiate(gameObject) as GameObject;
		inst.SetActive(true);
	}

	public void OpenPopup(Popup popup)
	{
		popup.Open();
	}

	public void Close()
	{
		GameObject.Destroy(gameObject);
	}

	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
