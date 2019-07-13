using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_P1 : MonoBehaviour
{
	public GameObject ContinueBtn, Text1, Text2, Text3, Text4, Text5, Seta, Mao1, Mao2, SwicthButton, Caixa, BW1, BW2, BW3, BW4, BW5;//, TEST1;

	//public GameObject[] UiElements;

	public GameObject wrdControl;
	private WorldSwitchScript wrdScript;

	private bool clickedOn = false, stage4 = false, firstTouch = false;
	private int touches = 0;
	public bool sceneDone = false;

	private float startTime = 0;

	private Color btnTextColor;

	// Start is called before the first frame update
	void Start()
    {
		wrdControl = GameObject.FindGameObjectWithTag("WCS");
		wrdScript = wrdControl.GetComponent<WorldSwitchScript>();

		btnTextColor = ContinueBtn.GetComponentInChildren<Text>().color;

	}

    // Update is called once per frame
    void Update()
    {

    }

	private IEnumerator Cutscene()
	{
		yield return new WaitForSeconds(1);

		Time.timeScale = 0;

		//PARTE 1-----------------------------------------------------------------------------------------------
		Text1.SetActive(true);
		Mao1.SetActive(true);
		BW1.SetActive(true);
		SwicthButton.SetActive(true);

		while (!wrdScript.worldHasShifted())
			yield return null;

		//PARTE 2-----------------------------------------------------------------------------------------------
		Text1.SetActive(false);
		Mao1.SetActive(false);
		BW1.SetActive(false);

		Text2.SetActive(true);
		ContinueBtn.SetActive(true);
		BW2.SetActive(true);
		SwicthButton.GetComponent<Image>().raycastTarget = false;

		while (!ContinueBtn.GetComponent<ContinuarClicked>().clicked)
			yield return null;
		for(int i = 0; i < 10; i++)
			yield return null;

		Text2.SetActive(false);
		BW2.SetActive(false);

		Text3.SetActive(true);
		Seta.SetActive(true);
		BW3.SetActive(true);

		while (!ContinueBtn.GetComponent<ContinuarClicked>().clicked)
			yield return null;

		Time.timeScale = 1;

		Text2.SetActive(false);
		Text3.SetActive(false);
		Seta.SetActive(false);
		BW3.SetActive(false);

		ContinueBtn.GetComponent<Image>().color = Color.clear;
		ContinueBtn.GetComponentInChildren<Text>().color = Color.clear;
		ContinueBtn.GetComponent<Image>().raycastTarget = false;
		SwicthButton.GetComponent<Image>().raycastTarget = true;

		while (!wrdScript.worldHasShifted())
			yield return null;

		//PARTE 3------------------------------------------------------------------------------------------------
		startTime = Time.time;
		while(Time.time-startTime < 0.7f)
			yield return null;

		Text4.SetActive(true);
		BW4.SetActive(true);
		ContinueBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.92f);
		ContinueBtn.GetComponentInChildren<Text>().color = btnTextColor;
		ContinueBtn.GetComponent<Image>().raycastTarget = true;
		SwicthButton.GetComponent<Image>().raycastTarget = false;

		Time.timeScale = 0;

		while (!ContinueBtn.GetComponent<ContinuarClicked>().clicked)
			yield return null;
		
		//Text4.SetActive(false);
		Text4.GetComponent<Text>().color = Color.clear;
		BW4.SetActive(false);
		//ContinueBtn.SetActive(false);
		ContinueBtn.GetComponent<Image>().color = Color.clear;
		ContinueBtn.GetComponentInChildren<Text>().color = Color.clear;
		ContinueBtn.GetComponent<Image>().raycastTarget = false;
		SwicthButton.GetComponent<Image>().raycastTarget = true;
		Mao1.SetActive(true);

		while (!wrdScript.worldHasShifted())
			yield return null;

		//PARTE 4-------------------------------------------------------------------------------------------------
		stage4 = true;

		Mao1.SetActive(false);

		Text5.SetActive(true);
		Mao2.SetActive(true);
		Caixa.GetComponent<ShiftBehavior>().isShiftable = true;
		BW5.SetActive(true);

		//TEST1.GetComponent<Image>().color = new Color(109, 109, 109, 1);

		//for (int i = 0; i < UiElements.Length; i++)
		//	UiElements[i].GetComponent<Image>().color = new Color(109, 109, 109, 1);

		while (!Caixa.GetComponent<ShiftBehavior>().GetIsReal())
		//while (!clickedOn)
			yield return null;

		Time.timeScale = 1;

		Text5.SetActive(false);
		Mao2.SetActive(false);
		BW5.SetActive(false);

		//for (int i = 0; i < UiElements.Length; i++)
		//	UiElements[i].GetComponent<Image>().color = new Color(255, 255, 255, 1);

		while (!wrdScript.worldHasShifted())
			yield return null;

		yield return new WaitForFixedUpdate();

		sceneDone = true;
	}

	void OnMouseUp()
	{
		if (stage4)
			clickedOn = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		touches++;

		if (!firstTouch && touches == 2)
		{
			StartCoroutine(Cutscene());
			firstTouch = true;
		}
	}


}
