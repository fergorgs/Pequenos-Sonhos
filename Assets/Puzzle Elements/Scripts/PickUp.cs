﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {


    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject spriteCanvas;
	// Use this for initialization
	void Start () {

		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllingScript>();

		pb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
		//spriteCanvas = Fi
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Player")
		{
			if (!pc.HasPickUp())
			{
				//Debug.Log("Entra");
				if (spriteCanvas != null)
					spriteCanvas.GetComponent<Image>().enabled = true;
				pc.SetPickUp(true);
				//Debug.Log("Entra\npb.hasPickUp is now: " + pb.hasPickUp);
				Destroy(gameObject);
			}
		}


		//old stuff------------------------------------------
		if (collision.tag == "Player")
        {
            if (!pb.hasPickUp)
            {
                //Debug.Log("Entra");
                if (spriteCanvas != null)
                    spriteCanvas.GetComponent<Image>().enabled = true;
                pb.hasPickUp = true;
                //Debug.Log("Entra\npb.hasPickUp is now: " + pb.hasPickUp);
                Destroy(gameObject);
            }
        }
    }

    /*private void OnMouseUp() {
        if(Mathf.Abs(pb.transform.position.x - transform.position.x) < 1f) {
            if (spriteCanvas != null)
                spriteCanvas.GetComponent<Image>().enabled = true;
            pb.hasPickUp = true;
            Destroy(gameObject);
        }
        
    }*/
}
