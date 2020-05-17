using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToSiteButton : MonoBehaviour {

	private string siteURL = "https://www.andrefrancovive.org.br/doar/";

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
