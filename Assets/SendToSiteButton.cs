using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToSiteButton : MonoBehaviour {

	public string siteURL = "https://www.kickante.com.br/campanhas/instituto-andre-franco-vive-arrecadacao";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenSite()
	{
		Application.OpenURL(siteURL);
	}
}
