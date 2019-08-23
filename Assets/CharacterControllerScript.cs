using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
	public float Speed = 5;

	private CharacterController _controller;

	// Start is called before the first frame update
	void Start()
    {
		_controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {

		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		_controller.Move(move * Time.deltaTime * Speed);


		/*if (Input.GetKeyDown(KeyCode.RightArrow))
			GetComponent<CharacterController>().SimpleMove(new Vector3(speed, 0, 0));
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			GetComponent<CharacterController>().SimpleMove(new Vector3(-speed, 0, 0));
		else if (Input.GetKeyDown(KeyCode.UpArrow))
			GetComponent<CharacterController>().SimpleMove(new Vector3(0, speed, 0));*/
	}
}
