using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinalTrigger : MonoBehaviour
{
    public GameObject uiCanvas; // Reference to your UI Canvas object

    private void Start()
    {
        uiCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Replace with your specific collider tag
        {
            uiCanvas.SetActive(true); // Activate the UI Canvas

            Debug.Log("Tutorial Completed" + " Achievement Unlocked");
        }
    }
}
