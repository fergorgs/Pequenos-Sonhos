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
		/*runs++;
		float deltaAlpha;
		float absAlpha = Mathf.Abs(start.a - end.a);

		if(start.a > end.a)
		{
			while (go.GetComponent<SpriteRenderer>().color.a > end.a + 0.05f)
			{
				deltaAlpha = (absAlpha * Time.deltaTime) / duration;

				go.GetComponent<SpriteRenderer>().color = new Color(start.r, start.g, start.b, go.GetComponent<SpriteRenderer>().color.a-deltaAlpha);

				yield return null;
			}
		}
		else
		{
			while (go.GetComponent<SpriteRenderer>().color.a < end.a - 0.05f)
			{
				deltaAlpha = (absAlpha * Time.deltaTime) / duration;

				go.GetComponent<SpriteRenderer>().color = new Color(start.r, start.g, start.b, go.GetComponent<SpriteRenderer>().color.a + deltaAlpha);

				yield return null;
			}
		}*/
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

		//Debug.Log("runs = " + runs);
		//if (runs == 2)
		//{
			//Debug.Log("destroy");
			Destroy(gameObject);
		//}

		StartCoroutine(ChangeAlpha(gameObject, Color.white, new Color(1, 1, 1, 0), flashDuration / 2));
	}
}
