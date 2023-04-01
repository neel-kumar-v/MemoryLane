using UnityEngine;
using System.Collections;

public class TorchTurnOff : MonoBehaviour {
    public GameObject parent;
    public AudioSource src;
    public AudioSource src2;
    private Transform[] children;
    public void SetChildrenState(bool active)
    {
        // Get all the children of the parent GameObject
        if(children == null) children = parent.GetComponentsInChildren<Transform>();
        src.Play();
        src2.mute = active;
        // Loop through all the children
        if (children != null) {
            foreach (Transform child in children)
            {
                // Set the state of the child GameObject after waiting 0.1 seconds
                StartCoroutine(SetChildStateWithDelay(child.gameObject, active, 0.1f));
            }
        }
        
    }

    private IEnumerator SetChildStateWithDelay(GameObject child, bool active, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the state of the child GameObject
        child.SetActive(active);
    }
}
