using UnityEngine;
using System.Collections;

public class GoToField : MonoBehaviour
{
	public void ChangeScene()
	{
		Debug.Log("ChangeScene");
		Application.LoadLevel("Loading");
	}
}
