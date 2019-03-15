using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgControll : MonoBehaviour {

    public GameObject bgReal, bgSonho;
    public float timeDisplayed, fadeTime, timeMov;
    public Vector3 movmentRange;

    private SpriteRenderer sprdR, sprdS;

    private bool real = true;

	// Use this for initialization
	void Start () {

        sprdR = bgReal.GetComponent<SpriteRenderer>();
        sprdS = bgSonho.GetComponent<SpriteRenderer>();

        StartCoroutine(Cicle());
	}
	
	// Update is called once per frame
	void Update () {



		
	}

    IEnumerator ChangeAlpha(Color start, Color end, float duration, SpriteRenderer sprd)
    {
        for (float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            sprd.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }

        sprd.color = end;
    }

    IEnumerator Cicle()
    {

        yield return new WaitForSeconds(timeDisplayed);

        if (real) {

            //StartCoroutine(ChangeAlpha(Color.white, Color.clear, fadeTime, sprdR));
            StartCoroutine(ChangeAlpha(new Color(1, 1, 1, 0), Color.white, fadeTime, sprdS));
        }
        else
        {
            StartCoroutine(ChangeAlpha(Color.white, new Color(1, 1, 1, 0), fadeTime, sprdS));
            //StartCoroutine(ChangeAlpha(Color.clear, Color.white, fadeTime, sprdR));
        }

        real = !real;

        StartCoroutine(Cicle());
    }

    /*IEnumerator MoveProp(Vector2 pos)
    {

        var t = 0f;
        var currentPosition = transform.position;
        while (t < 1)
        {
            t += Time.deltaTime / timeMov;
            transform.position = Vector2.Lerp(currentPosition, pos, t);
            yield return null;
        }
    }*/
}
