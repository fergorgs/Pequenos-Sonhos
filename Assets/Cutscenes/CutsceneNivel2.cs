using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel2 : MonoBehaviour {

    public PlayerBehavior pb;
    public GameObject flash, particula, passaroMorto, passaroReal, passaroDefault;
    public Vector3 finalPos, finalPos2, finalPosCam;

    public WorldSwitchScript wrdCont;

    public GameObject[] uiButtons;
    public Camera cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < uiButtons.Length; i++)
                Destroy(uiButtons[i]);

            StartCoroutine(Cutscene(pb.transform.position, finalPos, finalPos2, pb.maxVel));
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

    private IEnumerator Cutscene(Vector3 a, Vector3 b, Vector3 c, float speed)
    {
        //-------------Movimentação Inicial-----------------------------
        
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        Debug.Log("speed = " + speed + " / step = " + step);
        float t = 0;
        while (t <= 1.0f)
        {
            pb.rb2d.velocity = new Vector2(speed, 0f);
            t += step;
            pb.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        cam.GetComponent<SmoothCameraScript>().enabled = false;
        StartCoroutine(MoveCamera(cam.transform.position, finalPosCam, 2f));

		yield return new WaitForSeconds(2f);
		//---------------------------------------------------------------
		//--------Throw-------------------------------------------
		particula.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula);
        flash.SetActive(true);
        yield return new WaitForSeconds(1f);

        Destroy(passaroMorto);
        passaroReal.SetActive(true);
        yield return new WaitForSeconds(9.8f);
        passaroDefault.SetActive(true);
        passaroDefault.transform.position = passaroReal.transform.position;
        passaroDefault.GetComponent<IdleBehavior>().idleSpeed = 2f;

        yield return new WaitForSeconds(4f);

        passaroDefault.GetComponent<IdleBehavior>().idleSpeed = 0.3f;

        //---------------------------------------------------------------------
        //-------------Movimentação Final-----------------------------------------------
        pb.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
        speed = 1f;
        float step2 = (speed / (b - c).magnitude) * Time.fixedDeltaTime;
        float d = 0;
        while (d <= 1.0f)
        {
            pb.rb2d.velocity = new Vector2(speed, 0f);
            d += step;
            pb.transform.position = Vector2.Lerp(b, c, d);
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
