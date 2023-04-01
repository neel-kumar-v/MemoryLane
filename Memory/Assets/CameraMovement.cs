using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Waypoint[] waypointsArray;
    private int currentWaypoint = 0;
    private bool start = false;
    public GameObject parent;

    private void Update()
    {
        MoveToWaypoint();
    }

    private void Start() {
        StartCoroutine(LateStart( .5f));
        waypointsArray = FindWavepointsInOrder(parent);
    }
    
    public Waypoint[] FindWavepointsInOrder(GameObject parent)
    {
        // Create a list to store the wavepoints
        List<Waypoint> waypoints = new List<Waypoint>();

        // Get all the children of the parent GameObject
        Transform[] children = parent.GetComponentsInChildren<Transform>();

        // Loop through all the children
        foreach (Transform child in children)
        {
            // Check if the child has a Wavepoint component
            Waypoint waypoint = child.GetComponent<Waypoint>();

            // If the child has a Wavepoint component, add it to the list
            if (waypoint != null)
            {
                waypoints.Add(waypoint);
            }
        }

        // Return the array of wavepoints
        return waypoints.ToArray();
    }

    IEnumerator LateStart(float seconds) {
        yield return new WaitForSeconds(seconds);
        start = true;
    }

    private void MoveToWaypoint()
    {
        if (currentWaypoint >= waypointsArray.Length || !start)
        {
            return;
        }


        Waypoint waypoint = waypointsArray[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * waypoint.speed);

        Debug.Log(waypoint.gameObject.name);
        if (waypoint.turnDuringTravel) {
            transform.rotation = Quaternion.Slerp(transform.rotation, waypoint.rotation, Time.deltaTime * waypoint.speed / 4);
        }
        else if (Vector3.Distance(transform.position, waypoint.transform.position) < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, waypoint.rotation, Time.deltaTime * waypoint.speed);
        }
        if (transform.position == waypoint.transform.position && 
            (Mathf.Abs(transform.rotation.y - waypoint.rotation.y) < 0.2f))
        {
            Debug.Log("changed: " + currentWaypoint.ToString());
            currentWaypoint++;
            waypoint.events?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}