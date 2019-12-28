using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

//using StartApp;

public class AdManagement : MonoBehaviour {

	string gameId = "3412316";
	bool testMode = true;

	bool handled = false;

	void Start()
	{
		Debug.Log("script initiating");
		Advertisement.Initialize(gameId, testMode);
		Debug.Log("finished initiating");
	}

	public void DisplayVideo()
	{
		Debug.Log("button pressed");

		Advertisement.Show();

	}

	public void DisplayVideo(ShowOptions options)
	{
		Debug.Log("button pressed");

		Advertisement.Show(options);
		
	}

	/*private bool hasBeenClosed = false;

	public GameObject blackWall;

	InterstitialAd ad;

	void Start()
	{
		ad = AdSdk.Instance.CreateInterstitial();

		ad.LoadAd(InterstitialAd.AdType.Automatic);

		blackWall.GetComponent<SpriteRenderer>().color = Color.clear;	
	}

	public void ShowAdd()
	{

		ad.RaiseAdLoaded += (sender, e) => {
			ad.ShowAd();
		};

		ad.RaiseAdShown += (sender, e) => blackWall.GetComponent<SpriteRenderer>().color = Color.black;

		ad.RaiseAdClosed += (sender, e) => { hasBeenClosed = true; blackWall.GetComponent<SpriteRenderer>().color = Color.clear; };

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

	}*/
}
