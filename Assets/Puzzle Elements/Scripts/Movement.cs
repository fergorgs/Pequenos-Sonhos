using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public enum Type { Still, Moving };

    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    public Type type;

    public float finalX;
    private Vector3 finalPos;
    private Vector3 iniPos;

    public float moveSpeed = 3;
    
    private Vector3 leftBound;
    private Vector3 rightBound;

    private Vector3 curPos;
    //private LineRenderer lird;
    
    private bool goingLeft = true;
    private bool playerInRange = false;
    private bool coolDown = false;

    private GameObject player;

    private Collider2D[] colliders;
    private SpriteRenderer sprd;

    // Use this for initialization
    void Start()
    {
        if (wrdControl == null)
        {
            wrdControl = GameObject.FindGameObjectWithTag("WCS");
            wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();
        }

        player = GameObject.FindGameObjectWithTag("Player");

        iniPos = transform.position;
        finalPos = transform.position;
        finalPos.x = finalX;

        if (iniPos.x < finalPos.x)
        {
            leftBound = iniPos;
            rightBound = finalPos;
        }
        else
        {
            leftBound = finalPos;
            rightBound = iniPos;
        }

        colliders = GetComponents<Collider2D>();

        sprd = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;

        if (wrdSwitch.worldIsReal())
        {
            for (int i = 0; i < colliders.Length; i++)
                Physics2D.IgnoreCollision(colliders[i], player.GetComponent<Collider2D>(), false);

        }
        else
        {
            playerInRange = false;
            for (int i = 0; i < colliders.Length; i++)
                Physics2D.IgnoreCollision(colliders[i], player.GetComponent<Collider2D>(), true);
        }



        if (type == Type.Moving)
        {
            if (goingLeft)
            {
                sprd.flipX = false;
                if (!(playerInRange && player.transform.position.x < transform.position.x))
                {
                    if (transform.position.x > leftBound.x)
                        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    else
                        goingLeft = false;
                }
            }
            else
            {
                sprd.flipX = true;
                if (!(playerInRange && player.transform.position.x > transform.position.x))
                {
                    if (transform.position.x < rightBound.x)
                        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                    else
                        goingLeft = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("name: " + collision.name);
        if (collision.tag != "Player" && collision.tag != "Plataforma" && collision.tag != "PickUp")
        {
            //Debug.Log("Enter: " + collision.name);
            if (!coolDown)
            {
                goingLeft = !goingLeft;
                coolDown = true;
                StartCoroutine(CoolDownReset());
            }

        }
        else if(collision.tag == "Player")
        {
            if (wrdSwitch.worldIsReal())
                playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (wrdSwitch.worldIsReal())
                playerInRange = false;
        }
    }

    private IEnumerator CoolDownReset()
    {
        yield return new WaitForSeconds(0.2f);
        coolDown = false;
    }
}

