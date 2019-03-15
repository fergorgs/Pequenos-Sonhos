using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBird : MonoBehaviour {

    public GameObject passaro;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Enter");
        passaro.GetComponent<SpriteRenderer>().flipX = !passaro.GetComponent<SpriteRenderer>().flipX;
    }
}
