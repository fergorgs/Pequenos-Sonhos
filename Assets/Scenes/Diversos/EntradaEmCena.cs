using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaEmCena : MonoBehaviour {

    public Camera cam;
    public PlayerBehavior pb;
    public Vector3 posFin;
    public float timeToWait = 2f;
    public Canvas canvas;

	// Use this for initialization
	void Start () {

        cam.GetComponent<SmoothCameraScript>().enabled = false;
        canvas.enabled = false;
        StartCoroutine(Entrada(pb.transform.position, posFin, pb.maxVel));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator Entrada(Vector3 posIni, Vector3 posFin, float speed) {
        yield return new WaitForSeconds(timeToWait);
        float step = (speed / (posIni - posFin).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            //Debug.Log("O2i");

            pb.set_playerState(PlayerBehavior.States.Andando);
            pb.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0f, 0);  
            t += step;
            pb.transform.position = Vector3.Lerp(posIni, posFin, t);
            yield return new WaitForFixedUpdate();
        }
        cam.GetComponent<SmoothCameraScript>().enabled = true;
        canvas.enabled = true;
    }
}
