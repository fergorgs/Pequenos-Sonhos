using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class TESTScript : MonoBehaviour
{

	void Start()
	{
		StartCoroutine(GetText());
	}

	IEnumerator GetText()
	{
		UnityWebRequest www = UnityWebRequest.Get("https://gist.github.com/fergorgs/9b884ff91d59ece2703fa6a55e08226e/raw/171a3d8c5f3d1e51a92d692b994057a2b8de311e/textInfo.txt");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log("error: " + www.error);
		}
		else
		{
			Debug.Log("message: " + www.downloadHandler.text);
			//textsActive = true;
		}
	}
}
