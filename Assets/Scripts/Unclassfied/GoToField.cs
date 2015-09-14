using UnityEngine;
using System.Collections;

public class GoToField : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		Application.LoadLevel("Loading");
	}
}
