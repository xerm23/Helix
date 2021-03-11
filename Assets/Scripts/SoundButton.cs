using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour{
    public Sprite sndoff;
    public Sprite sndon;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonSound()
    {
        if (GameManager.singleton.sound == 1){
           GameManager.singleton.sound = 0;
           GetComponent<Image>().sprite = sndoff;
        }
        else{
            GameManager.singleton.sound = 1;
            GetComponent<Image>().sprite = sndon;
        }
    }
}
