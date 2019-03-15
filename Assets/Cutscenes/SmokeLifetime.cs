using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeLifetime : MonoBehaviour {

    public float maxSize, lifeTime;

    private float growRate;

	// Use this for initialization
	void Start () {

        growRate = (maxSize - 1) / (lifeTime * 30);

        StartCoroutine(ChangeAlpha(gameObject, Color.white, new Color(1, 1, 1, 0), 1.6f));

    }
	
	// Update is called once per frame
	void Update () {

        transform.localScale += new Vector3(growRate, growRate, 0);
	}

    private IEnumerator ChangeAlpha(GameObject go, Color start, Color end, float duration)
    {
        yield return new WaitForSeconds(lifeTime);
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

        Destroy(gameObject);
    }
}
