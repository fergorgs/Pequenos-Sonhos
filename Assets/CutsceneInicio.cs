using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneInicio : MonoBehaviour {

    public GameObject blackWall, Text, cam, pauseCont;

    public Vector3 camStart, camEnd;

    public float camSpeed;

    public GameObject[] uiButtons;
    public GameObject shiftButton;

    private void Awake()
    {
        blackWall.SetActive(true);
    }

    // Use this for initialization
    void Start () {

        cam.GetComponent<SmoothCameraScript>().enabled = false;
        cam.transform.position = camStart;
        for (int i = 0; i < uiButtons.Length; i++)
            uiButtons[i].SetActive(false);
        shiftButton.SetActive(false);

        pauseCont.GetComponent<PauseScript>().SetPause(true);

        StartCoroutine(Cutscene());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Cutscene()
    {
        StartCoroutine(ChangeAlphaText(Text, new Color(1, 1, 1, 0), Color.white, 2f));

        yield return new WaitForSeconds(7f);

        StartCoroutine(MoveCamera(cam.transform.position, camEnd, camSpeed));

        StartCoroutine(ChangeAlphaText(Text, Color.white, new Color(1, 1, 1, 0), 3f));

        StartCoroutine(ChangeAlphaImage(blackWall, Color.black, Color.clear, 3f));

        pauseCont.GetComponent<PauseScript>().SetPause(false);
        yield return new WaitForSeconds(9f);

        for (int i = 0; i < uiButtons.Length; i++)
            uiButtons[i].SetActive(true);


        cam.GetComponent<SmoothCameraScript>().enabled = true;


    }

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

    private IEnumerator ChangeAlphaText(GameObject go, Color start, Color end, float duration)
    {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            go.GetComponent<Text>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        go.GetComponent<Text>().color = end;
    }

    private IEnumerator ChangeAlphaImage(GameObject go, Color start, Color end, float duration)
    {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            go.GetComponent<Image>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        go.GetComponent<Image>().color = end;
    }
}
