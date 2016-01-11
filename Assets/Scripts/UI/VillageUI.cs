using UnityEngine;
using System.Collections;

public class VillageUI : MonoBehaviour
{
	//! public, protected or everything can be used outside of this
	public Popup cashShopPopup;
	public Popup goldShopPopup;
	public Popup postShopPopup;
	public Popup itemShopPopup;
	public Popup optionPopup;
	public Popup mailPopup;
	public Popup questPopup;
	public Popup friendPopup;
	public Popup goToFieldPopup;
	
	//! private, callback or anything don’t be considered to be used outside of this
	#region
	private void Start ()
	{
	
	}
	
	// Update is called once per frame
	private void Update ()
	{
	
	}

	public void OpenCashShopPopup()
	{
		cashShopPopup.Open();
	}
	
	public void OpenGoldShopPopup()
	{
		goldShopPopup.Open();
	}
	
	public void OpenPostShopPopup()
	{
		postShopPopup.Open();
	}
	
	public void OpenItemShopPopup()
	{
		itemShopPopup.Open();
	}
	
	public void OpenOptionPopup()
	{
		optionPopup.Open();
	}
	
	public void OpenQuestPopup()
	{
		questPopup.Open();
	}
	
	public void OpenMailPopup()
	{
		mailPopup.Open();
	}
	
	public void OpenFriendPopup()
	{
		cashShopPopup.Open();
	}

	public void OpenGoToFieldPopup()
	{
		goToFieldPopup.Open();
	}

	#endregion
}
