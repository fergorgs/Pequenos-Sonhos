using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using StartApp;

public class SceneSwitch : MonoBehaviour
{

    public bool hasToClick = true;

    public string sceneName;
    private string path;
	private int curLevel = 0;
	private int highestLevel = 0;
    public GameObject loadingScreenObj, adsObj, yesButton, noButton, startButton;
	public GameObject title, btnsDefault, btnsLang, bird;
    public Slider slider;
	public GameObject AdsBlackWall;

    AsyncOperation async;

	InterstitialAd ad;



	// Use this for initialization
	void Start()
    {
		ad = AdSdk.Instance.CreateInterstitial();

		ad.LoadAd(InterstitialAd.AdType.Automatic);

		path = "Assets/Scenes/" + sceneName + ".unity";

		//ads.SetSceneSwitch(this);

		switch (sceneName)
		{
			case ("Nivel 0"):
				curLevel = 0;
				break;
			case ("Nivel 1"):
				curLevel = 1;
				break;
			case ("Nivel 2"):
				curLevel = 2;
				break;
			case ("Nivel 3"):
				curLevel = 3;
				break;
			case ("Nivel 4"):
				curLevel = 4;
				break;
			case ("Nivel 5"):
				curLevel = 5;
				break;
		}

		switch (PlayerPrefs.GetString("CurrentLevel"))
		{
			case ("Assets/Scenes/Nivel 0.unity"):
				highestLevel = 0;
				break;
			case ("Assets/Scenes/Nivel 1.unity"):
				highestLevel = 1;
				break;
			case ("Assets/Scenes/Nivel 2.unity"):
				highestLevel= 2;
				break;
			case ("Assets/Scenes/Nivel 3.unity"):
				highestLevel = 3;
				break;
			case ("Assets/Scenes/Nivel 4.unity"):
				highestLevel = 4;
				break;
			case ("Assets/Scenes/Nivel 5.unity"):
				highestLevel = 5;
				break;
		}
	}

    // Update is called once per frame
    void Update()
    {

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (hasToClick == false)
        {
			if (col.gameObject.tag == "Player")
			{
				if (sceneName != "Menu" && curLevel > highestLevel) {
					PlayerPrefs.SetString("CurrentLevel", path);
			}
                PlayerPrefs.SetInt(sceneName + "Completed", 1);
                PlayerPrefs.SetInt("isAfterCP", 0);
                LoadScreen(path);
            }
                
        }
    }

    void OnMouseUp()
    {
        /*if (sceneName != "Menu")
            PlayerPrefs.SetString("CurrentLevel", path);
        PlayerPrefs.SetInt("isAfterCP", 0);*/
        LoadScreen(path);
    }

    public void SwitchScene()
    {
        //if (ads!=null && PlayerPrefs.GetInt("AdsOn") == 1) {
        //    ads.ShowRewardedAd();
        //}
        if (sceneName != "Menu" && curLevel > highestLevel)
		{
			Debug.Log("Aqui");

			PlayerPrefs.SetString("CurrentLevel", path);
		}
        PlayerPrefs.SetInt("isAfterCP", 0);
        LoadScreen(path);
        Time.timeScale = 1;  
    }
    
    public void Continue() {
        if(!PlayerPrefs.GetString("CurrentLevel").Equals("Assets / Scenes /Menu.unity"))
            LoadScreen(PlayerPrefs.GetString("CurrentLevel"));
    }

    public void TurnOnOffAds(int activate) {
        PlayerPrefs.SetInt("AdsOn", activate);
		/*if (activate == 1)
			ads.ShowRewardedAd();*/
		LoadScreen("Nivel 0");
        yesButton.SetActive(false);
        noButton.SetActive(false);
        //startButton.SetActive(true);*/
        
    }
    public void ShowAdsObj(bool turnOn) {
		title.SetActive(false);
		btnsDefault.SetActive(false);
		btnsLang.SetActive(false);
		bird.SetActive(false);
		PlayerPrefs.SetString("CurrentLevel", "Assets/Scenes/Nivel 0.unity");
		adsObj.SetActive(turnOn);
    }

    public void LoadScreen(string lvlName) {

		if (PlayerPrefs.GetInt("AdsOn") == 1)
		{
			ShowAdd(lvlName);
		}
		else
			StartCoroutine(LoadingScreen(lvlName));
    }

    public IEnumerator LoadingScreen(string lvlName) {
		Debug.Log("Chamado");
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(lvlName, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (!async.isDone) {
            float progress = Mathf.Clamp01(async.progress / .9f);
            //Debug.Log(progress);
            slider.value = async.progress;
            if (async.progress == 0.9f) {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

	public void ShowAdd(string lvlName)
	{
		ad.RaiseAdLoaded += (sender, e) => {
			ad.ShowAd();
		};

		ad.RaiseAdShown += (sender, e) => AdsBlackWall.GetComponent<SpriteRenderer>().color = Color.black;

		ad.RaiseAdLoadingFailed += (sender, e) => StartCoroutine(LoadingScreen(lvlName));

		ad.RaiseAdClosed += (sender, e) => StartCoroutine(LoadingScreen(lvlName));

		ad.RaiseAdClicked += (sender, e) => StartCoroutine(LoadingScreen(lvlName));

		ad.LoadAd(InterstitialAd.AdType.Automatic);

	}
}
