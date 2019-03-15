using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapAvoidance : MonoBehaviour {

    public GameObject wrdControl;
   // public GameObject Player;

    private WorldSwitchScript wrdSwitch;

    private Vector3 curPos;

    // Use this for initialization
    void Start ()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");

        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

    }

    // Update is called once per frame
    void Update() {

        curPos = transform.position;

        //transform.position = new Vector3(20, 15, 0);

        if (wrdSwitch.worldHasShifted()) {

            //Physics2D.BoxCast(new Vector2(curPos.x, curPos.y - 1.2f), new Vector2(0.4f, 0.4f), 0, Vector2.down, 2);

            if (Physics2D.BoxCast(new Vector2(curPos.x, curPos.y - 1.2f), new Vector2(0.4f, 0.4f), 0, Vector2.down, 2)){

                Debug.Log("Overlap detected");

                if(!Physics2D.CircleCast(new Vector2(curPos.x, curPos.y + 2.5f), 1.05f, Vector2.right))
                    transform.position = new Vector3(curPos.x, curPos.y + 2.5f, curPos.z);
                else if (!Physics2D.CircleCast(new Vector2(curPos.x + 2.5f, curPos.y), 1.05f, Vector2.right))
                    transform.position = new Vector3(curPos.x + 2.5f, curPos.y, curPos.z);
                else if (!Physics2D.CircleCast(new Vector2(curPos.x - 2.5f, curPos.y), 1.05f, Vector2.right))
                    transform.position = new Vector3(curPos.x - 2.5f, curPos.y, curPos.z);
                else
                    transform.position = new Vector3(curPos.x, curPos.y - 2.5f, curPos.z);

            }
            else
                Debug.Log("Overlap not detected");
        }
    }
}
