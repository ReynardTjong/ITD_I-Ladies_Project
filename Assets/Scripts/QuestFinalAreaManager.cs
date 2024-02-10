using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestFinalAreaManager : MonoBehaviour
{
    public static QuestFinalAreaManager Instance { get; private set; }

    private bool triggerArea1Occupied = false;
    private bool triggerArea2Occupied = false;
    private GameObject tutorialUICompletion;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
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
            SpawnGoodJobUI();
        }
    }

    private void SpawnGoodJobUI()
    {
        if (tutorialUICompletion != null)
        {
            tutorialUICompletion.SetActive(true);
            StartCoroutine(CompleteTutorialCoroutine());
        }
        else
        {
            Debug.LogError("Tutorial UI Canvas reference is not set!");
        }
    }

    private IEnumerator CompleteTutorialCoroutine()
    {
        yield return new WaitForSeconds(3f);

        // Check if the tutorial has been completed
        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 0)
        {
            // Tutorial not completed, don't unlock achievement
            Debug.LogWarning("Tutorial not completed yet, cannot unlock achievement.");
            yield break;
        }

        // Tutorial completed, proceed to unlock achievement and save progress
        SceneManager.LoadScene("MainMenu");
        AchievementManager.instance.UnlockAchievement("MasterOfBasics");
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        PlayerPrefs.Save();

        UpdateFirebaseData();
    }

    private void UpdateFirebaseData()
    {
        if (FirebaseManager.instance != null && FirebaseManager.instance.User != null)
        {
            string userId = FirebaseManager.instance.User.UserId;

            // Update player data in Firebase database
            FirebaseManager.instance.UpdatePlayerDataInDatabase(userId);
        }
    }
}
