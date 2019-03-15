using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambiarraEnemy : MonoBehaviour {
    
    private BoxCollider2D bc2d;

    public float tol;

    private GameObject parent;
    public GameObject enemy;

	// Use this for initialization
	void Start () {

        bc2d = GetComponent<BoxCollider2D>();

        parent = transform.parent.gameObject;

    }

    private bool suitablePos()
    {
        if (enemy.transform.position.x < parent.transform.position.x + tol
            && enemy.transform.position.x > parent.transform.position.x - tol)
        {
            Debug.Log("Returning true: " + (parent.transform.position.x - tol) + " < " + enemy.transform.position.x + " < " + (parent.transform.position.x + tol));
            return true;
        }
        else
            return false;
    }


    // Update is called once per frame
            void Update () {

        /*if (enemy.transform.parent == null)
            Debug.Log("True, parent null");
        else
            Debug.Log("False, parent's name = " + enemy.transform.parent.name);*/

        if (enemy.transform.parent == null)
            bc2d.enabled = false;
        else if (enemy.transform.parent.tag == "Plataforma" && suitablePos())
            bc2d.enabled = true;
	}
}
