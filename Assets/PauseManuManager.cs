using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManuManager : MonoBehaviour {

    public bool doarIsOn = false;

    public GameObject defaultBtns, doarStuff;
	
	// Update is called once per frame
	void Update () {

        doarStuff.SetActive(doarIsOn);
        defaultBtns.SetActive(!doarIsOn);
	}

    public void SetDoarOn()
    {
        doarIsOn = true;
    }

    public void SetDoarOff()
    {
        doarIsOn = false;
    }
}
