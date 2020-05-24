using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CurrentLanguage : MonoBehaviour {

	public enum Lingua { Port, Ing, Tchec }

	public GameObject portBtn, ingBtn, tchecBtn;

	public Lingua lingua;

	private bool first = false; public bool GetFirst() { return first; }

	private int temp;

	public bool textsActive = false;

	public GameObject debuger;
	//public TextMesh txtMs;

	public void SetLingua(Lingua val)
	{
		lingua = val;
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			if (portBtn != null && ingBtn != null && tchecBtn != null)
			{

				if (val == Lingua.Ing)
				{
					ingBtn.GetComponent<Image>().color = Color.white;
					portBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
					tchecBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
				}
				else if (val == Lingua.Tchec)
				{
					tchecBtn.GetComponent<Image>().color = Color.white;
					ingBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
					portBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
				}
				else
				{
					portBtn.GetComponent<Image>().color = Color.white;
					ingBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
					tchecBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
				}
			}
		}
	}
	public Lingua GetLingua() { return lingua; }

	private void Awake()
	{
		if (SceneManager.GetActiveScene().name == "Menu")
			DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{Debug.Log(PlayerPrefs.GetInt("CurrentLang"));

		temp = PlayerPrefs.GetInt("DefaultText", 0);

		if (temp == 0)
			textsActive = false;
		else
			textsActive = true;

		if (!textsActive)
		{
			StartCoroutine(GetText());
		}
		else
		{
			if(debuger != null)
			{
				//debuger.GetComponent<Text>().text = "true";
			}
			//txtMs.text = "true";
		}

		if (PlayerPrefs.GetInt("CurrentLang") == 1)
		{
			SetLinguatoIng();
		}
		else if (PlayerPrefs.GetInt("CurrentLang") == 2)
		{
			SetLinguatoTchec();
		}
		else
		{
			SetLinguatoPort();
		}
		
		GameObject[] others = GameObject.FindGameObjectsWithTag("LM");

		

		if (others.Length == 1){
			first = true;
		}
		else
		{
			if (!first)
			{
				Destroy(gameObject);
			}
		}
    }

	void Update()
	{
		portBtn = GameObject.Find("Br");
		ingBtn = GameObject.Find("Us");
		tchecBtn = GameObject.Find("Cz");
	}

	public void SetLinguatoPort() { SetLingua(Lingua.Port); PlayerPrefs.SetInt("CurrentLang", 0); }
    public void SetLinguatoIng() { SetLingua(Lingua.Ing); PlayerPrefs.SetInt("CurrentLang", 1); }
    public void SetLinguatoTchec() { SetLingua(Lingua.Tchec); PlayerPrefs.SetInt("CurrentLang", 2); }

	IEnumerator GetText()
	{
		UnityWebRequest www = UnityWebRequest.Get("https://gist.githubusercontent.com/fergorgs/9b884ff91d59ece2703fa6a55e08226e/raw/textInfo.txt");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			if (debuger != null)
			{
				//debuger.GetComponent<Text>().text = "returned " + www.downloadHandler.text;
			}
			//txtMs.text = "returned " + www.downloadHandler.text;
			if (www.downloadHandler.text == "true")
			{
				textsActive = true;
				PlayerPrefs.SetInt("DefaultText", 1);
			}
		}

		//yield return null;
	}
}
