using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IdleBehavior : MonoBehaviour {

    public enum States { Idle, PressingBtn };

    private States birdState;
    public States get_birdState() { return birdState; }
    public void set_birdState(States state) { birdState = state; }

    public GameObject WrdSwtCon;
    private WorldSwitchScript wrdSwtScr;
    public GameObject Player;
    private PlayerBehavior plrBhr;
    private SpriteRenderer sprd;

    private PlayerBehavior.States plrState;
    private PlayerBehavior.Sides plrSide;
	
    private Transform btnCoord;

    private Vector3 anchorPoint;
    private Vector3 idlePoint1;
    private Vector3 idlePoint2;
    private Vector3 velocity = Vector3.zero;

    private bool idleGoingDown = true;
    private bool pressingGoingAway = false;

    public float movementDelay = 0.5f;
    public float idleAmplitude = 0.2f;
    public float idleLimitTolerance = 0.01f;
    public float idleSpeed = 0.3f;
    public float pressingSpeed = 0.5f;

    public bool isReal = false;

	private bool isCutScene = false;
	public void SetIsCutScene(bool val) { isCutScene = val; }

	// Use this for initialization
	void Start () {

        sprd = GetComponent<SpriteRenderer>();
        WrdSwtCon = GameObject.FindGameObjectWithTag("WCS");
        wrdSwtScr = WrdSwtCon.GetComponent<WorldSwitchScript>();

        plrBhr = Player.GetComponent<PlayerBehavior>();

        birdState = States.Idle;
        if(!isReal)
            sprd.color = new Color(1, 1, 1, 0.3f);
    }


    // Update is called once per frame
    void Update()
    {
		if (!isCutScene)
		{
			if (!Player.GetComponent<SpriteRenderer>().flipX)
			{
				anchorPoint = Player.transform.position + new Vector3(-1, 2, 0);
				GetComponent<SpriteRenderer>().flipX = false;
			}
			else
			{
				anchorPoint = Player.transform.position + new Vector3(1, 2, 0);
				GetComponent<SpriteRenderer>().flipX = true;
			}
		}
		else
		{
			anchorPoint = Player.transform.position + new Vector3(-1, 2, 0);
			GetComponent<SpriteRenderer>().flipX = false;
		}

		if (Mathf.Abs(Player.GetComponent<Rigidbody2D>().velocity.x) < 0.3f)
		{
			idlePoint1 = anchorPoint + new Vector3(0, idleAmplitude, 0);
			idlePoint2 = anchorPoint + new Vector3(0, -idleAmplitude, 0);

			if (idleGoingDown)
			{
				transform.position = Vector3.SmoothDamp(transform.position, idlePoint2, ref velocity, idleSpeed);
				if (transform.position.y < idlePoint2.y + idleLimitTolerance)
					idleGoingDown = false;
			}
			else
			{
				transform.position = Vector3.SmoothDamp(transform.position, idlePoint1, ref velocity, idleSpeed);
				if (transform.position.y > idlePoint1.y - idleLimitTolerance)
					idleGoingDown = true;
			}
		}
		else
			transform.position = Vector3.SmoothDamp(transform.position, anchorPoint, ref velocity, movementDelay);

		if (!isReal)
		{
			if (wrdSwtScr.worldHasShifted())
			{
				if (wrdSwtScr.worldIsReal())
					sprd.color = new Color(1, 1, 1, 0.3f);
				else
					sprd.color = Color.white;
			}
		}













		//---------------------------ANTIGO---------------------------------------------------
		plrSide = plrBhr.get_facing();
           
        plrState = plrBhr.get_playerState();

        if (plrSide == PlayerBehavior.Sides.Direita) {
            anchorPoint = Player.transform.position + new Vector3(-1, 2, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
            anchorPoint = Player.transform.position + new Vector3(1, 2, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (plrState == PlayerBehavior.States.Parado)
        {
            idlePoint1 = anchorPoint + new Vector3(0, idleAmplitude, 0);
            idlePoint2 = anchorPoint + new Vector3(0, -idleAmplitude, 0);

            if (idleGoingDown)
            {
                transform.position = Vector3.SmoothDamp(transform.position, idlePoint2, ref velocity, idleSpeed);
                if (transform.position.y < idlePoint2.y + idleLimitTolerance)
                    idleGoingDown = false;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, idlePoint1, ref velocity, idleSpeed);
                if (transform.position.y > idlePoint1.y - idleLimitTolerance)
                    idleGoingDown = true;
            }
        }
        else
            transform.position = Vector3.SmoothDamp(transform.position, anchorPoint, ref velocity, movementDelay);

        if (!isReal)
        {
            if (wrdSwtScr.worldHasShifted())
            {
                if (wrdSwtScr.worldIsReal())
                    sprd.color = new Color(1, 1, 1, 0.3f);
                else
                    sprd.color = Color.white;
            }
        }
    }

}
