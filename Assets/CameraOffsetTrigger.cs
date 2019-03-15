using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffsetTrigger : MonoBehaviour {

    public GameObject cam;

	// Use this for initialization
	void Start () {

        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.GetComponent<SmoothCameraScript>().SetInsideCave(true);
            cam.GetComponent<SmoothCameraScript>().SetOffSet(new Vector3(0, 3.5f, 0));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.GetComponent<SmoothCameraScript>().SetInsideCave(false);
            cam.GetComponent<SmoothCameraScript>().SetOffSet(new Vector3(0, 0, 0));
        }
    }
}
