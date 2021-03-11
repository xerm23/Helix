using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float impulseforce = 4f;
    public int perfectPass = 1;

    private Vector3 startPos;
    private Rigidbody rb;
    private bool ignoreNextCollision = false; //sluzi za proveru kolizije sa dve platforme u isto vreme
    private bool isSuperSpeedActive = false;
    Material ballMat;

    public AudioSource klik1, klik2, klik3, klik4, klik5;



    private void Awake()
    {
        ballMat = GetComponent<Renderer>().material;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();

    }

    //Collision handling
    private void OnCollisionEnter(Collision other)
    {
        if(ignoreNextCollision)
            return;
        if (isSuperSpeedActive)
        {
            //unistava se parent jer se rusi cela platforma
            if(other.transform.parent.GetComponent<PassCheck>()) //provera zbog vertikalnih protivnika
            Destroy(other.transform.parent.gameObject);
            else
                Destroy(other.transform.parent.parent.gameObject);
        }
        else
        {
            //ovde ako se sudari sa crvenim (Death part)
            DeathPart deathPart = other.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HittedDeathPart();

        }

        rb.velocity = Vector3.zero; // Stavlja se na nulu jer lopta moze da pada sa vece visine
        rb.AddForce(Vector3.up * impulseforce, ForceMode.Impulse); // skakutanje

        //check
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        //super speed reset
        perfectPass = 1;
        isSuperSpeedActive = false;
        ballMat.color = Color.cyan; // (0,1,1,1) (r,g,b,a)


    }

    // Update is called once per frame
    void Update()
    {
        //aktiviranje super speed
        if (perfectPass > 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 1.5f * impulseforce, ForceMode.Impulse); //brze krece da pada
            ballMat.color = Color.red; // (1,0,0,0) (r,g,b,a)

        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, impulseforce *1.5f ); // limit na brzinu padanja

        if (GameManager.singleton.playable == 0)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
        else rb.useGravity = true;
        
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }


    public void ResetBall()
    {
        //super speed reset
        perfectPass = 1;
        isSuperSpeedActive = false;
        ballMat.color = Color.cyan; // (0,1,1,1) (r,g,b,a)
        rb.velocity = Vector3.zero;
        transform.position = startPos;
    }

    public void Prolazak()
    {
        if (perfectPass == 1) klik1.Play();
        else if (perfectPass == 2) klik2.Play();
        else if (perfectPass == 3) klik3.Play();
        else if (perfectPass == 4) klik4.Play();
        else if (perfectPass >= 5) klik5.Play();
        perfectPass++;
    }
}
