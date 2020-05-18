using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel3 : MonoBehaviour {

    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject enemy, happyEnemy, particula, particula2, flash, fazendeiroT, fazendeiroF, interrogacao;
    public Vector3 outOfLevelPos, keyCutscenePos, finalCutscenePos, outOfCutscenePos, cutsceneCamPos;

    public WorldSwitchScript wrdCont;

    public GameObject[] uiButtons;
    public Camera cam;

	public AudioSource throwSound, glowSound, flashSound, manSound, questionSound;

	private bool firstTouch = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !firstTouch)
        {
			firstTouch = true;

            for (int i = 0; i < uiButtons.Length; i++)
                Destroy(uiButtons[i]);

            StartCoroutine(Cutscene(pc.transform.position, outOfLevelPos, outOfCutscenePos, pc.maxVelocity));
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

        //-------------sai do nivel-----------------------------
        float step = (speed / (a - outOfLevelPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, outOfLevelPos, t);
            yield return new WaitForFixedUpdate();
        }

		pc.transform.position = keyCutscenePos;
		cam.transform.position = cutsceneCamPos;
		wrdCont.BgMusic.enabled = false;
		pc.passos.enabled = false;

		//-------------entra em cena----------------------------
		step = (speed / (keyCutscenePos - finalCutscenePos).magnitude) * Time.fixedDeltaTime;
		t = 0;
		while (t <= 1.0f)
		{
			pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
			t += step;
			pc.transform.position = Vector2.Lerp(keyCutscenePos, finalCutscenePos, t);
			yield return new WaitForFixedUpdate();
		}

		//---------------------------------------------------------------
		//--------Olha pra cima-------------------------------------------
		yield return new WaitForSeconds(1f);
		pc.GetComponent<Animator>().Play("Looking_Up");
		yield return new WaitForSeconds(2f);
		pc.GetComponent<Animator>().Play("Idle_Animation");
		yield return new WaitForSeconds(2f);

		//---------------------------------------------------------------
		//--------joga pickup--------------------------------------------
		particula.SetActive(true);
        pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
		glowSound.Play();
        pc.GetComponent<ParticleSystem>().maxParticles--;
        pc.GetComponent<ParticleSystem>().Clear();
        pc.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(6f);
        Destroy(particula);
		StartCoroutine(ChangeVolume(glowSound, glowSound.volume, 0, 1f));
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(ChangeAlpha(interrogacao, interrogacao.GetComponent<SpriteRenderer>().color, Color.white, 0.1f));
		yield return new WaitForSeconds(0.5f);
		questionSound.Play();
		yield return new WaitForSeconds(2f);
		StartCoroutine(ChangeAlpha(interrogacao, interrogacao.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), 0.1f));
		yield return new WaitForSeconds(1.5f);
		particula2.SetActive(true);
		pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
		glowSound.volume = 0.5f;
		glowSound.Play();
		pc.GetComponent<ParticleSystem>().maxParticles--;
		pc.GetComponent<ParticleSystem>().Clear();
		pc.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(3f);

		//---------------------------------------------------------------
		//--------muda sprite inimigo------------------------------------
		Destroy(particula2);
		StartCoroutine(ChangeVolume(glowSound, glowSound.volume, 0, 1f));
		StartCoroutine(ChangeAlpha(enemy, enemy.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), 2f));
        StartCoroutine(ChangeAlpha(happyEnemy, happyEnemy.GetComponent<SpriteRenderer>().color, Color.white, 2f));
		flash.SetActive(true);
		flashSound.Play();
        yield return new WaitForSeconds(2f);
		Destroy(enemy);
		yield return new WaitForSeconds(2f);

		//---------------------------------------------------------------
		//--------muda sprite fazendeiro---------------------------------

		StartCoroutine(ChangeAlpha(fazendeiroT, fazendeiroT.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), 2f));
		StartCoroutine(ChangeAlpha(fazendeiroF, fazendeiroF.GetComponent<SpriteRenderer>().color, Color.white, 2f));

		yield return new WaitForSeconds(1f);

		manSound.Play();
		yield return new WaitForSeconds(2f);

		//---------------------------------------------------------------------
		//-------------Movimentação Final-----------------------------------------------
		cam.GetComponent<SmoothCameraScript>().enabled = false;
        //pc.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
        float step2 = (speed / (finalCutscenePos - outOfCutscenePos).magnitude) * Time.fixedDeltaTime;
        float d = 0;
        while (d <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            d += step;
            pc.transform.position = Vector2.Lerp(finalCutscenePos, outOfCutscenePos, d);
            yield return new WaitForFixedUpdate();
        }

    }

	private void Update()
	{
		if (wrdCont.worldIsReal() && Mathf.Abs(pc.GetComponent<Rigidbody2D>().velocity.y) < 0.5f)//pb.get_playerState() == PlayerBehavior.States.Andando)
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		else
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

		if(enemy != null)
		{
			if (enemy.GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 0))
				Destroy(enemy);
		}
	}
}
