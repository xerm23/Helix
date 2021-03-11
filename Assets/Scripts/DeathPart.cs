using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private GameObject gameOverSound;

    private void OnEnable()
    {
        gameOverSound = GameObject.Find("Audio/Game Over");
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void HittedDeathPart()
    {
        gameOverSound.GetComponent<AudioSource>().Play();
        GameManager.singleton.GameOverPrikaz();
    }
}
