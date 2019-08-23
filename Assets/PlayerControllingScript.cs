using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllingScript : MonoBehaviour
{
	public float maxVelocity = 5, fowardForce = 20, jumpForce = 200, grnDrag = 10, airDrag = 7;

	private bool grounded = true;

	private Rigidbody2D rb2d;
	private RaycastHit2D[] grnHit;

    // Start is called before the first frame update
    void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		//HORIZONTAL MOVEMENT----------------------------------------------------------------------
		if (SimpleInput.GetAxis("Horizontal") < 0f || Input.GetKey(KeyCode.LeftArrow))                  //Walk Left
		{
			if (rb2d.velocity.x > -maxVelocity)
				rb2d.AddForce(new Vector2(-fowardForce, 0));
		}
		else if (SimpleInput.GetAxis("Horizontal") > 0f || Input.GetKey(KeyCode.RightArrow))            //Walk Right
		{
			if (rb2d.velocity.x < maxVelocity)
				rb2d.AddForce(new Vector2(fowardForce, 0));
		}
		else
		{
			if(grounded)
				rb2d.AddForce(new Vector2(-grnDrag * rb2d.velocity.x, 0));
			else
				rb2d.AddForce(new Vector2(-airDrag * rb2d.velocity.x, 0));
		}


		//JUMP-------------------------------------------------------------------------------------
		if (grounded)
		{
			//Debug.Log("Gounded")
			if (SimpleInput.GetAxis("Vertical") > 0f || Input.GetKey(KeyCode.UpArrow))
			{
				rb2d.AddForce(new Vector2(0, jumpForce));
			}
		}


		//RAYCAST----------------------------------------------------------------------------------
		grnHit = Physics2D.RaycastAll(transform.position, Vector2.down, 1.51f);

		bool foundGround = false;

		if (grnHit.Length > 0)
		{
			for (int i = 0; i < grnHit.Length; i++)
			{
				if (!grnHit[i].collider.isTrigger)
				{
					foundGround = true;
					break;
				}
			}
		}

		grounded = foundGround;


		//Debug.Log("hit name: " + grnHit.collider.gameObject.name);
		//grounded = true;
	}
}
