using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButtonEffect : MonoBehaviour
{
	public enum TurnType { Still, FastClock, SlowClock, FastCounter, SlowCounter };

	public GameObject wrdControl;
	private WorldSwitchScript wrdSwitch;

	public Sprite realSprite, dreamSprite;

	public float fastDuration = 500f;
	public float slowDuration;
	public TurnType tt;
	private RectTransform rct;

	private bool doneTurn = true;

	private float startTime;
	//private float tFJ;

	//private bool turnButton = false;
	//private bool turnClockWise = true;

	//private bool abortTurn = false;

	// Start is called before the first frame update
	void Start()
	{
		wrdControl = GameObject.FindGameObjectWithTag("WCS");
		wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

		tt = TurnType.Still;
		rct = GetComponent<RectTransform>();

		//Debug.Log("fastDuration = " + fastDuration);
		//fastTime = (float)(293.3 - 7.11 * fastDuration);
	//	/Debug.Log("fastTime = " + fastTime);
		//slowDuration = (float)((293.3-(wrdSwitch.timeToReset-0.2-fastTime))/7.11);
		//Debug.Log("slowDur = " + slowDuration);
	}

	// Update is called once per frame
	void Update()
    {

		if (wrdSwitch.worldHasShifted())
		{
			//Debug.Log("entra 1");
			if (wrdSwitch.worldIsReal())
			{
				if (tt != TurnType.FastCounter)
				{
					StopAllCoroutines();
					StartCoroutine(Turn(rct.eulerAngles, new Vector3(0, 0, 360), fastDuration));
					tt = TurnType.FastCounter;
					doneTurn = false;
				}
			}
			else
			{
				//Debug.Log("entra 2");
				if (tt != TurnType.FastClock)
				{
					//Debug.Log("entra 3");
					//Debug.Log("aqui");
					StopAllCoroutines();
					StartCoroutine(Turn(rct.eulerAngles, new Vector3(0, 0, -360), fastDuration));
					tt = TurnType.FastClock;
					doneTurn = false;
				}
			}
		}

		if (!wrdSwitch.worldIsReal())
		{
			if (tt != TurnType.SlowCounter && doneTurn)
			{
				StopAllCoroutines();
				StartCoroutine(Turn(rct.eulerAngles, new Vector3(0, 0, 360), slowDuration));
				tt = TurnType.SlowCounter;
				doneTurn = false;
			}
		}

    }

	private IEnumerator Turn(Vector3 posIni, Vector3 posFin, float speed)
	{
		float step = (speed / (posIni - posFin).magnitude) * Time.fixedDeltaTime;
		float t = 0;

		startTime = Time.time;

		while (t <= 1.0f)
		{

			//pb.rb2d.velocity = new Vector2(speed, 0f);
			t += step;
			GetComponent<RectTransform>().eulerAngles = Vector3.Lerp(posIni, posFin, t);

			yield return new WaitForFixedUpdate();
		}
		GetComponent<RectTransform>().eulerAngles = posFin;
		doneTurn = true;
		//Debug.Log("Done");

		if (tt == TurnType.SlowCounter)
			Debug.Log("f(" + speed + ") = " + (Time.time - startTime));
		tt = TurnType.Still;
	}
}
