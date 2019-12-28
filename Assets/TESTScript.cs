using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class TESTScript : MonoBehaviour
{
	string gameId = "3412316";
	bool testMode = false;

	void Start()
	{
		Debug.Log("script initiating");
		Advertisement.Initialize(gameId, testMode);
		Debug.Log("finished initiating");
	}

	public void DisplayTest()
	{
		Debug.Log("button pressed");
		Advertisement.Show();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.T))
			DisplayTest();
	}
}
