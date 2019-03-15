using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltroSonhoScript : MonoBehaviour {
    
    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    private Image img;

	// Use this for initialization
	void Start ()
    {
        wrdControl = GameObject.FindGameObjectWithTag("WCS");
        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (wrdSwitch.worldHasShifted())
        {
            if (wrdSwitch.worldIsReal())
                img.color = Color.clear;
            else
                img.color = Color.white;
        }
        
	}
}
