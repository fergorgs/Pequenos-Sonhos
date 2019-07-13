using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuarClicked : MonoBehaviour
{
	public bool clicked = false;

	private int frameCount = 0;

    public void ButtonClick()
	{
		clicked = true;
	}

	void Update()
	{
		if (clicked)
		{
			frameCount++;
			if(frameCount == 3)
			{
				clicked = false;
				frameCount = 0;
			}
		}	
	}
}
