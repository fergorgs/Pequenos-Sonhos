using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambiarraEnemy2 : MonoBehaviour
{
    public GameObject WorldSwitchController;
    private WorldSwitchScript wrdSwtScr;

    public GameObject plataform;
    private Vector3 plfPos;
    private ShiftBehavior sftBhv;

    private BoxCollider2D bc2d;

    public GameObject enemy1, enemy2;
    private Collider2D[] colsEnemy1, colsEnemy2;

    public float upperBound;

    // Use this for initialization
    void Start()
    {
        WorldSwitchController = GameObject.FindGameObjectWithTag("WCS");
        wrdSwtScr = WorldSwitchController.GetComponent<WorldSwitchScript>();

        sftBhv = plataform.GetComponent<ShiftBehavior>();
        bc2d = GetComponent<BoxCollider2D>();

        colsEnemy1 = enemy1.GetComponents<Collider2D>();
        colsEnemy2 = enemy2.GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        plfPos = plataform.transform.position;

        if (sftBhv.GetIsReal() != wrdSwtScr.worldIsReal() || plfPos.y == upperBound)
            bc2d.enabled = false;
        else
        {
            bc2d.enabled = true;

            if (enemy1.transform.parent == plataform)
            {
                for (int i = 0; i < colsEnemy1.Length; i++)
                    Physics2D.IgnoreCollision(bc2d, colsEnemy1[i], false);
            }
            else
            {
                for (int i = 0; i < colsEnemy1.Length; i++)
                    Physics2D.IgnoreCollision(bc2d, colsEnemy1[i], true);
            }

            if (enemy2.transform.parent == plataform)
            {
                for (int i = 0; i < colsEnemy2.Length; i++)
                    Physics2D.IgnoreCollision(bc2d, colsEnemy2[i], false);
            }
            else
            {
                for (int i = 0; i < colsEnemy2.Length; i++)
                    Physics2D.IgnoreCollision(bc2d, colsEnemy2[i], true);
            }
        }
    }
}