using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitchBehavior : MonoBehaviour
{

    public GameObject wrdControl;
    private WorldSwitchScript wrdCon;

    private PlayerBehavior plrBh;

    public GameObject ghost;

    private Transform trfm;
    private Vector2 lastPos;
    private Rigidbody2D rb2d;
    private Vector2 lastVel;
    private Vector3 safePos;

    private PlayerBehavior.States lastState;
    private PlayerBehavior.Sides lastSide;

    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");

        wrdCon = wrdControl.GetComponent<WorldSwitchScript>();

        plrBh = GetComponent<PlayerBehavior>();

        trfm = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (wrdCon.worldHasShifted())
        {
            if (wrdCon.worldIsReal())
            {
                if (ghost.GetComponent<GhostBehavior>().alert)
                {
                    transform.position = safePos;
                    rb2d.velocity = Vector3.zero;
                    plrBh.set_playerState(PlayerBehavior.States.Parado);
                }
                else
                {
                    transform.position = ghost.GetComponent<GhostBehavior>().GetLastDreamPos() + new Vector3(0, 1, 0);
                    rb2d.velocity = Vector3.zero;
                    plrBh.set_playerState(PlayerBehavior.States.Parado);
                }
            }
            else
                safePos = transform.position;
        }
    }
}

