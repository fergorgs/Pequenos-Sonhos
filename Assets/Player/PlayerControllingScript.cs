using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllingScript : MonoBehaviour
{
	//Animations Stuff-----------------------------------
	public enum AnimStates { Idle, Walking, Jumping, Falling, Pushing, Throwing }
	private AnimStates animState = AnimStates.Idle;
	private Animator anim;

	//Public Variables------------------------------------
	public float maxVelocity = 5, fowardForce = 20, jumpSpeed = 11.3f, grnDrag = 10, airDrag = 7;

	//Jump and State Variables----------------------------
	public float ghostJumpTime = 0.5f, jumpBufferTime = 0.5f;
	private bool canGhostJump = true, canGhostBuffer = false;

	private bool grounded = true, pushing = false, usingPickUp = false;
	public bool IsUsingPickUp() { return usingPickUp; }
	public void SetUsingPickUp(bool val) { usingPickUp = val; }

	//Camponents------------------------------------------
	private Rigidbody2D rb2d;
	private RaycastHit2D[] grnHitLeft, grnHitRight, handsHit;

	//Sound Effects---------------------------------------
	public AudioSource passos, landing;
	public AudioSource[] jumping;

	private bool foundTile = true, groundedBuffer = true, usingPickupBuffer = false;

	//Others----------------------------------------------
	public LayerMask raycastLayer;

	private bool pickUp = false;
	public bool HasPickUp() { return pickUp; }
	public void SetPickUp(bool val) { pickUp = val; }

    // Start is called before the first frame update
    void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		float xVel = rb2d.velocity.x;
		float yVel = rb2d.velocity.y;

		//-----------------------------------------------------------------------------------------
		//--------------------------------HORIZONTAL MOVEMENT--------------------------------------
		//-----------------------------------------------------------------------------------------
		if (!usingPickUp)
		{
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
			else                                                                                            //Apply drag
			{
				if (grounded)
					rb2d.AddForce(new Vector2(-grnDrag * rb2d.velocity.x, 0));
				else
					rb2d.AddForce(new Vector2(-airDrag * rb2d.velocity.x, 0));
			}
		}

		//-----------------------------------------------------------------------------------------
		//----------------------------------------JUMP---------------------------------------------
		//-----------------------------------------------------------------------------------------
		if (!usingPickUp)
		{
			if (grounded || canGhostJump)
			{
				if (SimpleInput.GetAxis("Vertical") > 0f || Input.GetKey(KeyCode.UpArrow))
				{
					canGhostJump = false;                                                                   //logicamente desnecessario
					rb2d.velocity = new Vector2(xVel, jumpSpeed);
					//rb2d.AddForce(new Vector2(0, jumpForce));
				}
			}
		}

		//-----------------------------------------------------------------------------------------
		//--------------------------------------RAYCAST--------------------------------------------
		//-----------------------------------------------------------------------------------------
		grnHitLeft = Physics2D.RaycastAll(transform.position + new Vector3(-0.17f, 0, 0), Vector2.down, 1.51f, raycastLayer);		//1 is the "default" layer mask
		grnHitRight = Physics2D.RaycastAll(transform.position + new Vector3(0.17f, 0, 0), Vector2.down, 1.51f, raycastLayer);
		if(rb2d.velocity.x > 0)
			handsHit = Physics2D.RaycastAll(transform.position, Vector2.right, 0.63f);
		else
			handsHit = Physics2D.RaycastAll(transform.position, Vector2.left, 0.63f);

		bool foundGround = false, foundBox = false;
		foundTile = false;

		if (grnHitLeft.Length > 0)																				//Left Foot Raycast
		{
			for (int i = 0; i < grnHitLeft.Length; i++)
			{
				if (!grnHitLeft[i].collider.isTrigger)
				{
					foundGround = true;
					if (grnHitLeft[i].collider.gameObject.CompareTag("Tile"))
						foundTile = true;

					break;
				}
			}
		}
		if (grnHitRight.Length > 0)																				//Right Foot Raycast
		{
			for (int i = 0; i < grnHitRight.Length; i++)
			{
				if (!grnHitRight[i].collider.isTrigger)
				{
					foundGround = true;
					if (grnHitRight[i].collider.gameObject.CompareTag("Tile"))
						foundTile = true;

					break;
				}
			}
		}

		grounded = foundGround;

		//------------------------------------CALLS GHOST JUMP
		if (grounded)
			canGhostJump = true;
		else if (canGhostJump)
			StartCoroutine(ResetGhostJump());


		if(handsHit.Length > 0)																					//Hands Raycast
		{
			for(int i = 0; i < handsHit.Length; i++)
			{
				if(handsHit[i].collider.gameObject.tag == "Caixa Empurravel")
				{
					foundBox = true;
					break;
				}
			}
		}

		pushing = foundBox;

		//-----------------------------------------------------------------------------------------
		//--------------------------------------ANIMATOR-------------------------------------------
		//-----------------------------------------------------------------------------------------

		if (grounded)
		{
			if (Mathf.Abs(rb2d.velocity.x) > 0.2f)
			{
				if (pushing)
					animState = AnimStates.Pushing;
				else
					animState = AnimStates.Walking;
			}
			else
				animState = AnimStates.Idle;
		}
		else
		{
			if (rb2d.velocity.y > 0)
				animState = AnimStates.Jumping;
			else
				animState = AnimStates.Falling;
		}

		anim.SetInteger("State", (int)animState);

		if (rb2d.velocity.x > 0.2f)
			GetComponent<SpriteRenderer>().flipX = false;
		else if (rb2d.velocity.x < -0.2f)
			GetComponent<SpriteRenderer>().flipX = true;


		//-----------------------------------------------------------------------------------------
		//------------------------------------SOUND EFFECTS----------------------------------------
		//-----------------------------------------------------------------------------------------

		if (!usingPickUp)
		{
			//Passos
			//if ((SimpleInput.GetAxis("Horizontal") != 0f || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (grounded && foundTile))
			if (animState == AnimStates.Walking && Time.timeScale > 0)
			{
				if (!passos.isPlaying)
					passos.Play();
			}
			else
				passos.Pause();

			//Pulo
			if ((grounded || canGhostJump) && (SimpleInput.GetAxis("Vertical") > 0f || Input.GetKey(KeyCode.UpArrow)))
				jumping[(int)Random.Range(0, 3)].Play();

			//Landing
			if (grounded != groundedBuffer && grounded)
				landing.Play();
			groundedBuffer = grounded;
		}
		else if (usingPickupBuffer != usingPickUp && usingPickUp)
		{
			jumping[(int)Random.Range(0, 3)].Play();
			usingPickupBuffer = usingPickUp;
		}
	}










	//-----------------------------------------------------------------------------------------
	//----------------------------------GHOST JUMP RESET---------------------------------------
	//-----------------------------------------------------------------------------------------
	private IEnumerator ResetGhostJump()
	{
		yield return new WaitForSeconds(ghostJumpTime);

		canGhostJump = false;
	}


}
