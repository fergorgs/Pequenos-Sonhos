using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    private Collider2D[] colliders;

	// Use this for initialization
	void Start () {

        colliders = GetComponents<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < colliders.Length; i++)
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), colliders[i], true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < colliders.Length; i++)
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), colliders[i], false);
        }
    }
}
