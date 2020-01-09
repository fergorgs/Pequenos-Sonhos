using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSafeScript : MonoBehaviour
{
	public GameObject targetObj;

	public Vector3 targetDest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject == targetObj)
			targetObj.transform.position = targetDest;	
	}
}
