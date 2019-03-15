using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNiveis : MonoBehaviour {

    public GameObject CanvasManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowHideLevelButtons()
    {
        CanvasManager.GetComponent<CanvasManagement>().SetState(CanvasManagement.States.Niveis);
    }
}
