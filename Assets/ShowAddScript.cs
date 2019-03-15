using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class ShowAddScript : MonoBehaviour {

	public string lvlName;

	private SceneSwitch scSw;
	public void SetSceneSwitch(SceneSwitch val) { scSw = val; }

    private void Start() {
        Monetization.Initialize("3052658", true);
    }

    public void ShowRewardedAd() {
		//Debug.Log("SRA");
        if (Monetization.IsReady("banner")){
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent("banner") as ShowAdPlacementContent;
			if (ad != null) {
                ad.Show(AdFinished);
            }
        }
    }

	void AdFinished(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			//Debug.Log("Chama");

			StartCoroutine(scSw.LoadingScreen(lvlName));
		}
	}
}
