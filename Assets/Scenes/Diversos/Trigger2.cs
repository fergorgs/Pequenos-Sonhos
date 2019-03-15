using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2 : MonoBehaviour {

    public enum State { Pre, Real1, Sonho, Real2 };

    public GameObject WorldController;
    private WorldSwitchScript wrdSftScr;

    public GameObject text1, text2;
    public GameObject seta1, seta2;
    public GameObject bloco;
    public GameObject player;

    private State state = State.Pre;
    public void SetState(State val) { state = val; }
    public State GetState() { return state; }

    // Use this for initialization
    void Start ()
    {
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {

            case State.Pre:

                text1.SetActive(true);
                text2.SetActive(false);
                seta1.SetActive(false);
                seta2.SetActive(false);
                break;

            case State.Real1:

                text1.SetActive(true);
                text2.SetActive(false);
                seta1.SetActive(true);
                seta2.SetActive(false);
                if (wrdSftScr.worldHasShifted())
                    state = State.Sonho;

                break;

            case State.Sonho:

                text1.SetActive(false);
                text2.SetActive(true);
                seta1.SetActive(false);
                seta2.SetActive(true);
                if (bloco.GetComponent<ShiftBehavior>().GetIsReal())
                    state = State.Real2;

                break;

            case State.Real2:

                text1.SetActive(false);
                text2.SetActive(true);
                seta1.SetActive(false);
                seta2.SetActive(false);
                if (player.transform.position.x > 145)
                    Destroy(gameObject);

                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Pre)
            state = State.Real1;
    }
}
