using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel2 : MonoBehaviour {

    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject flash, particula, passaroMorto, passaroReal, passaroDefault, balao;
    public Vector3 outOfLevelPos, keyCutscenePos, cutscenePos, outOfCutscenePos, firstCamPos, finalPosCam;

    public WorldSwitchScript wrdCont;

    public GameObject[] uiButtons;
    public Camera cam;

	public AudioSource throwSound, glowSound, flashSound;

	private bool firstTouch = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !firstTouch)
        {
			firstTouch = true;

			for (int i = 0; i < uiButtons.Length; i++)
                Destroy(uiButtons[i]);

            StartCoroutine(Cutscene(pc.transform.position, cutscenePos, outOfCutscenePos, pc.maxVelocity));
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
        yield return new WaitForSeconds(2f);
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

	private IEnumerator Cutscene(Vector3 a, Vector3 b, Vector3 c, float speed)
    {
		cam.GetComponent<SmoothCameraScript>().enabled = false;

		//-------------Movimentação Out of Level-----------------------------

		float step = (speed / (a - outOfLevelPos).magnitude) * Time.fixedDeltaTime;
        Debug.Log("speed = " + speed + " / step = " + step);
        float t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, outOfLevelPos, t);
            yield return new WaitForFixedUpdate();
        }

		pc.gameObject.transform.position = keyCutscenePos;
		cam.transform.position = firstCamPos;
		wrdCont.BgMusic.enabled = false;

		//-------------Movimentação Into Cutscene-----------------------------

		step = (speed / (keyCutscenePos - cutscenePos).magnitude) * Time.fixedDeltaTime;
		Debug.Log("speed = " + speed + " / step = " + step);
		t = 0;
		while (t <= 1.0f)
		{
			pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
			t += step;
			pc.transform.position = Vector2.Lerp(keyCutscenePos, cutscenePos, t);
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(2f);
		//---------------------------------------------------------------
		//--------Throw-------------------------------------------
		particula.SetActive(true);
        pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
		glowSound.Play();
        pc.GetComponent<ParticleSystem>().maxParticles--;
        pc.GetComponent<ParticleSystem>().Clear();
        pc.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
		StartCoroutine(ChangeVolume(glowSound, glowSound.volume, 0, 1));
        Destroy(particula);
        flash.SetActive(true);
		flashSound.Play();
        yield return new WaitForSeconds(1f);

		//passaro real voando-------------------------------------------------------
        Destroy(passaroMorto);
        passaroReal.SetActive(true);
		yield return new WaitForSeconds(5f);
		pc.GetComponent<Animator>().Play("Looking_Up");
		yield return new WaitForSeconds(4.5f);
		pc.GetComponent<SpriteRenderer>().flipX = true;
		yield return new WaitForSeconds(2f);
		pc.GetComponent<Animator>().Play("Idle_Animation");

		yield return new WaitForSeconds(1f);

		//camera wideshot---------------------------------------------------------
		StartCoroutine(MoveCamera(cam.transform.position, finalPosCam, 1f));
		yield return new WaitForSeconds(1f);

		//pop up balao-------------------------------------------------------------
		balao.SetActive(true);
		yield return new WaitForSeconds(3.5f);
		passaroDefault.SetActive(true);
		StartCoroutine(ChangeAlpha(passaroDefault, new Color(1, 1, 1, 0), Color.white, 2));
		yield return new WaitForSeconds(3f);

		//pop out----------------------------------------------------------------
		balao.GetComponent<Animator>().Play("PopOut");
		passaroDefault.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
		pc.GetComponent<Animator>().Play("Looking_Up");
		yield return new WaitForSeconds(1f);
		passaroDefault.GetComponent<IdleBehavior>().enabled = true;
		passaroDefault.GetComponent<IdleBehavior>().SetIsCutScene(true);
		passaroDefault.GetComponent<IdleBehavior>().idleSpeed = 2f;
        yield return new WaitForSeconds(3f);
		pc.GetComponent<Animator>().Play("Idle_Animation");
		yield return new WaitForSeconds(1f);
		passaroDefault.GetComponent<IdleBehavior>().idleSpeed = 0.3f;
		yield return new WaitForSeconds(1f);
		pc.GetComponent<SpriteRenderer>().flipX = false;

		//---------------------------------------------------------------------
		//-------------Movimentação Final-----------------------------------------------
		//pb.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
		speed = 1f;
        float step2 = (speed / (cutscenePos - outOfCutscenePos).magnitude) * Time.fixedDeltaTime;
        float d = 0;
        while (d <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            d += step;
            pc.transform.position = Vector2.Lerp(cutscenePos, outOfCutscenePos, d);
            yield return new WaitForFixedUpdate();
        }

    }

    private void Update()
    {
        if (wrdCont.worldIsReal() && Mathf.Abs(pc.GetComponent<Rigidbody2D>().velocity.y) < 0.5f)//pb.get_playerState() == PlayerBehavior.States.Andando)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        else
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
