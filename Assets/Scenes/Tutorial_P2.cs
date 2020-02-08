using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_P2 : MonoBehaviour
{
	public GameObject Camera, Mao1, Mao2, ContinueBtn, RightBtn, LeftBtn, UpBtn, SwitchBtn, prevCutscene, caixa1, caixa2;

	public float finalX, time;
	private float step, distTotal;

	public Vector3 finalPos;

	public GameObject wrdControl;
	private WorldSwitchScript wrdScript;

	private bool firstTouch = false;
	public bool forcedEnd = false;
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

	}

	private IEnumerator Cutscene()
	{
		while (!wrdScript.worldHasShifted())
			yield return null;

		Camera.GetComponent<SmoothCameraScript>().enabled = false;

		RightBtn.GetComponent<Image>().raycastTarget = false;
		LeftBtn.GetComponent<Image>().raycastTarget = false;
		UpBtn.GetComponent<Image>().raycastTarget = false;
		SwitchBtn.GetComponent<Image>().raycastTarget = false;

		distTotal = finalX - Camera.transform.position.x;

		Mao1.SetActive(true);
		Mao2.SetActive(true);

		StartCoroutine(ForceEnd());

		while (Camera.transform.position.x < finalX-0.5f)
		{
			Camera.transform.position = Vector3.Lerp(Camera.transform.position, finalPos, time);

			if (forcedEnd)
				break;
			
			yield return null;
		}

		while (!wrdScript.worldHasShifted())
		{
			if (forcedEnd)
				break;
			
			yield return null;
		}

		//ContinueBtn.SetActive(false);
		Mao1.SetActive(false);
		Mao2.SetActive(false);

		RightBtn.GetComponent<Image>().raycastTarget = true;
		LeftBtn.GetComponent<Image>().raycastTarget = true;
		UpBtn.GetComponent<Image>().raycastTarget = true;
		SwitchBtn.GetComponent<Image>().raycastTarget = true;

		Camera.GetComponent<SmoothCameraScript>().enabled = true;

		sceneDone = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!firstTouch && prevCutscene.GetComponent<Tutorial_P1>().sceneDone)
		{
			StartCoroutine(Cutscene());
			firstTouch = true;
		}
	}

	private IEnumerator ForceEnd()
	{
		while (wrdScript.worldHasShifted())
			yield return null;
		
		while (!wrdScript.worldHasShifted())
		{
			if (caixa1.GetComponent<ShiftBehavior>().GetIsReal() && caixa2.GetComponent<ShiftBehavior>().GetIsReal())
				SwitchBtn.GetComponent<Image>().raycastTarget = true;
			else
				SwitchBtn.GetComponent<Image>().raycastTarget = false;

			yield return null;
		}

		forcedEnd = true;
	}
}
