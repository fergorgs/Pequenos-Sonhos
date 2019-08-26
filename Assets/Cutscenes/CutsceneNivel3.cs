using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel3 : MonoBehaviour {

    public PlayerBehavior pb;
	public PlayerControllingScript pc;
    public GameObject enemy, happyEnemy, particula, particula2;
    public Vector3 finalPos;
    public Vector3 finalPos2;

    public WorldSwitchScript wrdCont;

    public GameObject[] uiButtons;
    public Camera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < uiButtons.Length; i++)
                Destroy(uiButtons[i]);

            StartCoroutine(Cutscene(pc.transform.position, finalPos, finalPos2, pc.maxVelocity));
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

    private IEnumerator Cutscene(Vector3 a, Vector3 b, Vector3 c, float speed)
    {
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
		//--------Troca sprites-------------------------------------------
		yield return new WaitForSeconds(1f);
		particula.SetActive(true);
        pc.GetComponent<Animator>().Play("Player_Throw");
        pc.GetComponent<ParticleSystem>().maxParticles--;
        pc.GetComponent<ParticleSystem>().Clear();
        pc.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(5f);
        Destroy(particula);
        particula2.SetActive(true);
		pc.GetComponent<Animator>().Play("Player_Throw");
		pc.GetComponent<ParticleSystem>().maxParticles--;
		pc.GetComponent<ParticleSystem>().Clear();
		pc.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(3f);
        Destroy(particula2);
        StartCoroutine(ChangeAlpha(enemy, enemy.GetComponent<SpriteRenderer>().color, Color.clear, 2f));
        StartCoroutine(ChangeAlpha(happyEnemy, happyEnemy.GetComponent<SpriteRenderer>().color, Color.white, 2f));
        yield return new WaitForSeconds(4f);
        //---------------------------------------------------------------------
        //-------------Movimentação Final-----------------------------------------------
        cam.GetComponent<SmoothCameraScript>().enabled = false;
        //pc.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
        float step2 = (speed / (b - c).magnitude) * Time.fixedDeltaTime;
        float d = 0;
        while (d <= 1.0f)
        {
            pc.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0f);
            d += step;
            pc.transform.position = Vector2.Lerp(b, c, d);
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
