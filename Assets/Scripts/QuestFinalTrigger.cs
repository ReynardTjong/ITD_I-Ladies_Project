using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinalTrigger : MonoBehaviour
{
    [Header("Tutorial UI Completion")]
    [SerializeField] private GameObject tutorialUICompletion;

    private void Start()
    {
        tutorialUICompletion.SetActive(false);
        QuestFinalAreaManager.Instance.SetTutorialUICompletion(tutorialUICompletion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabbableObject"))
        {
            QuestFinalAreaManager.Instance.SetTriggerAreaOccupied(transform.GetSiblingIndex() + 1, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabbableObject"))
        {
            QuestFinalAreaManager.Instance.SetTriggerAreaOccupied(transform.GetSiblingIndex() + 1, false);
        }
    }
}
