using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_P3 : MonoBehaviour
{
	public GameObject Camera, playerActor, caixa, caixaDummy, Mao1, Mao2, ContinueBtn, RightBtn, LeftBtn, UpBtn, SwitchBtn, prevCutscene;

	public float time;
	private float step, distTotal, finalX;

	public Vector3 finalPos;
	private Vector3 caixaOrigPos;

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

		btnTextColor = ContinueBtn.GetComponentInChildren<Text>().color;

		Camera = GameObject.FindGameObjectWithTag("MainCamera");

		finalX = finalPos.x;

		

	}

	// Update is called once per frame
	void Update()
	{
		if(!firstTouch)
			GetComponent<BoxCollider2D>().isTrigger = wrdScript.worldIsReal();
	}

	private IEnumerator Cutscene()
	{
		caixaOrigPos = caixa.transform.position;

		caixa.GetComponent<ShiftBehavior>().isShiftable = true;

		Mao1.SetActive(true);

		Time.timeScale = 0;
		
		while (!wrdScript.worldHasShifted())
			yield return null;

		Mao1.SetActive(false);
		Time.timeScale = 1;

		caixaDummy.transform.position = caixa.transform.position;

		for(int i = 0; i < 2; i++)
			yield return new WaitForEndOfFrame();

		caixa.GetComponent<ShiftBehavior>().enabled = false;
		caixa.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
		caixaDummy.SetActive(true);
		playerActor.SetActive(true);

		Camera.GetComponent<SmoothCameraScript>().enabled = false;

		RightBtn.GetComponent<Image>().raycastTarget = false;
		LeftBtn.GetComponent<Image>().raycastTarget = false;
		UpBtn.GetComponent<Image>().raycastTarget = false;
		SwitchBtn.GetComponent<Image>().raycastTarget = false;

		caixa.GetComponent<ShiftBehavior>().isShiftable = false;

		distTotal = finalX - Camera.transform.position.x;

		while (Camera.transform.position.x < finalX - 0.5f)
		{
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, finalPos, time);

			yield return null;
		}

		playerActor.GetComponent<PlayerActingCommands>().PressRight(5);

		yield return new WaitForSeconds(5);

		caixa.GetComponent<ShiftBehavior>().enabled = true;

		//ContinueBtn.SetActive(false);
		Mao2.SetActive(true);

		while (!wrdScript.worldHasShifted())
			yield return null;

		Mao2.SetActive(false);
		playerActor.SetActive(false);
		caixaDummy.SetActive(false);
		caixa.transform.position = caixaOrigPos;

		RightBtn.GetComponent<Image>().raycastTarget = true;
		LeftBtn.GetComponent<Image>().raycastTarget = true;
		UpBtn.GetComponent<Image>().raycastTarget = true;
		SwitchBtn.GetComponent<Image>().raycastTarget = true;

		caixa.GetComponent<ShiftBehavior>().isShiftable = true;

		Camera.GetComponent<SmoothCameraScript>().enabled = true;

		sceneDone = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!firstTouch && prevCutscene.GetComponent<Tutorial_P2>().sceneDone)
		{
			StartCoroutine(Cutscene());
			firstTouch = true;
		}
	}
}
