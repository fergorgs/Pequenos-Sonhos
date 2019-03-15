using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneNivel3 : MonoBehaviour {

    public PlayerBehavior pb;
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

            StartCoroutine(Cutscene(pb.transform.position, finalPos, finalPos2, pb.maxVel));
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
            pb.rb2d.velocity = new Vector2(speed, 0f);
            t += step;
            pb.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        //---------------------------------------------------------------
        //--------Troca sprites-------------------------------------------
        particula.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula);
        particula2.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula2);
        StartCoroutine(ChangeAlpha(enemy, enemy.GetComponent<SpriteRenderer>().color, Color.clear, 2f));
        StartCoroutine(ChangeAlpha(happyEnemy, happyEnemy.GetComponent<SpriteRenderer>().color, Color.white, 2f));
        yield return new WaitForSeconds(3f);
        //---------------------------------------------------------------------
        //-------------Movimentação Final-----------------------------------------------
        cam.GetComponent<SmoothCameraScript>().enabled = false;
        pb.GetComponent<PlayerBehavior>().set_playerState(PlayerBehavior.States.Andando);
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

    void Update()
    {
        if (wrdCont.worldIsReal() && pb.get_playerState() == PlayerBehavior.States.Andando)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        else
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
