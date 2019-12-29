using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTriggerScript : MonoBehaviour
{
	private float parentX, parentY, parentZ;

    // Start is called before the first frame update
    void Start()
    {
		parentX = transform.parent.transform.position.x;
		parentY = transform.parent.transform.position.y;
		parentZ = transform.parent.transform.position.z;

	}

    // Update is called once per frame
    void Update()
    {

    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Collision: " + collision.gameObject.tag);
		if (collision.gameObject.CompareTag("Ghost"))
		{
			Debug.Log("HELLO");
			collision.gameObject.transform.position = new Vector3(parentX, parentY+2, collision.gameObject.transform.position.z);
			collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		}

	}
}
