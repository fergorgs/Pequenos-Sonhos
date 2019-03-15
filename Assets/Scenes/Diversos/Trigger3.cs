using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger3 : MonoBehaviour {

    public GameObject WorldController;
    private WorldSwitchScript wrdSftScr;

    private enum State { BlocoDir, BlocoEsq };
    private State state = State.BlocoDir;

    private float myX;

    public GameObject bloco;
    public GameObject text1, text2, text3;
    public GameObject seta;

	// Use this for initialization
	void Start ()
    {
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();

        myX = transform.position.x;
	}

    // Update is called once per frame
    void Update()
    {

        if (bloco.transform.position.x > myX)
            state = State.BlocoDir;
        else
            state = State.BlocoEsq;

        switch (state)
        {

            case State.BlocoDir:

                text3.SetActive(false);

                if (wrdSftScr.worldIsReal())
                {
                    text1.SetActive(true);
                    text2.SetActive(false);
                    seta.SetActive(false);
                }
                else
                {

                    text1.SetActive(false);
                    text2.SetActive(true);
                    seta.SetActive(true);
                }

                break;

            case State.BlocoEsq:

                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
                seta.SetActive(false);

                break;
        }
    }
}
