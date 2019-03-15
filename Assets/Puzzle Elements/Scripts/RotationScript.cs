using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

    public float maxAngle = 30;

    public float coeficRestituicao = 20f;

    private Rigidbody2D rb2d;

    private bool plrOn = false;

    private Vector3 startPos;

	// Use this for initialization
	void Start () {

        rb2d = GetComponent<Rigidbody2D>();

        startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = startPos;
        //Debug.Log("plrOn = " + plrOn);
        if (!plrOn)
        {
            if (transform.eulerAngles.z > 359 || transform.eulerAngles.z < 1)
            {
                rb2d.angularVelocity = 0;
                transform.eulerAngles = Vector3.zero;
            }
            else if (transform.eulerAngles.z > 180)
                rb2d.angularVelocity = coeficRestituicao;

            else
                rb2d.angularVelocity = -coeficRestituicao;
        }
        else
        {
            if (transform.eulerAngles.z > maxAngle && transform.eulerAngles.z < 180)
                transform.eulerAngles = new Vector3(0, 0, maxAngle);

            else if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < 360 - maxAngle)
                transform.eulerAngles = new Vector3(0, 0, -maxAngle);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Caixa Empurravel")
        {
            Debug.Log("Entra");
            plrOn = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Caixa Empurravel")
        {
            Debug.Log("Sai");
            plrOn = false;
        }
    }
}
