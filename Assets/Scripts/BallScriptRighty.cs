using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScriptRighty : MonoBehaviour
{

    private GameManager gm;

    private Rigidbody2D rb;
    public Vector2 leftLaunchVector;
    public Vector2 rightLaunchVector;
    public float constantSpeed = 10f;

    public Transform leftPaddle;
    public Transform rightPaddle;
    private Vector3 xCorrection2;
    private Vector3 corrected2;

    public bool inPlay = false;

    public bool rightDeath = false;
    public bool rightsBall = true;
    //want to have brickScript read this to see which method to call in the gamemanager


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!inPlay && rightDeath)
        {
            xCorrection2 = new Vector3(.35f, 0, 0);
            corrected2 = leftPaddle.position + xCorrection2;
            transform.position = corrected2;//only this line isn't working, why? same in ballscript.
            //it was because you had to drag in the paddle references by hand from the heirarchy,
            //prefabbed public object references dont work i guess
            //then why does it work with the bricks?

            if (Input.GetButtonDown("Jump"))
            {
                inPlay = true;
                rb.AddForce(leftLaunchVector);
            }
        }
        else if (!inPlay && !rightDeath)
        {
            xCorrection2 = new Vector3(-0.35f, 0, 0);
            corrected2 = rightPaddle.position + xCorrection2;
            transform.position = corrected2;

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
        if (other.CompareTag("Death Zone Left"))
        {
            //Debug.Log("Dead");
            gm.UpdateLeftScore(-1);
            inPlay = false;
            rightDeath = false;

            //to clear built up force, not sure if necessary
            rb.velocity = Vector2.zero;
        }
        else if (other.CompareTag("Death Zone Right"))
        {
            //Debug.Log("Dead");
            gm.UpdateRightScore(-1);
            inPlay = false;
            rightDeath = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Lefty"))
        {
            rightsBall = false;
        }
        else if (collision.collider.CompareTag("Righty"))
        {
            rightsBall = true;
        }
    }
}
