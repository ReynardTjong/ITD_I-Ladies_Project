using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string name;
        public Func<bool> isAchieved;
        public bool isCompleted;
    }

    public List<Achievement> achievements = new List<Achievement>();
    private PersistentManager persistentManager; // No need to make it public anymore

    // Ensure that only one instance of AchievementManager exists
    private static AchievementManager _instance;

    void Awake()
    {
        Debug.Log("AchievementManager Awake");
    }

    void Start()
    {
        // Ensure only one instance of AchievementManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes

            Debug.Log("AchievementManager Started.");

            // Add at least one achievement to the list
            achievements.Add(new Achievement
            {
                name = "Clicker Pro",
                isAchieved = () => false,
                isCompleted = false
            });
            // Add more achievements as needed

            // Find the PersistentManager in the scene
            persistentManager = FindObjectOfType<PersistentManager>();

            if (persistentManager == null)
            {
                Debug.LogError("PersistentManager not found in the scene.");
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perform any scene-specific initialization here if needed
    }

    void Update()
    {
        if (achievements == null)
        {
            Debug.LogError("Achievements list is not initialized. Make sure to add achievements in the Unity Editor or via script.");
            return;
        }

        foreach (var achievement in achievements)
        {
            if (achievement == null)
            {
                Debug.LogError("Achievement object is null. Check the Unity Editor to ensure all achievements are properly configured.");
                continue;
            }

            if (!achievement.isCompleted && achievement.isAchieved != null && achievement.isAchieved())
            {
                achievement.isCompleted = true;

                if (persistentManager == null)
                {
                    Debug.LogError("PersistentManager is not assigned. Make sure to assign it in the Unity Editor.");
                    return;
                }

                // Update persistent data
                UpdatePersistentData(achievement.name);

                // Do something when the achievement is unlocked, e.g., show a notification
                Debug.Log("Achievement Unlocked: " + achievement.name);
                Debug.Log("AchievementManager Update called.");
            }
        }
    }

    public void OnPersistentDataUpdated()
    {
        // Handle any logic you need when persistent data is updated
        // This can include re-evaluating achievement progress, etc.
        // For example, you might want to call Update() or a specific method in AchievementManager.
        Update();
    }

    void UpdatePersistentData(string achievementName)
    {
        Debug.Log("Updating Persistent Data for Achievement: " + achievementName);

        if (persistentManager != null)
        {
            // Update persistent data based on the unlocked achievement
            switch (achievementName)
            {
                case "Clicker Pro":
                    persistentManager.achievement1Unlocked = true;
                    // Add more cases for other achievements as needed
                    break;
                default:
                    Debug.LogWarning("Unexpected achievement name: " + achievementName);
                    break;
            }
        }
        else
        {
            Debug.LogError("PersistentManager is not assigned. Make sure to assign it in the Unity Editor.");
        }
    }
}
