using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneNivel4 : MonoBehaviour {

    public WorldSwitchScript wrdCont;

    public PlayerBehavior pb;
    public GameObject estatua, particula, particula2, particula3, bird, birdPuppet;
    public Vector3 finalPos;
    public Vector3 finalPos2;
    public GameObject[] moveButtons;
    public GameObject SwitchButton;
    public GameObject pickupCanvas;
    public GameObject flash;
	public AudioSource bgMusic;

    public CutsceneShift ctSh;
    public ParticleFollowPath[] paths;

    private bool cutS2started = false;

    //public GameObject[] uiButtons;
    public Camera cam;

    private void Start()
    {
        ctSh = birdPuppet.GetComponent<CutsceneShift>();
        paths = birdPuppet.GetComponents<ParticleFollowPath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < moveButtons.Length; i++)
                moveButtons[i].GetComponent<Image>().raycastTarget = false;
            SwitchButton.GetComponent<Image>().raycastTarget = false;

            StartCoroutine(Cutscene(pb.transform.position, finalPos, pb.maxVel));
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

    private IEnumerator Cutscene(Vector3 a, Vector3 b, float speed)
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

		wrdCont.BgMusic.enabled = false;
        SwitchButton.GetComponent<Image>().raycastTarget = true;
        //---------------------------------------------------------------
        //--------Troca sprites-------------------------------------------
        //THROW 1
        particula.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula);

        //THROW 2
        particula2.SetActive(true);
        pb.animator.Play("Player_Throw");
        pb.GetComponent<ParticleSystem>().maxParticles--;
        pb.GetComponent<ParticleSystem>().Clear();
        pb.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        Destroy(particula2);

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
        pb.GetComponent<SpriteRenderer>().flipX = true;
        pb.animator.Play("Triste");
        yield return new WaitForSeconds(1f);

        //Bird Movement
        birdPuppet.SetActive(true);
        bird.SetActive(false);
        birdPuppet.GetComponent<SpriteRenderer>().flipX = true;
        birdPuppet.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(3f);
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
        pb.set_playerState(PlayerBehavior.States.Parado);
        pb.animator.Play("Idle_Animation");
        yield return new WaitForSeconds(2f);
        pb.set_playerState(PlayerBehavior.States.Andando);
        pb.animator.Play("Player_Walk");
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            pb.rb2d.velocity = new Vector2(speed, 0f);
            t += step;
            pb.transform.position = Vector2.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }


    }

    void Update()
    {

        if (wrdCont.worldIsReal() && pb.get_playerState() == PlayerBehavior.States.Andando)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        else
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        if (ctSh.GetIsReal() && wrdCont.worldIsReal() && !cutS2started)
        {
            StartCoroutine(Cutscene2(pb.transform.position, finalPos2, pb.maxVel));
            cutS2started = true;
        }
    }
}
