using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaMovelScript : MonoBehaviour {

    private Rigidbody2D rb2d;
    private AudioSource sfx;
    private Collider2D col;
    private WorldSwitchScript wcs;
    private ShiftBehavior sftBh;
    private bool noCollision = false;
    private bool onPlatform = false;
	// Use this for initialization
	void Start () {
        sfx = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        wcs = GameObject.FindWithTag("WCS").GetComponent< WorldSwitchScript >();
        sftBh = GetComponent<ShiftBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if((sfx.isPlaying && (rb2d.velocity.x == 0) || rb2d.velocity.y != 0)) {
            sfx.Stop();
        }else if(rb2d.velocity.x != 0 && !sfx.isPlaying && rb2d.velocity.y == 0) {
            sfx.Play();
        }

        if (wcs.worldIsReal() != sftBh.GetIsReal())
            sfx.Stop();

        if(transform.parent != null && transform.parent.tag == "Plataforma")
            onPlatform = true;
        else
            onPlatform = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (onPlatform)   //se a caixa está na plataforma e colide com o player
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.transform.position.y < transform.position.y)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
                noCollision = true;
            }
        }
        else */if (collision.CompareTag("Player") && rb2d.velocity.y == 0)
        {
            sfx.Play();
            collision.GetComponent<Collider2D>().transform.SetParent(transform);
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        /*if (onPlatform)
        {
            if (noCollision = true && col.gameObject.tag == "Player")
            {
                if (col.gameObject.transform.position.y > transform.position.y)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), false);
                    noCollision = false;
                }
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (onPlatform)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>(), true);
            noCollision = true;
        }*/
        collision.GetComponent<Collider2D>().transform.SetParent(null);
    }
}
