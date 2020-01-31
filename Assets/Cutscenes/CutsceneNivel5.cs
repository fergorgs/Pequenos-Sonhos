using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneNivel5 : MonoBehaviour {

    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject bird, birdPuppet, casa, smoke;
    public Vector3 finalPos;
    public Vector3 finalPos2;
    public Vector3 finalPosCamera;

    public Sprite portaFechada, portaAberta;

    public GameObject[] uiButtons;
    public Camera cam;

    public WorldSwitchScript wrdCont;

    public GameObject blackWall, text1, text2, text3, text4, text5, text6, nextBtn, doarBtn, menuBtn, doarText, menuText;

	public AudioSource doorOpenSound, doorCloseSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < uiButtons.Length; i++)
                Destroy(uiButtons[i]);

            StartCoroutine(Cutscene(pc.transform.position, finalPos, finalPos2, pc.maxVelocity));
        }
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

    private IEnumerator ChangeAlpha(GameObject go, Color start, Color end, float duration)
    {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            go.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        go.GetComponent<SpriteRenderer>().color = end;
    }

	IEnumerator ChangeVolume(AudioSource audSorce, float startVol, float endVol, float duration)
	{
		for (float t = 0f; t < 0.5f; t += Time.deltaTime)
		{
			yield return null;
		}
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float normalizedTime = t / duration;
			audSorce.volume = Mathf.Lerp(startVol, endVol, normalizedTime);
			//go.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
			yield return null;
		}
		audSorce.volume = endVol;
	}


	private IEnumerator SendSmoke()
    {
        while (true)
        {
            Instantiate(smoke);

            yield return new WaitForSeconds(2f);
        }


    }

    private IEnumerator Cutscene(Vector3 a, Vector3 b, Vector3 c, float speed)
    {
		StartCoroutine(ChangeVolume(wrdCont.BgMusic, wrdCont.BgMusic.volume, wrdCont.BgMusic.volume * 0.4f, 3f));

        //-------------Movimentação Inicial-----------------------------
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        //---------------------------------------------------------------
        //--------Passaro-------------------------------------------
        birdPuppet.transform.position = bird.transform.position;
        Destroy(bird);
        birdPuppet.SetActive(true);
        yield return new WaitForSeconds(5f);
        birdPuppet.GetComponent<Animator>().Play("BirdPousado");
        yield return new WaitForSeconds(1f);
        //---------------------------------------------------------------------
        //-------------Movimentação Final-----------------------------------------------
        //cam.GetComponent<SmoothCameraScript>().enabled = false;
        //pb.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
        float step2 = (speed / (b - c).magnitude) * Time.fixedDeltaTime;
        float d = 0;
        while (d <= 1.0f)
        {
			pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            d += step;
            pc.transform.position = Vector2.Lerp(b, c, d);
            yield return new WaitForFixedUpdate();
        }

        //-------------------------Entra na casa-----------------------------
        casa.GetComponent<SpriteRenderer>().sprite = portaAberta;
		doorOpenSound.Play();
        StartCoroutine(ChangeAlpha(pc.gameObject, Color.white, Color.clear, 2));
        pc.gameObject.GetComponent<ParticleSystem>().Stop();
        pc.gameObject.GetComponent<ParticleSystem>().Clear();
        yield return new WaitForSeconds(2f);
        casa.GetComponent<SpriteRenderer>().sprite = portaFechada;
		doorCloseSound.Play();
        yield return new WaitForSeconds(2f);

        StartCoroutine(SendSmoke());

        cam.GetComponent<SmoothCameraScript>().enabled = false;

        StartCoroutine(MoveCamera(cam.transform.position, finalPosCamera, 3f));

        //-----------------------Text Final-------------------------------

        yield return new WaitForSeconds(3f);

        blackWall.SetActive(true);
        StartCoroutine(ChangeAlphaImage(blackWall, Color.clear, new Color(0, 0, 0, 0.7f), 3f));
        yield return new WaitForSeconds(2f);

        text1.SetActive(true);
        StartCoroutine(ChangeAlphaText(text1, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(2f);

        text2.SetActive(true);
        StartCoroutine(ChangeAlphaText(text2, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(4f);

        text3.SetActive(true);
        StartCoroutine(ChangeAlphaText(text3, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(7f);

        nextBtn.SetActive(true);
        StartCoroutine(ChangeAlphaImage(nextBtn, Color.clear, Color.white, 1f));

    }

    public void CallNextPage()
    {

        StartCoroutine(NextPage());
    }

    public IEnumerator NextPage()
    {
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        nextBtn.SetActive(false);

        text4.SetActive(true);
        StartCoroutine(ChangeAlphaText(text4, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(3f);

        text5.SetActive(true);
        StartCoroutine(ChangeAlphaText(text5, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(5f);

        text6.SetActive(true);
        StartCoroutine(ChangeAlphaText(text6, Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(7f);
        
        doarBtn.SetActive(true);
        doarText.SetActive(true);
        StartCoroutine(ChangeAlphaImage(doarBtn, Color.clear, Color.white, 1f));
        StartCoroutine(ChangeAlphaText(doarText, Color.clear, Color.black, 1f));
        yield return new WaitForSeconds(1f);

        menuBtn.SetActive(true);
        menuText.SetActive(true);
        StartCoroutine(ChangeAlphaImage(menuBtn, Color.clear, Color.white, 1f));
        StartCoroutine(ChangeAlphaText(menuText, Color.clear, Color.black, 1f));
    }

    private void Update()
    {

		if (wrdCont.worldIsReal() && Mathf.Abs(pc.GetComponent<Rigidbody2D>().velocity.y) < 0.5f)//pb.get_playerState() == PlayerBehavior.States.Andando)
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		else
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
	}
}
