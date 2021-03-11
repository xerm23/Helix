using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    float volume;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<AudioSource>().volume;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.singleton.sound == 0) 
        GetComponent<AudioSource>().volume = 0;
        else GetComponent<AudioSource>().volume = volume;
    }
}
