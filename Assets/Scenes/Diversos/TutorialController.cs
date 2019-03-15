using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    public GameObject player;

    private int onOffset = 2;
    private bool ativavel = false;
    private bool firstCalled = false;

    public GameObject switchButton;
    public GameObject Seta;

    public GameObject WorldController;
    private WorldSwitchScript wrdSftScr;

    public GameObject keyText;
    public GameObject texts1;
    public GameObject texts2;
    public GameObject trigger2, trigger3, trigger4;
    public GameObject[] puzzleElem;
    public Camera cam;
    public float smoothDelay = 4f;
    public float delayReset = 5f;

    private IEnumerator MoveCamera(Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            cam.transform.position = Vector3.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }

    }

    // Use this for initialization
    void Start ()
    {
        
        if (WorldController == null)
            WorldController = GameObject.FindGameObjectWithTag("WCS");
        wrdSftScr = WorldController.GetComponent<WorldSwitchScript>();

        Seta.SetActive(false);
        switchButton.SetActive(false);
        texts1.SetActive(true);
        //texts2.SetActive(false); Debug.Log("Caled false");
        for (int i = 0; i < puzzleElem.Length; i++)
            puzzleElem[i].GetComponent<ShiftBehavior>().isShiftable = false;

        StartCoroutine(OnOffset());
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("Teste");
        if (wrdSftScr.worldHasShifted() && ativavel)
        {
            ativavel = false;
            changeTutorialStage();
        }
    }


    void OnTriggerEnter2D(Collider2D collision){

        if (!firstCalled)
        {
            Seta.SetActive(true);
            switchButton.SetActive(true);
            texts2.SetActive(true);
            trigger2.SetActive(true);
            trigger3.SetActive(true);
            trigger4.SetActive(true);
            keyText.SetActive(true);
            Time.timeScale = 0;
            firstCalled = true;
        }
    }

    public void changeTutorialStage()
    {
        //Debug.Log("Chamado");
        float aux = cam.GetComponent<SmoothCameraScript>().dampTime;

        cam.GetComponent<SmoothCameraScript>().setNivel0(true, gameObject);
        cam.GetComponent<SmoothCameraScript>().enabled = false;
        StartCoroutine(MoveCamera(cam.transform.position, player.transform.position + new Vector3(0, 0, -10), smoothDelay));
        //cam.GetComponent<SmoothCameraScript>().dampTime = smoothDelay;
       
        texts1.SetActive(false);
        //texts2.SetActive(true); Debug.Log("Caled true");
        for (int i = 0; i < puzzleElem.Length; i++)
            puzzleElem[i].GetComponent<ShiftBehavior>().isShiftable = true;

        Seta.SetActive(false);
        keyText.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(ResetCamDelay(aux));
    }

    private IEnumerator ResetCamDelay(float aux)
    {
        yield return new WaitForSeconds(delayReset);
        
        cam.GetComponent<SmoothCameraScript>().setNivel0(false, gameObject);
        cam.GetComponent<SmoothCameraScript>().enabled = true;
    }

    private IEnumerator OnOffset()
    {
        yield return new WaitForSeconds(onOffset);

        ativavel = true;
    }

}
