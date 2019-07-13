using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltroSonhoScript : MonoBehaviour {
    
    public GameObject wrdControl;
    private WorldSwitchScript wrdSwitch;

    private Image img;

	private SpriteRenderer sprd;

	private bool gettingWhite = false;
	public float iniPeriodTime = 0.05f;
	//public float finPeriodTime = 0.1f;
	public float minTransparency = 0.6f;

	public float periodLeap = 0.04f;
	private float timeDreaming = 0;

	private float curPeriodTime;

	// Use this for initialization
	void Start ()
    {
        wrdControl = GameObject.FindGameObjectWithTag("WCS");
        wrdSwitch = wrdControl.GetComponent<WorldSwitchScript>();

        img = GetComponent<Image>();

		sprd = GetComponent<SpriteRenderer>();
		//periodLeap = (finPeriodTime - iniPeriodTime) / wrdSwitch.timeToReset;
	}
	
	// Update is called once per frame
	void Update () {

        if (wrdSwitch.worldHasShifted())
        {
			if (wrdSwitch.worldIsReal())
				img.color = Color.clear;
			else
			{
				img.color = Color.white;
				curPeriodTime = iniPeriodTime;
				timeDreaming = 0;
			}
        }

		//if (!wrdSwitch.worldIsReal())
		//{
			//if(wrdSwitch.worldStartsReal || wrdSwitch.hasDoneFirstSwitch())
			//{ 
				if (!wrdSwitch.pauseControll.gameObject.GetComponent<PauseScript>().IsPaused())
				{
					timeDreaming += Time.deltaTime;

					if (!gettingWhite)
					{
						//Debug.Log("hello");
						//img.color = Color.Lerp(img.color, new Color(1, 1, 1, minTransparency), curPeriodTime);
						sprd.color = Color.Lerp(sprd.color, new Color(1, 1, 1, minTransparency), curPeriodTime);

						if (img.color.a < minTransparency + 0.05)
							gettingWhite = true;
					}
					else
					{
						sprd.color = Color.Lerp(sprd.color, Color.white, curPeriodTime);

						if (img.color.a > 0.95f)
						{
							gettingWhite = false;
							if (timeDreaming > 1)
							{
								curPeriodTime += periodLeap;
								timeDreaming = 0;
							}
							//	curPeriodTime += 0.05f;
						}
					}
				}
			//}
		//}
        
	}
}
