using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{
	public GameObject wrdControl;
	private WorldSwitchScript wrdScript;

	public Image img;

	public Animator anm;

	// Start is called before the first frame update
	void Start()
    {
		wrdControl = GameObject.FindGameObjectWithTag("WCS");
		wrdScript = wrdControl.GetComponent<WorldSwitchScript>();

		img = GetComponent<Image>();

		anm = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		img.enabled = !wrdScript.worldIsReal();

		if (wrdScript.worldHasShifted())
		{
			anm.CrossFade("Reset", 0);
		}

	}
}
