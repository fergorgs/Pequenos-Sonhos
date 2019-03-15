using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutsceneTutorial : MonoBehaviour {

    public PlayerBehavior pb;
    public Vector2 finalPos;
    public Vector2 finalPos2;
    public Vector3 finalPosCamera;
    public GameObject title;
    public Camera mCamera;
    private bool isRuning=false;
    public Canvas canvas;

    public WorldSwitchScript wrdCont;

    public GameObject loadingScreenObj;
    public Slider slider;
    public string sceneName;
    private string path;

    private bool doneOnce = false;

    AsyncOperation async;

    private void Start()
    {

        path = "Assets/Scenes/" + sceneName + ".unity";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isRuning)
            {
                canvas.enabled = false;
                StartCoroutine(MoveCharacter(pb.transform.position, finalPos, pb.maxVel));
                //canvas.enabled = true;
            }
        }
    }
    private IEnumerator MoveCamera(Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            isRuning = true;
            t += step;
            mCamera.transform.position = Vector3.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        isRuning = false;
        /*if (!isRuning)
            StartCoroutine(ChangeAlpha(title.GetComponent<SpriteRenderer>().color, Color.white, 3f));*/
        
    }

    private IEnumerator MoveCharacter(Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            isRuning = true;
            pb.rb2d.velocity = new Vector2(speed, 0f);
            t += step;
            pb.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        pb.rb2d.velocity = Vector2.zero;
        isRuning = false;
        mCamera.GetComponent<SmoothCameraScript>().enabled = false;
        if (!isRuning && !doneOnce)
        {
            doneOnce = true;
            StartCoroutine(MoveCamera(mCamera.transform.position, finalPosCamera, 3f));
            StartCoroutine(ChangeAlpha(title.GetComponent<SpriteRenderer>().color, Color.white, 3f));
        }

    }

    IEnumerator ChangeAlpha(Color start, Color end, float duration)
    {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            title.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        title.GetComponent<SpriteRenderer>().color = end;
        yield return new WaitForSeconds(2f);

        /*PlayerPrefs.SetString("CurrentLevel", path);
        PlayerPrefs.SetInt("isAfterCP", 0);
        LoadScreen(path);*/
        mCamera.GetComponent<SmoothCameraScript>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        mCamera.GetComponent<SmoothCameraScript>().enabled = false;
        pb.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
        StartCoroutine(MoveCharacter(finalPos, finalPos2, pb.maxVel)); 
    }

    private void Update()
    {
        if (wrdCont.worldIsReal() && pb.get_playerState() == PlayerBehavior.States.Andando)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        else
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
