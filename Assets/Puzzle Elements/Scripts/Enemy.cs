using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public GameObject WorldController;
    private WorldSwitchScript wrdSftScr;

    public PlayerBehavior pb;
    public GameObject spritePickup;
    private Animator animator;

    private bool pickUpUsed = false;

    void Start()
    {
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();

        pb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        //spriteCanvas = Fi
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!pickUpUsed) {
            if (wrdSftScr.worldIsReal())
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            else
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        }
    }


    private void OnMouseUp() {
        if (pb.hasPickUp && Vector3.Distance(pb.gameObject.transform.position, transform.position) < 4.5f) {

            pb.animator.Play("Player_Throw");

            animator.SetTrigger("pickUpUsed");

            pickUpUsed = true;

            StartCoroutine(ChangeAlpha(GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0.9f), 1f));

            GetComponent<Movement>().enabled = false;
        }
    }
    IEnumerator ChangeAlpha(Color start, Color end, float duration) {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime) {
            yield return null;
        }
        GetComponent<Rigidbody2D>().simulated = false;
        //GetComponent<Rigidbody2D>().constraints = new RigidbodyConstraints2D();
        //GetComponent<Rigidbody2D>().freezeRotation = true;
        //GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
        GetComponents<BoxCollider2D>()[1].isTrigger = true;
        if (spritePickup != null)
            spritePickup.GetComponent<Image>().enabled = false;
        pb.hasPickUp = false;
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = end;
        pb.isThrowing = false;
    }
}
