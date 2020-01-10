using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel1 : MonoBehaviour
{
   // public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public Camera mCamera;
    public Vector3 outOfLevelPlayerPos, cutScenePlayerPos, finalCamera;
	public AudioSource level1;
    public GameObject /*fazenda, fazendaViva, fazendeiro, fazendeiroFeliz,*/ finalLevelGo, eventSystem, particle;
	public GameObject[] floresMortas, floresVivas, flashes;
    public Canvas canvas;
    public GameObject rightArrow;
    public WorldSwitchScript wrdCont;

	public Vector3 keyPositionPlayer;
	public AudioSource throwSound;

	private bool firstTouch = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !firstTouch)
        {
			//canvas.enabled = false;
			firstTouch = true;
			Destroy(rightArrow);
			//level1.Stop();
			//level1.volume = (level1.volume / 2);
			//cutSong.Play();
			eventSystem.SetActive(false);
			StartCoroutine(Cutscene(pc.transform.position, cutScenePlayerPos, finalCamera,
				new Vector3(finalLevelGo.transform.position.x, pc.transform.position.y, pc.transform.position.z), pc.maxVelocity));

			canvas.enabled = false;//eventSystem.SetActive(true);



			/*//canvas.enabled = false;
            Destroy(rightArrow);
			//level1.Stop();
			level1.volume = (level1.volume / 2);
			//cutSong.Play();
			eventSystem.SetActive(false);
            StartCoroutine(Cutscene(pb.transform.position, cutScenePlayerPos,finalCamera,
                new Vector3(finalLevelGo.transform.position.x, pb.transform.position.y, pb.transform.position.z), pb.maxVel));
            
            canvas.enabled = false;//eventSystem.SetActive(true);*/

		}
    }

    

    IEnumerator ChangeAlpha(GameObject go, Color start, Color end, float duration)
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

    private IEnumerator Cutscene(Vector3 a, Vector3 b, Vector3 finalCamera, Vector3 finalLevel, float speed)
    {
		mCamera.GetComponent<SmoothCameraScript>().enabled = false;

        //-------------------Movimentação Sai de fase------------------------------------
        float step = (speed / (a - outOfLevelPlayerPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            //Debug.Log("pos = " + pb.transform.position + "; cutScenePlayerPos = " + cutScenePlayerPos);
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            t += step;
            pc.transform.position = Vector2.Lerp(a, outOfLevelPlayerPos, t);
            yield return new WaitForFixedUpdate();
        }

		//Corta para a cutscene
		pc.gameObject.transform.position = keyPositionPlayer;
		level1.volume = 0;
		mCamera.transform.position = finalCamera;

		//-------------------Movimentação Entra Cutscene------------------------------------
		step = (speed / (keyPositionPlayer - cutScenePlayerPos).magnitude) * Time.fixedDeltaTime;
		t = 0;
		while (t <= 1.0f)
		{
			//Debug.Log("pos = " + pb.transform.position + "; cutScenePlayerPos = " + cutScenePlayerPos);
			pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
			t += step;
			pc.transform.position = Vector2.Lerp(keyPositionPlayer, cutScenePlayerPos, t);
			yield return new WaitForFixedUpdate();
		}

		pc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

		yield return new WaitForSeconds(2.5f);

		//pb.set_playerState(PlayerBehavior.States.Parado);
		//Debug.Log("Sai");
		//------------------Movimentação Camera---------------------------------------------------------
		/*mCamera.GetComponent<SmoothCameraScript>().enabled = false;
        //pb.GetComponent<ParticleSystem>().maxParticles--;
        Vector3 startCamera = mCamera.transform.position;
        step = (3f / (startCamera - finalCamera).magnitude) * Time.fixedDeltaTime;
        t = 0;
        while (t <= 1.0f)
        {
            t += step;
            mCamera.transform.position = Vector3.Lerp(startCamera, finalCamera, t);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(4.5f);*/
		//------------------Troca Sprites Fanzeda e Fazendeiro -------------------------------------
		particle.SetActive(true);
        pc.GetComponent<Animator>().Play("Player_Throw");
		throwSound.Play();
        pc.GetComponent<ParticleSystem>().maxParticles--;
        pc.GetComponent<ParticleSystem>().Clear();
        pc.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        particle.SetActive(false);
		for (int i = 0; i < 3; i++)
			flashes[i].SetActive(true);
		yield return new WaitForSeconds(2.5f);
		for(int i = 0; i < 3; i++)
		{
			StartCoroutine(ChangeAlpha(floresMortas[i], floresMortas[i].GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), 2f));
			StartCoroutine(ChangeAlpha(floresVivas[i], floresVivas[i].GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 1), 2f));
		}
		//StartCoroutine(ChangeAlpha(fazenda, fazenda.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0), 2f));
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(ChangeAlpha(fazendaViva, fazendaViva.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 1), 2f));
        yield return new WaitForSeconds(6f);
        //StartCoroutine(ChangeAlpha(fazendeiro, fazendeiro.GetComponent<SpriteRenderer>().color, Color.clear, 2f));
        //StartCoroutine(ChangeAlpha(fazendeiroFeliz, fazendeiroFeliz.GetComponent<SpriteRenderer>().color, Color.white, 2f));
        //yield return new WaitForSeconds(3f);
        //---------------------Movimentação final--------------------------------------------------------
        step = (speed / (cutScenePlayerPos - finalLevel).magnitude) * Time.fixedDeltaTime;

        //Debug.Log("Aqui");
        t = 0;
        while (t <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(pc.maxVelocity, 0f);
            //pb.set_playerState(PlayerBehavior.States.Andando);
            t += step;
            pc.transform.position = Vector2.Lerp(cutScenePlayerPos, finalLevel, t);
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
