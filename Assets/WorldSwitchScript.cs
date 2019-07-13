using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitchScript : MonoBehaviour
{
    public GameObject pauseControll, checkPoint;
    private PauseScript pauseScript;

	public float timeToReset = 10f;
	private float timeDreaming = 0;

    private bool worldReal = true;
    public bool worldStartsReal = true;
    private bool wsrAux = false;
    public bool worldIsReal() { return worldReal; }

    private bool worldShifted = false;
    public bool worldHasShifted() { return worldShifted; }
    public string colorDream, colorReal;
    public AudioSource transition;
    public AudioSource BgMusic;
	public AudioSource DreamMusic;
    public AudioSource[] ambient;

	private float startVolume;

	private bool doneFirstSwitch = false;
	public bool hasDoneFirstSwitch() { return doneFirstSwitch; }

	private bool pssS = false;
    
    // Use this for initialization
    void Start()
    {
        pauseControll = GameObject.FindGameObjectWithTag("PC");
        pauseScript = pauseControll.GetComponent<PauseScript>();

		startVolume = BgMusic.volume;
		DreamMusic.volume = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (worldShifted)
            worldShifted = false;

        if (!worldStartsReal && !wsrAux)
        {
            //Debug.Log("Entrou");
            worldReal = !worldReal;
            worldShifted = true;
            wsrAux = true;
        }

        // Debug.Log(SimpleInput.GetButtonDown("ZaWarudo"));
        if (!pauseScript.IsPaused())
        {
			if (!worldReal)
			{
				timeDreaming += Time.deltaTime;
				if (worldStartsReal || doneFirstSwitch)
				{
					if (timeDreaming > timeToReset)
					{
						transition.Play();
						worldReal = !worldReal;
						worldShifted = true;

						if (worldReal)
						{
							BgMusic.volume = startVolume;
							DreamMusic.volume = 0;
						}
						else
						{
							BgMusic.volume = 0;
							DreamMusic.volume = startVolume;
						}
					}
				}
			}

            BgMusic.UnPause();
			DreamMusic.UnPause();

            if (Input.GetKeyDown(KeyCode.S) || SimpleInput.GetButtonDown("ZaWarudo") || pssS)
            {
                transition.Play();
                worldReal = !worldReal;
                worldShifted = true;
				doneFirstSwitch = true;
				if (!worldReal)
					timeDreaming = 0;

				if (worldReal)
				{
					BgMusic.volume = startVolume;
					DreamMusic.volume = 0;
				}
				else
				{
					BgMusic.volume = 0;
					DreamMusic.volume = startVolume;
				}
			}
        }
        else
        {
            //Debug.Log("Is paused");
            BgMusic.Pause();
			DreamMusic.Pause();
        }


        StartCoroutine(GetChance());
    
    }
    public IEnumerator GetChance() { 
        float ambientChance = Random.Range(0f, 1f);

        if (ambientChance <= 0.005f) {
            int index = (int)Random.Range(0, 2);
            if (!ambient[0].isPlaying && !ambient[1].isPlaying) {
                ambient[index].Play();
            }
        }
        yield return new WaitForSeconds(120f);
    }

	public IEnumerator pressS()
	{
		Debug.Log("Pressed S");

		pssS = true;

		yield return new WaitForFixedUpdate();

		pssS = false;
	}
}