using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitchScript : MonoBehaviour
{
    public GameObject pauseControll, checkPoint;
    private PauseScript pauseScript;

    private bool worldReal = true;
    public bool worldStartsReal = true;
    private bool wsrAux = false;
    public bool worldIsReal() { return worldReal; }

    private bool worldShifted = false;
    public bool worldHasShifted() { return worldShifted; }
    public string colorDream, colorReal;
    public AudioSource transition;
    public AudioSource BgMusic;
    public AudioSource[] ambient;
    
    // Use this for initialization
    void Start()
    {
        pauseControll = GameObject.FindGameObjectWithTag("PC");
        pauseScript = pauseControll.GetComponent<PauseScript>();
        
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
            BgMusic.UnPause();
            if (Input.GetKeyDown(KeyCode.S) || SimpleInput.GetButtonDown("ZaWarudo"))
            {
                transition.Play();
                worldReal = !worldReal;
                worldShifted = true;
            }
        }
        else
        {
            //Debug.Log("Is paused");
            BgMusic.Pause();
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
}