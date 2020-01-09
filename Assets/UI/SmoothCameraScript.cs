using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraScript : MonoBehaviour {

    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    public float dampTime = 0.15f;
    public float normalDampTime = 0.15f;
    public float shiftDampTime = 0.3f;
    public float dampTimeReset = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private bool nivel0 = false; public void setNivel0(bool val, GameObject go) { if (go.name == "Controle do Tutorial") nivel0 = val; }

    public float zoomOutVel = 0.3f;

    private bool insideCave = false; public void SetInsideCave(bool val) { insideCave = val; }

    private Vector3 offSet = Vector3.zero; public void SetOffSet(Vector3 val) { offSet = val; }

    private Camera cam;

	//public bool getFromTarget = true;
	private Vector3 vectorTarget;
	public void SetVectorTarget(Vector3 vector) { vectorTarget = vector; }

	public Transform GetDefaultTarget() { return GameObject.FindGameObjectWithTag("Player").transform; }

    void Start()
    {
        wrdControl = GameObject.FindGameObjectWithTag("WCS");
        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        cam = GetComponent<Camera>();

        if (cam == null)
            Debug.Log("Deu ruim");

        dampTime = normalDampTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (wrdSwitch.worldHasShifted() && wrdSwitch.worldIsReal() && !nivel0)
        {
            dampTime = shiftDampTime;
            StartCoroutine(DampTimeReset(dampTimeReset));
        }

        //transform.position = new Vector3(0, 0, zoomOutVel);
        if (insideCave)
        {
            //Debug.Log("Entra, z = " + transform.position.z);
            if (transform.position.z >= -20f)
                transform.position -= new Vector3(0, 0, zoomOutVel);
        }
        else if (transform.position.z <= -10f)
            transform.position += new Vector3(0, 0, zoomOutVel);

    }

	private void FixedUpdate()
	{
		if (target)
		{
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.4f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination + offSet, ref velocity, dampTime);
		}
		else
		{
			Vector3 point = cam.WorldToViewportPoint(vectorTarget);
			Vector3 delta = vectorTarget - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.4f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination + offSet, ref velocity, dampTime);
		}
	}

	public IEnumerator DampTimeReset(float dampTimeReset)
    {
        yield return new WaitForSeconds(dampTimeReset);
        dampTime = normalDampTime;
    }
}
