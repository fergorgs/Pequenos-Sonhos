using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Tutorial : MonoBehaviour
{
    public enum State { SemPickUp, ComPickUp, PickUpUsado };

    public GameObject player;
    public float playerMinX;
    private State state = State.SemPickUp;
    
    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    public GameObject text1, text2, text3, text4;
    public GameObject seta;

	// Use this for initialization
	void Start ()
    {
        if (wrdControl == null)
        {
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
            wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();
        }

        text1.SetActive(true);
        text2.SetActive(true);
        text3.SetActive(false);
        text4.SetActive(false);
        seta.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.SemPickUp)
        {
            if (wrdSwitch.worldHasShifted())
            {
                text1.SetActive(!text1.activeSelf);
            }
            if (player.GetComponent<PlayerBehavior>().hasPickUp)
                state = State.ComPickUp;
        }
        else if (state == State.ComPickUp)
        {

            text1.SetActive(false);
            text3.SetActive(true);
            Debug.Log("player.x = " + player.transform.position.x + "\nplayerMinX = " + playerMinX);
            if (player.transform.position.x >= playerMinX)
                seta.SetActive(true);
            else
                seta.SetActive(false);

            if (!player.GetComponent<PlayerBehavior>().hasPickUp)
                state = State.PickUpUsado;
        }
        else
        {
            text2.SetActive(false);
            text3.SetActive(false);
            seta.SetActive(false);
            text4.SetActive(true);
        }
    }
}
