using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{

    private GameManager gm;

    public GameObject ball;
    public GameObject ballRight;
    private BallScript ballScript;
    private BallScriptRighty ballScriptRighty;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {

        ballScript = ball.GetComponent<BallScript>();
        ballScriptRighty = ballRight.GetComponent<BallScriptRighty>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Left Ball"))
        {
            if (ballScript.leftsBall)
            {
                gm.UpdateLeftScore(1);
            }
            else if (!ballScript.leftsBall)
            {
                gm.UpdateRightScore(1);
            }
        }

        else if (collision.collider.CompareTag("Right Ball"))
        {
            if (ballScriptRighty.rightsBall)
            {
                gm.UpdateRightScore(1);
            }
            else if (!ballScriptRighty.rightsBall)
            {
                gm.UpdateLeftScore(1);
            }
        }

        gm.UpdateNumberOfBricks();

        Destroy(gameObject);

    }

}
