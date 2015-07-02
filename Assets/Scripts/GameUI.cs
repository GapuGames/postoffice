using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class GameUI : MonoBehaviour {
	

	public static void Notice(string title, string description, UnityAction okAction = null)
	{
		GameObject inst = GameObject.Instantiate(LoadPrefab(m_noticeUIPath)) as GameObject;
		if (Instance.m_popupOrder > m_popupMaxOrder) Debug.LogWarning("there are too many popup UIs");
		inst.GetComponent<Canvas>().sortingOrder = Instance.m_popupOrder++;

		Text   titleText = inst.transform.Find("Title").GetComponent<Text>();
		Text   descText  = inst.transform.Find("Desc").GetComponent<Text>();
		Button okBtn     = inst.transform.Find("OkBtn").GetComponent<Button>();
		
		//! make buttons be disanble to prevent overried touch and destroy this ui
		UnityAction closing = () =>
		{
			okBtn.enabled = false;
			if ((--Instance.m_popupOrder) < m_popupMinOrder) Debug.LogWarning("sorting order for popup ui is underflowed!");
			GameObject.Destroy(inst);
		};

		titleText.text = title;
		descText.text  = description;
		
		if (okAction != null) okBtn.onClick.AddListener(okAction);
		okBtn.onClick.AddListener(closing);
	}
	
	public static void Choice(string title, string description, UnityAction yesAction = null, UnityAction noAction = null)
	{
		GameObject inst = GameObject.Instantiate(LoadPrefab(m_choiceUIPath)) as GameObject;
		if (Instance.m_popupOrder > m_popupMaxOrder) Debug.LogWarning("there are too many popup UIs");
		inst.GetComponent<Canvas>().sortingOrder = Instance.m_popupOrder++;
		
		Text   titleText = inst.transform.Find("Title").GetComponent<Text>();
		Text   descText  = inst.transform.Find("Desc").GetComponent<Text>();
		Button yesBtn    = inst.transform.Find("YesBtn").GetComponent<Button>();
		Button noBtn     = inst.transform.Find("NoBtn").GetComponent<Button>();
		
		//! make buttons be disanble to prevent overried touch and destroy this ui
		UnityAction closing = () =>
		{
			yesBtn.enabled = false;
			noBtn.enabled  = false;
			if ((--Instance.m_popupOrder) < m_popupMinOrder) Debug.LogWarning("sorting order for popup ui is underflowed!");
			GameObject.Destroy(inst);
		};

		titleText.text = title;
		descText.text  = description;

		if (yesAction != null) yesBtn.onClick.AddListener(yesAction);
		if (noAction != null)  noBtn.onClick.AddListener(noAction);
		yesBtn.onClick.AddListener(closing);
		noBtn.onClick.AddListener(closing);
	}

	private static GameObject LoadPrefab(string path)
	{
		return Resources.Load(path) as GameObject;
	}

	private void Awake()
	{
		if (m_instance != null)
		{
			Debug.LogError("GameUI has been tried to be created more than once");
			return;
		}
	}

	private static GameUI Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = (new GameObject("GameUI")).AddComponent<GameUI>();
				GameObject.DontDestroyOnLoad(m_instance.gameObject);
			}
			return m_instance;
		}
	}

	private static GameUI m_instance = null;
	private static string m_noticeUIPath = "Prefab/UI/Notice";
	private static string m_choiceUIPath = "Prefab/UI/Choice";
	private static int m_popupMaxOrder = 1015;
	private static int m_popupMinOrder = 1000;
	private int m_popupOrder = m_popupMinOrder; //! popup(notice, choice) sort Order 1000~1015, there can be only 15-popups
}
