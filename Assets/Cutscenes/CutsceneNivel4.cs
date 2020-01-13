using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneNivel4 : MonoBehaviour {

    public WorldSwitchScript wrdCont;

    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject estatua, particula, particula2, particula3, bird, birdPuppet;
	public Vector3 outOfLevelPos, keyCutscenePos, cutscenePos, outOfCutscenePos, finalCamPos;
    public GameObject[] moveButtons;
    public GameObject SwitchButton;
    public GameObject pickupCanvas;
    public GameObject flash;
	//public AudioSource bgMusic;

    public CutsceneShift ctSh;
    public ParticleFollowPath[] paths;

    private bool cutS2started = false;

    //public GameObject[] uiButtons;
    public Camera cam;

	public AudioSource throwSound, landingSound, glowSound, flashSound, weepSound;

	private bool firstTouch = false;

    private void Start()
    {
        ctSh = birdPuppet.GetComponent<CutsceneShift>();
        paths = birdPuppet.GetComponents<ParticleFollowPath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !firstTouch)
        {
			firstTouch = true;

			for (int i = 0; i < moveButtons.Length; i++)
                moveButtons[i].GetComponent<Image>().raycastTarget = false;
            SwitchButton.GetComponent<Image>().raycastTarget = false;

            StartCoroutine(Cutscene(pc.transform.position, cutscenePos, pc.maxVelocity));
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

	private IEnumerator Cutscene(Vector3 a, Vector3 b, float speed)
    {
		cam.GetComponent<SmoothCameraScript>().enabled = false;
        //-------------out of level-----------------------------
        float step = (speed / (a - outOfLevelPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }

		pc.transform.position = keyCutscenePos;
		cam.transform.position = finalCamPos;

		wrdCont.BgMusic.enabled = false;
		wrdCont.DreamMusic.enabled = false;

		//--------------------into cutscene--------------------------
		step = (speed / (keyCutscenePos - cutscenePos).magnitude) * Time.fixedDeltaTime;
		t = 0;
		while (t <= 1.0f)
		{
			pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
			t += step;
			pc.transform.position = Vector2.Lerp(keyCutscenePos, cutscenePos, t);
			yield return new WaitForFixedUpdate();
		}

		SwitchButton.GetComponent<Image>().raycastTarget = true;
		//---------------------------------------------------------------
		//--------Troca sprites-------------------------------------------
		yield return new WaitForSeconds(2f);
		//THROW 1
		particula.SetActive(true);
        pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
		glowSound.Play();
        pc.GetComponent<ParticleSystem>().maxParticles--;
        pc.GetComponent<ParticleSystem>().Clear();
        pc.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula);
		StartCoroutine(ChangeVolume(glowSound, glowSound.volume, 0, 1f));
		yield return new WaitForSeconds(3f);

		//THROW 2
		particula2.SetActive(true);
		pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
		glowSound.volume = 0.5f;
		glowSound.Play();
		pc.GetComponent<ParticleSystem>().maxParticles--;
		pc.GetComponent<ParticleSystem>().Clear();
		pc.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(3f);
        Destroy(particula2);
		StartCoroutine(ChangeVolume(glowSound, glowSound.volume, 0, 1f));
		yield return new WaitForSeconds(3f);

		//THROW 3
		/*particula3.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula3);*/

		//Triste
		yield return new WaitForSeconds(2f);
		pc.GetComponent<Animator>().Play("Looking_Up");
		yield return new WaitForSeconds(3f);
		pc.GetComponent<Animator>().Play("Idle_Animation");
		pc.GetComponent<SpriteRenderer>().flipX = true;
		yield return new WaitForSeconds(3f);
		pc.GetComponent<Animator>().Play("Triste");
		landingSound.Play();
        //yield return new WaitForSeconds(1f);

        //Bird Movement
        birdPuppet.SetActive(true);
        bird.SetActive(false);
        birdPuppet.GetComponent<SpriteRenderer>().flipX = true;
        birdPuppet.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(4f);
        birdPuppet.GetComponent<Animator>().Play("BirdTriste");
        birdPuppet.GetComponent<SpriteRenderer>().flipX = false;

    }

    private IEnumerator Cutscene2(Vector3 a, Vector3 b, float speed)
    {
        cam.GetComponent<SmoothCameraScript>().enabled = false;
        birdPuppet.GetComponent<Animator>().Play("Bird");
        foreach (ParticleFollowPath path in paths){

            if (path.pathName == "PathBird")
                path.enabled = false;
            else
                path.enabled = true;

        }
        yield return new WaitForSeconds(6.5f);
        flash.SetActive(true);
		flashSound.Play();
        yield return new WaitForSeconds(0.5f);
        birdPuppet.SetActive(false);
        bird.transform.position = estatua.transform.position;
        bird.SetActive(true);

        bird.GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(ChangeAlpha(estatua, Color.white, new Color(1, 1, 1, 0), 3));
        yield return new WaitForSeconds(4f);
        Destroy(flash);

        //-------------Movimentação Final-----------------------------
        //Debug.Log("Ok");
        //pb.set_playerState(PlayerBehavior.States.Parado);
        pc.GetComponent<Animator>().Play("Idle_Animation");
		landingSound.Play();
		yield return new WaitForSeconds(1f);
		pc.GetComponent<SpriteRenderer>().flipX = false;
		yield return new WaitForSeconds(3f);
		//pb.set_playerState(PlayerBehavior.States.Andando);
		pc.GetComponent<Animator>().Play("Player_Walk");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }


    }

    void Update()
    {

		if (wrdCont.worldIsReal() && Mathf.Abs(pc.GetComponent<Rigidbody2D>().velocity.y) < 0.5f)//pb.get_playerState() == PlayerBehavior.States.Andando)
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		else
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
		if (ctSh.GetIsReal() && wrdCont.worldIsReal() && !cutS2started)
        {
            StartCoroutine(Cutscene2(pc.transform.position, outOfCutscenePos, pc.maxVelocity));
            cutS2started = true;
        }
    }
}
