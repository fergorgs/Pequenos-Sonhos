using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambiarraEnemy3 : MonoBehaviour {
    
    public GameObject WorldSwitchController;
    private WorldSwitchScript wrdSwtScr;

    public GameObject plataforma;
    private Vector3 pfmPos;
    private ShiftBehavior sftBhv;

    public float upperBound;

    private BoxCollider2D bc2d;

	// Use this for initialization
	void Start ()
    {
        WorldSwitchController = GameObject.FindGameObjectWithTag("WCS");
        wrdSwtScr = WorldSwitchController.GetComponent<WorldSwitchScript>();

        bc2d = GetComponent<BoxCollider2D>();

        sftBhv = plataforma.GetComponent<ShiftBehavior>();

    }
	
	// Update is called once per frame
	void Update () {

        pfmPos = plataforma.transform.position;

        if (sftBhv.GetIsReal() != wrdSwtScr.worldIsReal() || pfmPos.y != upperBound)
            bc2d.enabled = true;
        else
            bc2d.enabled = false;
	}
}
