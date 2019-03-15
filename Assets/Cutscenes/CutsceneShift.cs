using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneShift : MonoBehaviour {

    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    public GameObject pauseControll;
    private PauseScript pauseScript;

    private SpriteRenderer sprd;

    private Color color;
    public bool startsReal;
    public bool isShiftable;

    private bool isReal;
    public bool GetIsReal() { return isReal; }
    private bool clicked;

    public float birdTransVal = 0.3f;


    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
        isReal = startsReal;

        pauseControll = GameObject.FindGameObjectWithTag("PC");
        pauseScript = pauseControll.GetComponent<PauseScript>();

        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        sprd = GetComponent<SpriteRenderer>();

        if (startsReal)
            sprd.color = Color.white;
        else
            sprd.color = new Color(1, 1, 1, birdTransVal);
    }

    // Update is called once per frame
    void Update()
    {

        if (wrdSwitch.worldHasShifted())
        {
            sprd.color = Color.white;

            clicked = false;

            if (wrdSwitch.worldIsReal() == isReal)
                sprd.color = Color.white;
            else
                sprd.color = new Color(1, 1, 1, birdTransVal);
        }
    }


    void OnMouseUp()
    {
        if (!pauseScript.IsPaused())
        {
            if (isShiftable && clicked == false && !wrdSwitch.worldIsReal())
            {
                isReal = !isReal;
                clicked = true;
                sprd.color = Color.magenta;
            }
        }
    }
}
