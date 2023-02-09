using System;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public float speed = 10.0f;
    public bool start = false;
    public bool audioPlayed = false;
    public AudioSource src;
    public AudioSource src1;
    public void MoveForward()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    public void ChangeStart() {
        start = true;
    }

    public void Update() {
        if (!start) return;
        if (!audioPlayed) {
            src.Play();
            src1.mute = true;
            audioPlayed = true;
        }
        MoveForward();
    }
}