using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
//using UnityEngine.Monetization;

public class ShowAddScript : MonoBehaviour {
	
	/*public string lvlName;

	private SceneSwitch scSw;
	public void SetSceneSwitch(SceneSwitch val) { scSw = val; }

	public string adId = "ca-app-pub-1291216773466087/2489021997";

	public string appId = "ca-app-pub-1291216773466087~4072075711";

	private InterstitialAd regAd;

	//public GameObject debug;

	private int tries = 0;

	void Start()
	{
		MobileAds.Initialize(appId);

		regAd = new InterstitialAd(adId);

		regAd.OnAdFailedToLoad += HandleOnFailToLoad;
		regAd.OnAdClosed += HandleOnAdClosed;

		//debug.GetComponent<Text>().text = "Ok so far";

		LoadAd();
	}

    public void ShowRewardedAd() {

		ShowAd();
		//debug.GetComponent<Text>().text = "Showed";

	}

	private void LoadAd()
	{
		regAd.LoadAd(new AdRequest.Builder().Build());
	}

	private void ShowAd()
	{
		if (regAd.IsLoaded()) {
			//debug.GetComponent<Text>().text = "Loaded";
			regAd.Show();
		}
		else
		{
			//debug.GetComponent<Text>().text = "Not loaded";
		}
	}

	/*public IEnumerator TriggerShowAd()
	{
		yield return new WaitForSeconds(5);

		ShowAd();

	}

	public IEnumerator WaitTryAgain()
	{
		//tries++;
		//debug.GetComponent<Text>().text = "Trying again";
		yield return new WaitForSeconds(0.25f);

		//if (tries == 12)
			//StartCoroutine(scSw.LoadingScreen(lvlName));
		//else

			LoadAd();



	}

	public void HandleOnFailToLoad(System.Object sender, AdFailedToLoadEventArgs args)
	{
		StartCoroutine(WaitTryAgain());
		//debug.GetComponent<Text>().text = "Failed";

	}

	public void HandleOnAdClosed(System.Object sender, EventArgs args)
	{
		StartCoroutine(scSw.LoadingScreen(lvlName));
	}*/


}
