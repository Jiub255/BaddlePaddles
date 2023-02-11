using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRighty : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        Vector3 movement = new Vector3(0f, Input.GetAxis("Vertical2"), 0f);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        rb.MovePosition(transform.position + movement * Time.deltaTime * speed);
    }
}
