using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public UnityEvent pausedEvent;
    public UnityEvent unpausedEvent;
    public void Toggle() {
        // paused is still set to old state here
        //! Ex: Unpaused --> Paused - False --> True | comments on lines 29-32 show result of example
        if (paused) {
            unpausedEvent.Invoke();
        }
        else {
            pausedEvent.Invoke();
        }
        // float muffled = (paused ? 20000f : 600f);
        // mainMixer.SetFloat("Muffled", 500f);
        Time.timeScale = paused ? 1f : 0f;  // Time is stopped
        // now set paused to new state
        paused = !paused;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ 
            Toggle();
        } 
    }
}
