using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampaignFinalTrigger : MonoBehaviour
{
    [Header("Chapter 1 Completion Canvas")]
    [SerializeField] private GameObject chapter1CompletionCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bell Pepper"))
        {
            chapter1CompletionCanvas.SetActive(true);
            CampaignChapter1Manager.instance.SpawnGoodJobUI();
        }
    }
}
