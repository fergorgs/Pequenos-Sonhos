using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manchar : MonoBehaviour {

    public Sprite SpriteManchado;

	// Use this for initialization
	void Start () {

        int chance = (int) Random.Range(1, 4);

        if(chance == 1)
            GetComponent<SpriteRenderer>().sprite = SpriteManchado;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
