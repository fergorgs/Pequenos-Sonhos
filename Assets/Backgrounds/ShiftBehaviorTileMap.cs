using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftBehaviorTileMap : MonoBehaviour {

    public GameObject wrdControl;
    private Camera mainCamera;
    private WorldSwitchScript wrdSwitch;

    private Renderer tmrd;
    public bool startsReal = false;

    private bool isReal;
    public bool GetIsReal() { return isReal; }

    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        isReal = startsReal;

        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        tmrd = GetComponent<Renderer>();
        if (startsReal)
        {
            tmrd.enabled = true;
        }
        else
        {
            tmrd.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wrdSwitch.worldHasShifted())
        {
            if (wrdSwitch.worldIsReal() == isReal)
            {
                tmrd.enabled = true;
            }
            else
            {
                tmrd.enabled = false;
            }
        }
    }
}
