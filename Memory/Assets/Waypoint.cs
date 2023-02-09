using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    public Quaternion rotation;
    public bool turnDuringTravel;
    public UnityEvent events;

    public float speed = 5.0f;


}
