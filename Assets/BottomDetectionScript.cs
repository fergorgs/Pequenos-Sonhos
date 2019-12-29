using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Trigger: " + collision.gameObject.name);
	}
}
