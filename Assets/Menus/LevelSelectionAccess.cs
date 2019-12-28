using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionAccess : MonoBehaviour {

	public int nivel;

	private string curLevelPath;
	private int curLevel = 0;

	private int highestLevel = 0;

	// Use this for initialization
	void Start () {

		highestLevel = PlayerPrefs.GetInt("HighestLevel", 0);
	
	}
	
	// Update is called once per frame
	void Update () {

		curLevelPath = PlayerPrefs.GetString("CurrentLevel");
	
		switch (curLevelPath)
		{
			/*case ("Assets/Scenes/Nivel 0.unity"):
				if()
				break;*/
			case ("Assets/Scenes/Nivel 1.unity"):
				if (1 > highestLevel)
				{
					highestLevel = 1;
					PlayerPrefs.SetInt("HighestLevel", 1);
				}
				break;
			case ("Assets/Scenes/Nivel 2.unity"):
				if (2 > highestLevel)
				{
					highestLevel = 2;
					PlayerPrefs.SetInt("HighestLevel", 2);
				}
				break;
			case ("Assets/Scenes/Nivel 3.unity"):
				if (3 > highestLevel)
				{
					highestLevel = 3;
					PlayerPrefs.SetInt("HighestLevel", 3);
				}
				break;
			case ("Assets/Scenes/Nivel 4.unity"):
				if (4 > highestLevel)
				{
					highestLevel = 4;
					PlayerPrefs.SetInt("HighestLevel", 4);
				}
				break;
			case ("Assets/Scenes/Nivel 5.unity"):
				if (5 > highestLevel)
				{
					highestLevel = 5;
					PlayerPrefs.SetInt("HighestLevel", 5);
				}
				break;
		}
		
		switch (nivel)
		{
			case (0):

				GetComponent<Button>().interactable = true;
				GetComponent<Image>().color = Color.white;
				break;

			case (1):

				if (highestLevel >= 1)
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


				if (highestLevel >= 2)
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


				if (highestLevel >= 3)
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


				if (highestLevel >= 4)
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


				if (highestLevel >= 5)
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
