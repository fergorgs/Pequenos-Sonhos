using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            PlayerPrefs.SetInt("isAfterCP", 1);

        Debug.Log(PlayerPrefs.GetInt("isAfterCP"));
    }
}
