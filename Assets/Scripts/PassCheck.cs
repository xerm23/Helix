using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        int multiplier = FindObjectOfType<BallController>().perfectPass;
        GameManager.singleton.AddScore(10 * multiplier); // dodati
        FindObjectOfType<BallController>().Prolazak();

    }
}