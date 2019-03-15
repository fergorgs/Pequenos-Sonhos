using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagement : MonoBehaviour {

    public enum States { Deafult, Niveis, Doar }

    public GameObject btnsDefault, btnsLangs, btnsNiveis, btnFechar, doarAssets;

    public GameObject titulo, passaro;

    public GameObject whiteWall;

    private States state = States.Deafult; public void SetState(States stt) { state = stt; } public States GetStates() { return state; }

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {

            case States.Deafult:

                btnsDefault.SetActive(true);
				btnsLangs.SetActive(true);
				titulo.SetActive(true);
                passaro.SetActive(true);
                whiteWall.SetActive(false);
                btnsNiveis.SetActive(false);
                btnFechar.SetActive(false);
                doarAssets.SetActive(false);
                break;

            case States.Niveis:

                btnsDefault.SetActive(false);
				btnsLangs.SetActive(false);
				titulo.SetActive(false);
                passaro.SetActive(false);
                whiteWall.SetActive(true);
                btnsNiveis.SetActive(true);
                btnFechar.SetActive(true);
                break;

            case States.Doar:

                btnsDefault.SetActive(false);
				btnsLangs.SetActive(false);
				titulo.SetActive(false);
                passaro.SetActive(false);
                whiteWall.SetActive(false);
                btnsNiveis.SetActive(false);
                btnFechar.SetActive(true);
                doarAssets.SetActive(true);
                break;
        }
    }
}
