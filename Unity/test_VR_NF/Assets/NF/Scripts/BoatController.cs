using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
     GameObject[] waypoints;

     public int currentWayPoint = 0;
     Transform targetWayPoint;

     public float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        //tempWaypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        waypoints = new GameObject[6];

        foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            int pos = Int32.Parse(waypoint.gameObject.name.Replace("Point", ""));
            waypoints[pos - 1] = waypoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWayPoint < this.waypoints.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = waypoints[currentWayPoint].transform;
            walk();
        }
    }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = waypoints[currentWayPoint].transform;
        }
    }
}
