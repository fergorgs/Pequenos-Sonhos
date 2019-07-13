﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{

    public enum States { Pulando, Andando, Parado, Empurrando, Pickup };
    public enum Sides { Direita, Esquerda };

    public States playerState;
    public States get_playerState() { return playerState; }
    public void set_playerState(States state) { playerState = state; }

    private Sides facing;
    public Sides get_facing() { return facing; }
    public void set_facing(Sides side) { facing = side; }

    private bool onGround = true;
    private bool empurrando = false;
    private bool sobrePrancha = false;
    private bool jumpBuffer = false;
    private bool ghostJump = false;
    public bool hasPickUp = false;
    public bool isThrowing=false;
    private float xVel;
    private float yVel;
    public float maxVel = 5;
    public float aceleracao = 2;
    public float pulo = 5;
    public float drag = 1.5f;
    public float dragInAir = 1f;
    public float jumpBufferTime = 0.5f;
    public float ghostJumpTime = 0.5f;
    Transform trfm;
    public Rigidbody2D rb2d;
    RaycastHit2D hit;
    public Animator animator;
    public AudioSource grassSteps, goingUp, landing;
    public AudioSource[] jump;
    private bool jumped = false, falling = false;
    private Vector3 cpPos;
    public float tol = 0.2f;

	private float startJumpTime;
	public float jumpTol = 0.35f;

    // Use this for initialization
    void Start()
    {
        playerState = States.Parado;
        facing = Sides.Direita;
        animator = GetComponent<Animator>();
        trfm = GetComponent<Transform>();
        
        rb2d = GetComponent<Rigidbody2D>();
        cpPos = GameObject.FindWithTag("Checkpoint").transform.position;
        if (PlayerPrefs.GetInt("isAfterCP") == 1)
            transform.position = cpPos;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Estado = " + playerState);
        xVel = rb2d.velocity.x;
        yVel = rb2d.velocity.y;
        if (yVel < -0.3f )  
            animator.SetBool("Falling", true);                 
        else if(yVel >= -0.3f)
            animator.SetBool("Falling", false);
        animator.SetInteger("State", (int)playerState);
        animator.SetFloat("YVel", yVel);
        animator.SetFloat("XVel",Mathf.Abs(xVel));
        //animator.SetBool("Trow", isThrowing);
        
        if (yVel >= 1 && !jumped)
            falling = true;

        if (xVel < -0.5f && !GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (xVel > 0.5f && GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

		switch (playerState)
		{
			//PARADO-----------------------------------------------------------------------------------------------------
			case States.Parado:
				if (grassSteps.isPlaying)
					grassSteps.Pause();
				//COMPORTAMENTO

				rb2d.velocity = new Vector2(0, yVel);

				if (SimpleInput.GetAxis("Vertical") > 0f /*|| jumpBuffer == true*/ || Input.GetKey(KeyCode.UpArrow))
				{
					rb2d.velocity = new Vector2(0, (float)pulo);
					onGround = false;
				}

				//MUDANÇA DE ESTADO
				if (onGround == false)
				{
					playerState = States.Pulando;
					startJumpTime = Time.time;
				}
				//else if(yVel(
				else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || SimpleInput.GetAxis("Horizontal") < 0f || SimpleInput.GetAxis("Horizontal") > 0f)
					playerState = States.Andando;
				else if (Input.GetKeyDown(KeyCode.P))
					playerState = States.Pickup;

				break;

			//ANDANDO-----------------------------------------------------------------------------------------------------
			case States.Andando:
				if (!grassSteps.isPlaying)
					grassSteps.UnPause();
				//COMPORTAMENTO
				if (SimpleInput.GetAxis("Horizontal") < 0f || Input.GetKey(KeyCode.LeftArrow))
				{
					if (rb2d.velocity.x > -maxVel)
						rb2d.velocity = new Vector2(xVel - (aceleracao * Time.deltaTime), yVel);
					if (rb2d.velocity.x < 0) ;
					facing = Sides.Esquerda;
					//animação = anadando esquerda
				}
				else if (SimpleInput.GetAxis("Horizontal") > 0f || Input.GetKey(KeyCode.RightArrow))
				{
					//Debug.Log("Entrada");
					if (rb2d.velocity.x < maxVel)
						rb2d.velocity = new Vector2(xVel + aceleracao * Time.deltaTime, yVel);
					if (rb2d.velocity.x > 0)
						facing = Sides.Direita;
					//animação = andando direita
				}
				else
				{
					rb2d.velocity = new Vector2((float)xVel / drag, yVel);
					if (xVel > 0.5f && xVel < 0.5f)
						xVel = 0;
				}
				if (Input.GetKey(KeyCode.UpArrow) || SimpleInput.GetAxis("Vertical") > 0f)
				{
					rb2d.velocity = new Vector2(xVel, (float)pulo);
					onGround = false;
				}

				//MUDANÇA DE ESTADO
				if (onGround == false && (yVel < -tol || yVel > tol))
				{                //++++++++++
								 //Debug.Log("Indo para pulando, yVel = " + yVel);
					playerState = States.Pulando;
					startJumpTime = Time.time;
				}
				else if (rb2d.velocity.x == 0)
				{
					//Debug.Log("Aqui");//<= 0.2f && rb2d.velocity.x >= -0.2f)
					playerState = States.Parado;
				}
				else if (Input.GetKeyDown(KeyCode.P))
					playerState = States.Pickup;
				else if (empurrando == true /*&& (xVel < -tol || xVel > tol)*/)
					playerState = States.Empurrando;

				break;

			//PULANDO-----------------------------------------------------------------------------------------------------
			case States.Pulando:
				if (grassSteps.isPlaying)
					grassSteps.Pause();
				//COMPORTAMENTO
				//animação = pulando

				/*if(Time.time - startJumpTime < jumpTol)
				{
					if (Input.GetKey(KeyCode.UpArrow) || SimpleInput.GetAxis("Vertical") > 0f)
					{
						rb2d.velocity = new Vector2(xVel, (float)pulo);
						yVel = (float)pulo;
						Debug.Log("Haaay");
					}
					Debug.Log("Sup");
				}*/
					


				if (SimpleInput.GetAxis("Horizontal") < 0f || Input.GetKey(KeyCode.LeftArrow))
				{
					if (rb2d.velocity.x > -maxVel)
						rb2d.velocity = new Vector2(xVel - aceleracao * Time.deltaTime, yVel);
					if (rb2d.velocity.x < 0)
						facing = Sides.Esquerda;
					//animação = anadando esquerda
				}
				else if (SimpleInput.GetAxis("Horizontal") > 0f || Input.GetKey(KeyCode.RightArrow))
				{
					if (rb2d.velocity.x < maxVel)
						rb2d.velocity = new Vector2(xVel + aceleracao * Time.deltaTime, yVel);
					if (rb2d.velocity.x > 0)
						facing = Sides.Direita;
					//animação = andando direita
				}
				else
				{
					rb2d.velocity = new Vector2((float)xVel / dragInAir, yVel);
					if (xVel > 0.5f && xVel < 0.5f)
						xVel = 0;
				}
				if (Input.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetAxis("Vertical") > 0f)
				{
					if (!jump[0].isPlaying && !jump[1].isPlaying && !jump[2].isPlaying && !jumped && !goingUp.isPlaying) {
						jump[(int)Random.Range(0, 3)].Play();
						goingUp.Play();
						jumped = true;
					}


					if (ghostJump == true)
					{
						rb2d.velocity = new Vector2(xVel, (float)pulo);

						ghostJump = false;
					}
					else
					{
						jumpBuffer = true;
						StartCoroutine(jumpBufferReset());
					}
				}



				//MUDANÇA DE ESTADO
				if (onGround == true)
				{
					if (jumped || falling)
						landing.Play();
					falling = false;
					jumped = false;
					if (rb2d.velocity.x != 0 && !sobrePrancha)
					{
						//Debug.Log("Entrou (sobrePrancha = " + sobrePrancha + ")");
						playerState = States.Andando;
					}
					else
						playerState = States.Parado;
				}

				break;

			//PICKUP-----------------------------------------------------------------------------------------------------
			case States.Pickup:

				//COMPORTAMENTO
				rb2d.velocity = Vector2.zero;
				//animação = usando pick up

				//MUDANÇA DE ESTADO
				//if (animação.ended)
				playerState = States.Parado;

				break;

			//EMPURRANDO-----------------------------------------------------------------------------------------------------
			case States.Empurrando:

				//COMPORTAMENTO
				if (SimpleInput.GetAxis("Horizontal") < 0f || Input.GetKey(KeyCode.LeftArrow))
				{
					rb2d.velocity = new Vector2(-maxVel, yVel);
				}
				else if (SimpleInput.GetAxis("Horizontal") > 0f || Input.GetKey(KeyCode.RightArrow))
				{
					rb2d.velocity = new Vector2(maxVel, yVel);
				}
				if (Input.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetAxis("Vertical") > 0f)
				{
					rb2d.velocity = new Vector2(0, (float)pulo);
					onGround = false;
				}

				//MUDANÇA DE ESTADO
				if (onGround == false && (yVel < -tol || yVel > tol)) { 
					playerState = States.Pulando;
					startJumpTime = Time.time;
				}
                else if (empurrando == false || !GettingInput()/*(xVel > -tol && xVel < tol)*/)
                    playerState = States.Parado;
                else if (Input.GetKeyDown(KeyCode.P))
                    playerState = States.Pickup;


                break;
                
                
        }
        
       //Debug.Log("Player.State: " + playerState);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("Collision");
        //Debug.Log("yVel = " + yVel);
        if (rb2d.velocity.y == 0 && col.gameObject.tag != "Parede")
            onGround = true;
        else if (col.gameObject.tag == "Prancha")
        {
            onGround = true;
            sobrePrancha = true;
        }
            

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (/*transform.parent.tag == "Caixa Empurravel"*/col.gameObject.tag == "Caixa Empurravel")
        {
            empurrando = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (rb2d.velocity.y <= 0 && playerState == States.Andando)
        {
            ghostJump = true;
            StartCoroutine(ghostJumpReset());
        }

        //Debug.Log("Chamado, yVel = " + yVel);

        //if(rb2d.a
        if (rb2d.velocity.y != 0)
            onGround = false;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Caixa Empurravel")
        {
            empurrando = false;
        }
        //Debug.Log("Chamado");
    }

    private IEnumerator jumpBufferReset()
    {
        yield return new WaitForSeconds(jumpBufferTime);
        jumpBuffer = false;
    }

    private IEnumerator ghostJumpReset()
    {
        yield return new WaitForSeconds(ghostJumpTime);
        ghostJump = false;
    }

    private bool GettingInput(){
        if (Input.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetAxis("Vertical") > 0f)
            return true;
        else if (SimpleInput.GetAxis("Horizontal") > 0f || Input.GetKey(KeyCode.RightArrow))
            return true;
        else if (SimpleInput.GetAxis("Horizontal") < 0f || Input.GetKey(KeyCode.LeftArrow))
            return true;
        else
            return false;
    }

}