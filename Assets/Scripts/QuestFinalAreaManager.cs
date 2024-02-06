using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFinalAreaManager : MonoBehaviour
{
    public static QuestFinalAreaManager Instance { get; private set; }

    private bool triggerArea1Occupied = false;
    private bool triggerArea2Occupied = false;
    private GameObject tutorialUICompletion; // Reference to the UI canvas

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTutorialUICompletion(GameObject tutorialUI)
    {
        tutorialUICompletion = tutorialUI;
    }

    public bool AreBothAreasOccupied()
    {
        return triggerArea1Occupied && triggerArea2Occupied;
    }

    public void SetTriggerAreaOccupied(int areaNumber, bool occupied)
    {
        if (areaNumber == 1)
        {
            triggerArea1Occupied = occupied;
        }
        else if (areaNumber == 2)
        {
            triggerArea2Occupied = occupied;
        }

        if (AreBothAreasOccupied())
        {
            // Both areas are occupied, spawn the UI
            SpawnGoodJobUI();
        }
    }

    private void SpawnGoodJobUI()
    {
        if (tutorialUICompletion != null)
        {
            tutorialUICompletion.SetActive(true);
            Debug.Log("Spawning GoodJob UI");
        }
        else
        {
            Debug.LogError("Tutorial UI Canvas reference is not set!");
        }
    }
}
