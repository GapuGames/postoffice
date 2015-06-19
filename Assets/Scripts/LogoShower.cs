using UnityEngine;
using System.Collections;

public class LogoShower : MonoBehaviour
{

	void Start()
	{
		Invoke("ChangeScene", 3);
	}

	void ChangeScene()
	{
		Application.LoadLevel("Street");
	}
}
