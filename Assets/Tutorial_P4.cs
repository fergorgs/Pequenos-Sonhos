using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_P4 : MonoBehaviour
{
	public GameObject text, button, Mao1, RightBtn, LeftBtn, UpBtn, SwitchBtn, prevCutscene, panel;

	/*public float time;
	private float step, distTotal, finalX;*/

	/*public Vector3 finalPos;
	private Vector3 caixaOrigPos;*/

	public GameObject wrdControl;
	private WorldSwitchScript wrdScript;

	private bool firstTouch = false;
	public bool sceneDone = false;

	/*private bool clickedOn = false, stage4 = false, firstTouch = false;

	private float startTime = 0;*/

	private Color btnTextColor;

	// Start is called before the first frame update
	void Start()
	{
		wrdControl = GameObject.FindGameObjectWithTag("WCS");
		wrdScript = wrdControl.GetComponent<WorldSwitchScript>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private IEnumerator Cutscene()
	{
		Time.timeScale = 0;

		text.SetActive(true);
		Mao1.SetActive(true);
		panel.SetActive(true);

		RightBtn.GetComponent<Image>().raycastTarget = false;
		LeftBtn.GetComponent<Image>().raycastTarget = false;
		UpBtn.GetComponent<Image>().raycastTarget = false;
		SwitchBtn.GetComponent<Image>().raycastTarget = false;

		while (!button.GetComponent<ButtonBehavior>().IsActive())
			yield return null;

		Time.timeScale = 1;

		text.SetActive(false);
		Mao1.SetActive(false);
		panel.SetActive(false);

		RightBtn.GetComponent<Image>().raycastTarget = true;
		LeftBtn.GetComponent<Image>().raycastTarget = true;
		UpBtn.GetComponent<Image>().raycastTarget = true;
		SwitchBtn.GetComponent<Image>().raycastTarget = true;

		sceneDone = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!firstTouch && prevCutscene.GetComponent<Tutorial_P3>().sceneDone)
		{
			StartCoroutine(Cutscene());
			firstTouch = true;
		}
	}
}
