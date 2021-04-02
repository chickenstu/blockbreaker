//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parms
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 0f;
    [SerializeField] float yPush = 10f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBall;
    bool hasStarted = false;

    // cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRBD2D;

     // Start is called before the first frame update
    void Start()
    {
        paddleToBall = transform.position - paddle1.transform.position;
        // more efficient to do this only once
        myAudioSource = GetComponent<AudioSource>();
        myRBD2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LockBallToPaddle();
        LaunchBallOnClick();
    }
        
    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0) && !hasStarted)
        {
            myRBD2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;  
        }
    }

    private void LockBallToPaddle()
    {
        if (!hasStarted)
        {
            Vector2 ballPos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = ballPos + paddleToBall;
        }
    }

    // Play a click sound on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // to make sure ball doesn't get stuck in an endless loop
        float randX = Random.Range(-randomFactor, randomFactor);
        float randY = Random.Range(-randomFactor, randomFactor);
        Vector2 velTweak = new Vector2(randX, randY);
        if (hasStarted)
        {
            AudioClip randClip = ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)];
            //play one shot plays the clip without being interrupted
            myAudioSource.PlayOneShot(randClip);
            myRBD2D.velocity += velTweak;
        }
     }
}
