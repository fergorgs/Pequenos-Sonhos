using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour {

    public float flashDuration;
    private int runs = 0;

	// Use this for initialization
	void Start () {


        StartCoroutine(ChangeAlpha(gameObject, new Color(1, 1, 1, 0), Color.white, flashDuration / 2));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator ChangeAlpha(GameObject go, Color start, Color end, float duration)
    {
        runs++;
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

        StartCoroutine(ChangeAlpha(gameObject, Color.white, new Color(1, 1, 1, 0), flashDuration / 2));

        if (runs == 2)
            Destroy(gameObject);
    }
}
