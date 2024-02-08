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
    private PersistentManager persistentManager;

    public static AchievementManager Instance;

    void Awake()
    {
        Debug.Log("AchievementManager Awake");
    }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("AchievementManager Started.");

            // Add at least one achievement to the list
            achievements.Add(new Achievement
            {
                name = "Master of Basics",
                isAchieved = () => false,
                isCompleted = false
            });

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

    public void UnlockAchievement(string achievementName)
    {
        Debug.Log("Unlocking Achievement: " + achievementName);

        // Find the achievement by name
        Achievement achievement = achievements.Find(a => a.name == achievementName);

        if (achievement != null)
        {
            // Mark the achievement as completed
            achievement.isCompleted = true;

            // Update persistent data
            UpdatePersistentData(achievementName);
        }
        else
        {
            Debug.LogWarning("Achievement not found: " + achievementName);
        }
    }

    void UpdatePersistentData(string achievementName)
    {
        Debug.Log("Updating Persistent Data for Achievement: " + achievementName);

        if (persistentManager != null)
        {
            // Update persistent data based on the unlocked achievement
            switch (achievementName)
            {
                case "Master of Basics":
                    persistentManager.achievement1Unlocked = true;
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