using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel1 : MonoBehaviour
{
    public PlayerBehavior pb;
    public Camera mCamera;
    public Vector3 finalPos, finalCamera;
    public AudioSource level1, cutSong;
    public GameObject /*fazenda, fazendaViva, fazendeiro, fazendeiroFeliz,*/ finalLevelGo, eventSystem, particle;
	public GameObject[] floresMortas, floresVivas, flashes;
    public Canvas canvas;
    public GameObject rightArrow;
    public WorldSwitchScript wrdCont;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //canvas.enabled = false;
            Destroy(rightArrow);
			//level1.Stop();
			level1.volume = (level1.volume / 2);
			//cutSong.Play();
			eventSystem.SetActive(false);
            StartCoroutine(Cutscene(pb.transform.position, finalPos,finalCamera,
                new Vector3(finalLevelGo.transform.position.x, pb.transform.position.y, pb.transform.position.z), pb.maxVel));
            
            canvas.enabled = false;//eventSystem.SetActive(true);

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
        //-------------------Movimentação inicial------------------------------------
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            //Debug.Log("pos = " + pb.transform.position + "; finalPos = " + finalPos);
            pb.rb2d.velocity = new Vector2(speed, 0f);
            t += step;
            pb.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        pb.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        pb.set_playerState(PlayerBehavior.States.Parado);
        //Debug.Log("Sai");
        //------------------Movimentação Camera---------------------------------------------------------
        mCamera.GetComponent<SmoothCameraScript>().enabled = false;
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
        yield return new WaitForSeconds(4.5f);
        //------------------Troca Sprites Fanzeda e Fazendeiro -------------------------------------
        particle.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        particle.SetActive(false);
		for (int i = 0; i < 3; i++)
			flashes[i].SetActive(true);
		yield return new WaitForSeconds(1.5f);
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
        step = (speed / (finalPos - finalLevel).magnitude) * Time.fixedDeltaTime;

        //Debug.Log("Aqui");
        t = 0;
        while (t <= 1.0f)
        {
            pb.rb2d.velocity = new Vector2(pb.maxVel, 0f);
            pb.set_playerState(PlayerBehavior.States.Andando);
            t += step;
            pb.transform.position = Vector2.Lerp(finalPos, finalLevel, t);
            yield return new WaitForFixedUpdate();
        }

    }

    private void Update()
    {
        if (wrdCont.worldIsReal() && pb.get_playerState() == PlayerBehavior.States.Andando)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        else
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
