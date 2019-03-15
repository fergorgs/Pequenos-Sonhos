using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger4 : MonoBehaviour {

    public enum State { Pre, Prob, Cami, Corra };
    private State state = State.Pre;

    public GameObject WorldController;
    private WorldSwitchScript wrdSftScr;

    public GameObject text1, text2, text3, text4, text5, text6;
    public GameObject seta1, seta2, seta3;

    private int tentativas = 0;
    private bool coolDownTent = false;
    private float coolDownTentTime = 2;
    public float doorTime;

    public GameObject botao;
    public GameObject player;
    public float playerMinX;
    public float playerMaxX;

	// Use this for initialization
	void Start (){

        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
        switch (state)
        {

            case State.Pre:

                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(false);
                text4.SetActive(false);
                text5.SetActive(false);
                text6.SetActive(false);
                seta1.SetActive(false);
                seta2.SetActive(false);
                seta3.SetActive(false);
                if (tentativas >= 3)
                    StartCoroutine(WaitToChange());
                break;

            case State.Prob:

                text1.SetActive(!(player.transform.position.x > playerMinX));
                text2.SetActive(player.transform.position.x > playerMinX);
                text3.SetActive(false);
                text4.SetActive(false);
                text5.SetActive(false);
                text6.SetActive(false);
                seta1.SetActive(!(player.transform.position.x > playerMinX));
                seta2.SetActive(false);
                seta3.SetActive(false);
                if (!wrdSftScr.worldIsReal() && player.transform.position.x >= playerMinX)
                    state = State.Cami;
                break;

            case State.Cami:

                if (wrdSftScr.worldIsReal())
                    state = State.Prob;
                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(true);
                text4.SetActive(true);
                text5.SetActive(true);
                text6.SetActive(false);
                seta1.SetActive(false);
                seta2.SetActive(true);
                seta3.SetActive(false);
                if (botao.GetComponent<ButtonBehavior>().IsActive())
                {
                    state = State.Corra;
                    StartCoroutine(DoorTime());
                }
                break;

            case State.Corra:

                text1.SetActive(false);
                text2.SetActive(false);
                text3.SetActive(false);
                text4.SetActive(false);
                text5.SetActive(false);
                text6.SetActive(true);
                seta1.SetActive(false);
                seta2.SetActive(false);
                seta3.SetActive(true);
                break;
        }

		if (botao.GetComponent<ButtonBehavior>().IsActive() && !coolDownTent)
		{
			Debug.Log("Entra");
			coolDownTent = true;
			tentativas++;
			StartCoroutine(CoolDownTent());
		}
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        /*if (botao.GetComponent<ButtonBehavior>().IsActive() && !coolDownTent)
        {
            Debug.Log("Entra");
            coolDownTent = true;
            tentativas++;
            StartCoroutine(CoolDownTent());
        }*/

    }

    private IEnumerator CoolDownTent()
    {
        yield return new WaitForSeconds(coolDownTentTime);

        coolDownTent = false;
    }

    private IEnumerator DoorTime()
    {
        yield return new WaitForSeconds(doorTime);

        if (player.transform.position.x < playerMaxX)
            state = State.Prob;
        else
        {
            seta3.SetActive(false);
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitToChange()
    {
        yield return new WaitForSeconds(2);
        state = State.Prob;
    }
}
