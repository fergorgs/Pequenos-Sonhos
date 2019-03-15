﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

    public enum Type { Loop, LoopOnce, SingleMovement, NoButton };

    private Vector2 startPosition;
    public Vector2 finalPosition;
    private Vector2 currentPos;
    private Vector3 buttonPos;

    public Type type;

    public GameObject WorldController;

    private WorldSwitchScript wrdSftScr;
    private ShiftBehavior sftBeh;
    private LineRenderer lird;

    public GameObject[] buttons;

    private bool activated = false;
    public float timeMov;
    public float timeToKillLine = 2f;

    private void Start()
    {
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();
        sftBeh = GetComponent<ShiftBehavior>();

        if (buttons[0] != null)
            buttonPos = buttons[0].transform.position;
        lird = GetComponent<LineRenderer>();
        lird.enabled = false;
        lird.SetPosition(0, transform.position);
        lird.SetPosition(1, buttonPos);

        startPosition = transform.position;
    }

    public IEnumerator MoveProp(Vector2 pos)
    {
        var t = 0f;
        var currentPosition = transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / timeMov;
            transform.position = Vector2.Lerp(currentPosition, pos, t);
            yield return null;
        }
    }

    private bool AllBtnsActive()
    {

        int btnsActive = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<ButtonBehavior>().IsActive())
                btnsActive++;
        }

        if (btnsActive == buttons.Length)
            return true;
        else
            return false;
    }

    private void Update()
    {
        currentPos = transform.position;

        lird.SetPosition(0, currentPos);

        switch (type)
        {

            case Type.NoButton:

                if (currentPos == startPosition)
                    StartCoroutine(MoveProp(finalPosition));
                else if (currentPos == finalPosition)
                    StartCoroutine(MoveProp(startPosition));
                break;

            case Type.Loop:

                if (activated)
                {
                    if (currentPos == startPosition)
                        StartCoroutine(MoveProp(finalPosition));
                    else if (currentPos == finalPosition)
                        StartCoroutine(MoveProp(startPosition));
                }
                else if (AllBtnsActive())
                {
                    lird.enabled = true;
                    StartCoroutine(TurnOffButtonLine());
                    activated = true;
                }
                break;

            case Type.LoopOnce:

                if (currentPos == startPosition && AllBtnsActive())
                {
                    lird.enabled = true;
                    StartCoroutine(TurnOffButtonLine());
                    StartCoroutine(MoveProp(finalPosition));
                }
                else if (currentPos == finalPosition)
                    StartCoroutine(MoveProp(startPosition));
                break;

            case Type.SingleMovement:

                if (AllBtnsActive())
                {
                    lird.enabled = true;
                    StartCoroutine(TurnOffButtonLine());
                    if (currentPos == startPosition)
                        StartCoroutine(MoveProp(finalPosition));
                    else if (currentPos == finalPosition)
                        StartCoroutine(MoveProp(startPosition));
                }
                break;
        }

    }

    private IEnumerator TurnOffButtonLine()
    {
        yield return new WaitForSeconds(timeToKillLine);

        lird.enabled = false;
        //Debug.Log("Out");
    }
}
