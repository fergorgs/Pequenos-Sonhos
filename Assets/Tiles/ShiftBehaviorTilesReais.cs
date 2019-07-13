using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftBehaviorTilesReais : MonoBehaviour
{

    public GameObject wrdControl;
    private Camera mainCamera;
    private WorldSwitchScript wrdSwitch;

    private SpriteRenderer[] sprd;
    //private Collider2D[] colliders;
    private Rigidbody2D rb2d;

    private Color color;
    public bool startsReal;
    public bool isShiftable;

    private bool isReal;
    public bool GetIsReal() { return isReal; }
    private bool clicked;
	public bool revealOnTouch = false;
    private bool touchingGhost = false;


    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        isReal = startsReal;

        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        sprd = GetComponentsInChildren<SpriteRenderer>();
        //colliders = GetComponents<Collider2D>();
        if (GetComponent<Rigidbody2D>() != null)
            rb2d = GetComponent<Rigidbody2D>();

        if (startsReal)
        {
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.white;
            }
            /*for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = true;*/
            gameObject.layer = 0;

            if (rb2d != null)
                rb2d.simulated = true;
        }
        else
        {
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.clear;
            }
            /*for (int i = 0; i < colliders.Length; i++)
                colliders[i].enabled = false;*/
            gameObject.layer = 8;
            if (rb2d != null)
                rb2d.simulated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (wrdSwitch.worldHasShifted())
        {
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.white;
            }
            clicked = false;

            if (wrdSwitch.worldIsReal() == isReal)
            {
                if (ColorUtility.TryParseHtmlString(wrdSwitch.colorReal, out color))
                {
                    color.a = 0;
                    mainCamera.backgroundColor = color;
                }
                foreach (SpriteRenderer s in sprd)
                {
                    s.color = Color.white;
                }
                /*for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = true;*/
                gameObject.layer = 0;
                if (rb2d != null)
                    rb2d.simulated = true;

            }
            else
            {
                if (ColorUtility.TryParseHtmlString(wrdSwitch.colorDream, out color))
                {
                    color.a = 0;
                    mainCamera.backgroundColor = color;
                }
                foreach (SpriteRenderer s in sprd)
                {
                    s.color = Color.clear;
                }
                /*for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = false;*/
                gameObject.layer = 8;
                if (rb2d != null)
                    rb2d.simulated = false;
            }
        }

    }


    void OnMouseUp()
    {

        if (isShiftable && clicked == false)
        {
            isReal = !isReal;
            clicked = true;
            foreach (SpriteRenderer s in sprd)
            {
                s.color = Color.magenta;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (revealOnTouch)
		{
			if (collision.gameObject.tag == "Ghost")
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
		}
    }

}