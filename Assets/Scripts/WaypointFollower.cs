using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    //private Rigidbody2D rb;

    public Transform[] waypoints;
    [SerializeField] private int currentWaypointIndex = 0;

    public float speed = 5f;

    [SerializeField] private Vector2 currentPos;
    [SerializeField] private Vector2 nextWaypointPos;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.001f)
        {
            currentWaypointIndex++;
            
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        //get vector2's here, move in fixed update
        currentPos = transform.position;
        nextWaypointPos = waypoints[currentWaypointIndex].transform.position;

        transform.position = Vector2.MoveTowards(transform.position, nextWaypointPos, speed * Time.deltaTime);
    }

    //private void FixedUpdate()
   // {
        //rb.MovePosition(currentPos + nextWaypointPos * speed * Time.fixedDeltaTime);
   // }

}
