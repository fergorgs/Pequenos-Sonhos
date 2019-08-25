using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour {

    public GameObject player;
    public GameObject worldSwtCtr;
    private GameObject bird;
    private WorldSwitchScript wrdSwcScr;
    private AudioSource sfx;
    private Animator animator;

    public bool singleUse = false;
    public float timetoReset = 3;

    private bool active = false;
    public bool IsActive() { return active; }
    private bool inRange = false;

	// Use this for initialization
	void Start () {
        sfx = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        bird = GameObject.FindGameObjectWithTag("bird");

        if (worldSwtCtr == null)
            worldSwtCtr = GameObject.FindGameObjectWithTag("WCS");

        wrdSwcScr = worldSwtCtr.GetComponent<WorldSwitchScript>();
    }
	
	// Update is called once per frame
	void Update () {

        if (wrdSwcScr.worldHasShifted())
            active = false;

        //Debug.Log("active = " + active);

    }


    void OnMouseUp() {
        if (inRange)
        {
            Activate();
            animator.SetTrigger("buttonPressed");
        }
        /*else if (bird != null)
        {
            bird.GetComponent<IdleBehavior>().moveToPress(gameObject);
        }*/
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Player")
            inRange = true;
		GetComponent<SpriteRenderer>().color = Color.white;
    }

    void OnTriggerExit2D(Collider2D col) {

        if (col.tag == "Player")
            inRange = false;
		GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1);
	}

    public void Activate()
    {
        if (active == false)
        {
            active = true;
            sfx.Play();
            StartCoroutine(BtnReset());
        }
    }

    private IEnumerator BtnReset() {
        yield return new WaitForSeconds(timetoReset);
        active = false;
    }

}
