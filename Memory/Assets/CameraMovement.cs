using System;
using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Waypoint[] waypoints;
    private int currentWaypoint = 0;
    private bool start = false;

    private void Update()
    {
        MoveToWaypoint();
    }

    private void Start() {
        StartCoroutine(LateStart(1.5f));
    }

    IEnumerator LateStart(float seconds) {
        yield return new WaitForSeconds(seconds);
        start = true;
    }

    private void MoveToWaypoint()
    {
        if (currentWaypoint >= waypoints.Length || !start)
        {
            return;
        }


        Waypoint waypoint = waypoints[currentWaypoint];
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