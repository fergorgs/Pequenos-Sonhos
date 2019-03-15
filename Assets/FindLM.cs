using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindLM : MonoBehaviour {

	public enum LingType { Port, Ing, Tchec }

	public LingType lingua = LingType.Port;

	private GameObject[] LMs;
	private GameObject LM;
	private LingType bootLing;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt("CurrentLang") == 1)
			bootLing = LingType.Ing;
		else if (PlayerPrefs.GetInt("CurrentLang") == 2)
			bootLing = LingType.Tchec;
		else
			bootLing = LingType.Port;

		LMs = GameObject.FindGameObjectsWithTag("LM");

		if (LMs.Length == 1)
			LM = LMs[0];
		else if (LMs[0].GetComponent<CurrentLanguage>().GetFirst())
			LM = LMs[0];
		else
			LM = LMs[1];

		if(bootLing == lingua)
			GetComponent<Image>().color = Color.white;
		else
			GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ChangeLang()
	{
		switch (lingua){

			case LingType.Port:
				LM.GetComponent<CurrentLanguage>().SetLinguatoPort();
				break;
			case LingType.Ing:
				LM.GetComponent<CurrentLanguage>().SetLinguatoIng();
				break;
			case LingType.Tchec:
				LM.GetComponent<CurrentLanguage>().SetLinguatoTchec();
				break;
		}
	}
}
