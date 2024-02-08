using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            // Trigger the completion coroutine
            StartCoroutine(CompleteTutorialCoroutine());
        }
        else
        {
            Debug.LogError("Tutorial UI Canvas reference is not set!");
        }
    }

    private IEnumerator CompleteTutorialCoroutine()
    {
        // Wait for a few moments before transitioning back to the main menu scene
        yield return new WaitForSeconds(3f);

        // Transition back to the main menu scene
        SceneManager.LoadScene("MainMenu");

        // Update the achievement for completing the tutorial
        UpdateAchievements();
    }

    private void UpdateAchievements()
    {
        // Find the AchievementManager instance in the scene
        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();

        if (achievementManager != null)
        {
            // Loop through the achievements to find the "Master of Basics" achievement
            foreach (var achievement in achievementManager.achievements)
            {
                if (achievement.name == "Master of Basics")
                {
                    // Update the achievement progress for "Master of Basics"
                    achievement.isCompleted = true;

                    // Call the method in the AchievementManager to handle the persistent data update
                    achievementManager.OnPersistentDataUpdated();

                    // Log the achievement completion
                    Debug.Log("Master of Basics achievement updated.");
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("AchievementManager not found in the scene.");
        }
    }
}
