using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFinalTaskTrigger : MonoBehaviour
{
    [Header("Tutorial UI Completion")]
    [SerializeField] private GameObject firstTaskCompletionCanvas;

    private void Start()
    {
        firstTaskCompletionCanvas.SetActive(false);
        CampaignChapter1Manager.Instance.SetTutorialUICompletion(firstTaskCompletionCanvas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabbableObject"))
        {
            QuestFinalAreaManager.Instance.SetTriggerAreaOccupied(transform.GetSiblingIndex() + 1, true);
        }
    }
}
