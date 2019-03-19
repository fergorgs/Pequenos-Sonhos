using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StartApp;

public class AdManagement : MonoBehaviour {

	private bool hasBeenClosed = false;

	//public GameObject blackWall;

	InterstitialAd ad;

	void Start()
	{
		ad = AdSdk.Instance.CreateInterstitial();

		ad.LoadAd(InterstitialAd.AdType.Automatic);

		//blackWall.GetComponent<SpriteRenderer>().color = Color.clear;	
	}

	public void ShowAdd()
	{

		ad.RaiseAdLoaded += (sender, e) => {
			ad.ShowAd();
		};

		//ad.RaiseAdShown += (sender, e) => blackWall.GetComponent<SpriteRenderer>().color = Color.black;

		//ad.RaiseAdClosed += (sender, e) => { hasBeenClosed = true; blackWall.GetComponent<SpriteRenderer>().color = Color.clear; };

		ad.LoadAd(InterstitialAd.AdType.Automatic);
	}

	public bool HasBeenClosed()
	{

		if (hasBeenClosed)
		{
			hasBeenClosed = false;
			return true;
		}
		else
			return false;

	}
}
