using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaMovelScript : MonoBehaviour {

	public float drag = 2;
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
		if((sfx.isPlaying && (Mathf.Abs(rb2d.velocity.x) < 0.3f || Mathf.Abs(rb2d.velocity.y) > 0.3f))) {
            sfx.Stop();
        }else if(Mathf.Abs(rb2d.velocity.x) > 0.3f && !sfx.isPlaying && Mathf.Abs(rb2d.velocity.y) < 0.3f) {
            sfx.Play();
        }

        if (wcs.worldIsReal() != sftBh.GetIsReal())
            sfx.Stop();

		rb2d.AddForce(new Vector2(-drag * rb2d.velocity.x, 0));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player") && rb2d.velocity.y == 0)
            sfx.Play();
    }
}
