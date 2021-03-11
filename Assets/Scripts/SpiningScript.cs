using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningScript : MonoBehaviour
{
    private float rotateSpeed = 0.1f;
    private int rotateDir; 

    // Start is called before the first frame update
    void Start()
    {
        rotateDir = Random.Range(1, 3);
        if (rotateDir == 2) rotateDir = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * rotateDir);
        
    }
}
