using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBottomTriggerScript : MonoBehaviour
{
	public GameObject bottomTrigger;

	public bool hasBottomTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
		bottomTrigger.SetActive(hasBottomTrigger);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
