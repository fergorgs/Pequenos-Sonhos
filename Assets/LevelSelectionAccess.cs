using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionAccess : MonoBehaviour {

	public int nivel;

	private string curLevelPath;
	private int curLevel = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		curLevelPath = PlayerPrefs.GetString("CurrentLevel");

		switch (curLevelPath)
		{
			case ("Assets/Scenes/Nivel 0.unity"):
				curLevel = 0;
				break;
			case ("Assets/Scenes/Nivel 1.unity"):
				curLevel = 1;
				break;
			case ("Assets/Scenes/Nivel 2.unity"):
				curLevel = 2;
				break;
			case ("Assets/Scenes/Nivel 3.unity"):
				curLevel = 3;
				break;
			case ("Assets/Scenes/Nivel 4.unity"):
				curLevel = 4;
				break;
			case ("Assets/Scenes/Nivel 5.unity"):
				curLevel = 5;
				break;
		}

		//Debug.Log("curLevel = " + curLevel);
		switch (nivel)
		{
			case (0):

				GetComponent<Button>().interactable = true;
				GetComponent<Image>().color = Color.white;
				break;

			case (1):

				if (curLevel >= 1)
				{
					GetComponent<Button>().interactable = true;
					GetComponent<Image>().color = Color.white;
				}
				else
				{
					GetComponent<Button>().interactable = false;
					GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
				}
				break;

			case (2):


				if (curLevel >= 2)
				{
					GetComponent<Button>().interactable = true;
					GetComponent<Image>().color = Color.white;
				}
				else
				{
					GetComponent<Button>().interactable = false;
					GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
				}
				break;

			case (3):


				if (curLevel >= 3)
				{
					//Debug.Log("Cima");
					GetComponent<Button>().interactable = true;
					GetComponent<Image>().color = Color.white;
				}
				else
				{
					//Debug.Log("bAIXO");
					GetComponent<Button>().interactable = false;
					GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
				}
				break;

			case (4):


				if (curLevel >= 4)
				{
					GetComponent<Button>().interactable = true;
					GetComponent<Image>().color = Color.white;
				}
				else
				{
					GetComponent<Button>().interactable = false;
					GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
				}
				break;

			case (5):


				if (curLevel >= 5)
				{
					GetComponent<Button>().interactable = true;
					GetComponent<Image>().color = Color.white;
				}
				else
				{
					GetComponent<Button>().interactable = false;
					GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1);
				}
				break;


		}
	}
}
