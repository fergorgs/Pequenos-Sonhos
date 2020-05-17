using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour
{

    public GameObject pauseControll;
    private PauseScript pauseScript;

    public GameObject whiteWall;
    public GameObject menuButtons;

	public AudioListener audLis;

	public float iniTimeScale = 1;

    // Use this for initialization
    void Start()
    {
        pauseControll = GameObject.FindGameObjectWithTag("PC");
        pauseScript = pauseControll.GetComponent<PauseScript>();

    }

    public void Pause()
    {
        pauseScript.SetPause(!pauseScript.IsPaused());

        if (pauseScript.IsPaused())
        {
			iniTimeScale = Time.timeScale;

			//AudioListener.pause = true;
			Time.timeScale = 0;

            whiteWall.SetActive(true);
            menuButtons.SetActive(true);
        }
        else
        {
            Time.timeScale = iniTimeScale;
			//AudioListener.pause = false;

			whiteWall.SetActive(false);
			menuButtons.GetComponent<PauseManuManager>().SetDoarOff();
            menuButtons.SetActive(false);

        }
    }
}
