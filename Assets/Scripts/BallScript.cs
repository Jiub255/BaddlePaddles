using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    private GameManager gm;

    private Rigidbody2D rb;
    public Vector2 leftLaunchVector;
    public Vector2 rightLaunchVector;
    public float constantSpeed = 10f;

    public Transform leftPaddle;
    public Transform rightPaddle;
    private Vector3 xCorrection;
    private Vector3 corrected;

    public bool inPlay = false;
    public bool leftDeath = false;

    //want to have brickScript read this to see which method to call in the gamemanager
    public bool leftsBall = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!inPlay && !leftDeath)
        {
            xCorrection = new Vector3(.35f, 0, 0);
            corrected = leftPaddle.position + xCorrection;
            transform.position = corrected; 

            if (Input.GetButtonDown("Jump"))
            {
                inPlay = true;
                rb.AddForce(leftLaunchVector);
            }
        }
        else if (!inPlay && leftDeath)
        {
            xCorrection = new Vector3(-0.35f, 0, 0);
            corrected = rightPaddle.position + xCorrection;
            transform.position = corrected;

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                inPlay = true;
                rb.AddForce(rightLaunchVector);
            }
        }
        else
        {
            rb.velocity = constantSpeed * (rb.velocity.normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Death Zone Left"))
        {
            //Debug.Log("Dead");
            gm.UpdateLeftScore(-1);
            inPlay = false;
            leftDeath = true;

            //to clear built up force, not sure if necessary
            rb.velocity = Vector2.zero;
        }
        else if (other.CompareTag("Death Zone Right"))
        {
            //Debug.Log("Dead");
            gm.UpdateRightScore(-1);
            inPlay = false;
            leftDeath = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lefty"))
        {
            leftsBall = true;
        }
        else if (collision.collider.CompareTag("Righty"))
        {
            leftsBall = false;
        }
    }
}
