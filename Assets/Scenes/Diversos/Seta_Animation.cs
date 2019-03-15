using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta_Animation : MonoBehaviour {
    private SpriteRenderer sprd;

    private Vector3 startPos;
    public float finalX;

    public float speed = 0.1f;

    public enum Direction { Right, Left };
    public Direction dir = Direction.Right;

    private float movLength;

	// Use this for initialization
	void Start () {

        startPos = transform.position;
     
        movLength = Mathf.Abs(finalX - startPos.x);

        sprd = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        if (dir == Direction.Right)
        {
            transform.position += new Vector3(speed, 0, 0);

            if (transform.position.x >= finalX)
            {
            //Debug.Log("Entra");
                sprd.color = new Color(1, 1, 1, 1);
                transform.position = startPos;
            }

            if (transform.position.x > startPos.x + movLength / 2)
                sprd.color -= new Color(0, 0, 0, speed / (movLength / 2));
        }
        else
        {
            transform.position -= new Vector3(speed, 0, 0);

            if (transform.position.x <= finalX)
            {
                sprd.color = new Color(1, 1, 1, 1);
                transform.position = startPos;
            }

            if (transform.position.x < startPos.x - movLength / 2)
                sprd.color -= new Color(0, 0, 0, speed / (movLength / 2));
        }

    }
}
