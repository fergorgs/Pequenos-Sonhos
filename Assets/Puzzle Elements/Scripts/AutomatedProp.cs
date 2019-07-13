using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedProp : MonoBehaviour {

    public enum Type { Loop, LoopOnce, SingleMovement, NoButton };
    
    private Vector2 startPosition;
    public Vector2 finalPosition;
    private Vector2 currentPos;
    private Vector3 buttonPos;

    public Type type;
    
    public GameObject WorldController;
	private GameObject mainCamera;
	private WorldSwitchScript wrdSftScr;
    private ShiftBehavior sftBeh;
    private LineRenderer lird;
    public GameObject[] buttons;
    public BoxCollider2D bxcol;
    private bool activated = false;
    private bool subroutineCalled = false;
    private bool btnsActive;
    public float timeMov;
    public float activationDelay = 0.2f;
    public float timeToKillLine = 2f;

	private float startTime;

    private void Start()
    {
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();
        sftBeh = GetComponent<ShiftBehavior>();

        if(buttons[0] != null)
            buttonPos = buttons[0].transform.position;
        lird = GetComponent<LineRenderer>();
        lird.enabled = false;
        lird.SetPosition(0, transform.position);
        lird.SetPosition(1, buttonPos);

        startPosition = transform.position;

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

	}

    public IEnumerator MoveProp(Vector2 pos) {

        var t = 0f;
        var currentPosition = transform.position;
        while (t < 1) {
            t += Time.deltaTime / timeMov;
            transform.position = Vector2.Lerp(currentPosition, pos, t);
            yield return null;
        }
    }

    private bool AllBtnsActive() {

        int btnsActive = 0;

        for (int i = 0; i < buttons.Length; i++) {
            if (buttons[i].GetComponent<ButtonBehavior>().IsActive())
                btnsActive++;
        }

        if (btnsActive == buttons.Length)
            return true;
        else
            return false;
    }

    private void Update() {
        currentPos = transform.position;

        if(lird!=null)
            lird.SetPosition(0, currentPos);

        if (AllBtnsActive())
        {
            if (!subroutineCalled)
                StartCoroutine(SetBtnsAsActive());
        }
        else
            btnsActive = false;

        if(wrdSftScr.worldHasShifted() && wrdSftScr.worldIsReal() != sftBeh.GetIsReal())
            lird.enabled = false;

        switch (type)
        {

            case Type.NoButton:

                if (currentPos == startPosition)
                {
                    //Debug.Log("Entrou111");
                    StartCoroutine(MoveProp(finalPosition));
                }
                else if (currentPos == finalPosition)
                    StartCoroutine(MoveProp(startPosition));
                break;

            case Type.Loop:

                if (activated)
                {
                    if (currentPos == startPosition)
                        StartCoroutine(MoveProp(finalPosition));
                    else if (currentPos == finalPosition)
                        StartCoroutine(MoveProp(startPosition));
                }
                else if (btnsActive /*&& wrdSftScr.worldIsReal() == sftBeh.GetIsReal()*/)
                {
                    activated = true;
                    lird.enabled = true;
					StartCoroutine(ChangeCameraFocus());
					StartCoroutine(TurnOffButtonLine());
                }
                break;

            case Type.LoopOnce:

                if (currentPos == startPosition && btnsActive /*&& wrdSftScr.worldIsReal() == sftBeh.GetIsReal()*/)
                {
                    StartCoroutine(MoveProp(finalPosition));
                    lird.enabled = true;
					StartCoroutine(ChangeCameraFocus());
					StartCoroutine(TurnOffButtonLine());
                }
                else if (currentPos == finalPosition)
                    StartCoroutine(MoveProp(startPosition));
                break;

            case Type.SingleMovement:

                if (btnsActive /*&& wrdSftScr.worldIsReal() == sftBeh.GetIsReal()*/)
                {
                    lird.enabled = true;
					StartCoroutine(ChangeCameraFocus());
					StartCoroutine(TurnOffButtonLine());
                    if (currentPos == startPosition)
                        StartCoroutine(MoveProp(finalPosition));
                    else if (currentPos == finalPosition)
                        StartCoroutine(MoveProp(startPosition));
                }
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        Collider2D[] cols;
        cols = collision.GetComponents<Collider2D>();
        if (collision.CompareTag("Player") || collision.CompareTag("Caixa Empurravel") || collision.CompareTag("Inimigo"))
        {
            if (collision.gameObject.transform.position.y < transform.position.y + (collision.GetComponent<HeightProperty>().GetHeight() / 2))
            {
                for(int i = 0; i < cols.Length; i++)
                    Physics2D.IgnoreCollision(bxcol, cols[i], true);
            }
            else
                collision.GetComponent<Collider2D>().transform.SetParent(transform);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Collider2D[] cols;
        cols = collision.GetComponents<Collider2D>();
        if (collision.CompareTag("Player") || collision.CompareTag("Caixa Empurravel") || collision.CompareTag("Inimigo"))
        {
            if (collision.gameObject.transform.position.y >= transform.position.y + (collision.GetComponent<HeightProperty>().GetHeight() / 2))
            {
                for (int i = 0; i < cols.Length; i++)
                    Physics2D.IgnoreCollision(bxcol, cols[i], false);
                //Debug.Log("OnTSt / Entra. Col name: " + collision.name +
                //          "\nCol pos: " + collision.transform.position);
                collision.GetComponent<Collider2D>().transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") || collision.CompareTag("Caixa Empurravel") || collision.CompareTag("Inimigo"))
        {
            collision.GetComponent<Collider2D>().transform.SetParent(null);
        }
    }

    private IEnumerator SetBtnsAsActive()
    {
        yield return new WaitForSeconds(activationDelay);

        if(AllBtnsActive())
            btnsActive = true;
    }

    private IEnumerator TurnOffButtonLine()
    {
        yield return new WaitForSeconds(timeToKillLine);

        lird.enabled = false;
        //Debug.Log("Out");
    }

	private IEnumerator ChangeCameraFocus()
	{
		mainCamera.GetComponent<SmoothCameraScript>().target = gameObject.transform;
		startTime = Time.time;
		while (Time.time - startTime < 0.5f)
			yield return null;
		mainCamera.GetComponent<SmoothCameraScript>().enabled = false;

		while (lird.enabled)
			yield return null;

		mainCamera.GetComponent<SmoothCameraScript>().enabled = true;
		mainCamera.GetComponent<SmoothCameraScript>().target = mainCamera.GetComponent<SmoothCameraScript>().GetDefaultTarget();
	}
}
