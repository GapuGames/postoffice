using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Building : MonoBehaviour {

	//! public members & methods
	public float m_goldOffset = 0;
	public GameObject m_buildingModel = null;

	public void SetListener(UnityAction<int> listener)
	{
		m_listener = listener;
	}

	public void SetGoldAmount(int goldAmount)
	{
		m_goldString = (m_goldAmount = goldAmount).ToString();
	}

	//! private members & methods
	private GameObject m_earnedGoldPrefab = null;
	private int        m_goldAmount = 0;
	private string     m_goldString = string.Empty;

	private UnityAction<int> m_listener   = null;

	private void Awake()
	{
		m_earnedGoldPrefab = LoadPrefab("Prefab/World/EarnedGold");

		if (m_buildingModel != null)
		{
			m_buildingModel    = GameObject.Instantiate(m_buildingModel) as GameObject;
			m_buildingModel.transform.SetParent(transform, false);
		}
	}

	private void Start ()
	{
		SetGoldAmount(100);
		InvokeRepeating("EarnMoney", 0, 1);
	}
	
	private static GameObject LoadPrefab(string path)
	{
		return Resources.Load(path) as GameObject;
	}

	private void EarnMoney()
	{
		if (m_goldAmount <= 0) return;

		if (m_listener != null) m_listener(m_goldAmount);

		GameObject instance = GameObject.Instantiate(m_earnedGoldPrefab) as GameObject;
		instance.transform.SetParent(transform, false);
		instance.transform.localPosition += Vector3.up * m_goldOffset;

		Text text = instance.GetComponentInChildren<Text>();
		text.text = m_goldString;

		Animation animation = instance.GetComponent<Animation>();
		GameObject.Destroy(instance, animation.clip.length);
	}
}
