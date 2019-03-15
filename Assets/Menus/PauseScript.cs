using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    private bool isPaused = false;

    public bool IsPaused() { return isPaused; }
    public void SetPause(bool val) { isPaused = val; }
}

