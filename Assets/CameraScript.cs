using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Player;

    public float cameraDelay = 2;

    private Transform plrtrfm;
    private Transform trfm;

    private Vector3 trgPos;
    
    // Use this for initialization
	void Start () {

        trfm = GetComponent<Transform>();
        plrtrfm = Player.GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update () {

        trgPos = new Vector3(plrtrfm.position.x, plrtrfm.position.y+2, trfm.position.z);

        //var t = 0f;
        var currentPosition = transform.position;
        // while (t < 1)
        //{
        //t += Time.deltaTime / cameraDelay;
        trfm.position = trgPos;//Vector3.Lerp(currentPosition, trgPos, Time.time / cameraDelay);
            
        //}

        //StartCoroutine(MoveCamera(trgPos));
	}

    /*public IEnumerator MoveCamera(Vector3 trgPos)
    {
        
    }*/
}
