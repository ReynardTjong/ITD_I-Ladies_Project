using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string achievementName;
        public bool isUnlocked;
        public GameObject lockedImage;
        public GameObject unlockedImage;
    }

    public List<Achievement> achievements;

    // Singleton instance
    public static AchievementManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeAchievements();
    }

    void InitializeAchievements()
    {
        foreach (Achievement achievement in achievements)
        {
            if (!PlayerPrefs.HasKey(achievement.achievementName))
            {
                PlayerPrefs.SetInt(achievement.achievementName, achievement.isUnlocked ? 1 : 0);
            }

            int achievementUnlocked = PlayerPrefs.GetInt(achievement.achievementName);
            achievement.isUnlocked = achievementUnlocked == 1;

            if (achievement.isUnlocked)
            {
                achievement.unlockedImage.SetActive(true);
                achievement.lockedImage.SetActive(false);
            }
            else
            {
                achievement.lockedImage.SetActive(true);
                achievement.unlockedImage.SetActive(false);
            }
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        Achievement achievement = achievements.Find(a => a.achievementName == achievementName);
        if (achievement != null)
        {
            achievement.isUnlocked = true;
            PlayerPrefs.SetInt(achievementName, 1);
            achievement.lockedImage.SetActive(false);
            achievement.unlockedImage.SetActive(true);
        }
    }
}