using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraFocus : MonoBehaviour
{
	private GameObject mainCamera;

	public Vector3 targetPos;
	public bool changeCamera = false;
	public float durationInSeconds = 2;

	private float startTime;
	private bool refreshing = false;

	void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update()
	{
		if (changeCamera)
		{
			Debug.Log("Hey 1");
			if (GetComponent<ButtonBehavior>().IsActive())
			{
				Debug.Log("Hey 2");
				if (!refreshing)
				{
					Debug.Log("Hey 3");
					StartCoroutine(ChangeFocus());
					refreshing = true;
				}
			}
		}	
	}

	private IEnumerator ChangeFocus()
	{
		Debug.Log("Called button");
		mainCamera.GetComponent<SmoothCameraScript>().target = null;// transCopy;
		mainCamera.GetComponent<SmoothCameraScript>().SetVectorTarget(targetPos);

		startTime = Time.time;

		while (Time.time - startTime < 0.5f)
			yield return null;

		mainCamera.GetComponent<SmoothCameraScript>().enabled = false;

		yield return new WaitForSeconds(durationInSeconds);
		mainCamera.GetComponent<SmoothCameraScript>().enabled = true;

		mainCamera.GetComponent<SmoothCameraScript>().target = mainCamera.GetComponent<SmoothCameraScript>().GetDefaultTarget();

		refreshing = false;

	}
}
