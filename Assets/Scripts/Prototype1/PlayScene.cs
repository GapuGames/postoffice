using UnityEngine;
using System.Collections;

public class PlayScene : MonoBehaviour
{

	//! public members
	public static PlayScene Instance
	{
		get
		{
			if (m_instance == null) (new GameObject("PlayScene")).AddComponent<PlayScene>();
			return m_instance;
		}
	}

	public static Camera MainCamera { get { return Instance.m_mainCamera; } }
	public static Ground MainGround { get { return Instance.m_mainGround; } }
	
	public Camera m_mainCamera = null;
	public Ground m_mainGround = null;

	//! private members
	private static PlayScene m_instance = null;

	//! public methods
	//! private methods
	private void Awake()
	{
		m_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
