using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public void DestroyThis()
	{
		GameObject.Destroy(gameObject);
	}

	public void GoToField()
	{
		Application.LoadLevel("Loading");
	}

	public void GoToVillage()
	{
		Application.LoadLevel("Village");
	}
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	#endregion
}
