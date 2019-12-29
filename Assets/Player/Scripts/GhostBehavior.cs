using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{

    public GameObject wrdControl;

    private WorldSwitchScript wrdCont;

    public GameObject player;

    public bool alert = false;

    private Transform plrTrfm;
    private Vector3 plrPos;
    private Vector3 lastDreamPos; public Vector3 GetLastDreamPos() { return lastDreamPos; }
    private int lastFacing;
    private Transform trfm;
    private SpriteRenderer sprd;
    private Rigidbody2D rb2d;
    private float yDist;

	private Vector3 lastRealPos;
	
    private bool changed = false;
	private PlayerBehavior pb;
    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");

        wrdCont = wrdControl.GetComponent<WorldSwitchScript>();
		pb = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
        plrTrfm = player.GetComponent<Transform>();
        trfm = GetComponent<Transform>();
        sprd = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

		RaycastHit2D rh2d = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.down);

		if (rh2d.collider != null)
		{
			if (rh2d.collider.name == "R_Chao" || rh2d.collider.name == "R_Chao_Esq" || rh2d.collider.name == "R_Chao_Dir")
				lastRealPos = rh2d.collider.transform.position + new Vector3(0, 0.7f, 0);
		}

        plrPos = plrTrfm.position;

        if (wrdCont.worldIsReal())
        {
            // Debug.Log("Entra");
            trfm.position = plrPos - new Vector3(0, 1.3f, 0);
            sprd.enabled = false;
            rb2d.simulated = false;
            changed = false;
            
        }
        else
        {
            lastDreamPos = transform.position;
            if (!changed)
            {
                sprd.enabled = true;
                rb2d.simulated = true;
				rb2d.gravityScale = 1;
				//sprd.sprite = sprites[(int)pb.get_playerState()];
				/*if (pb.GetComponent<SpriteRenderer>().flipX)
                    sprd.flipX = true;
                else
                    sprd.flipX = false;*/
                changed = true;
            }

			if(transform.position.y < lastRealPos.y)
			{
				//Debug.Log("Detected");
				rb2d.velocity = Vector3.zero;
				rb2d.gravityScale = 0;

			}
				
        }

	}
}
