using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

///<summary>
/// Manages achievements in the game.
///</summary>
public class AchievementManager : MonoBehaviour
{
    ///<summary>
    /// Represents individual achievements in the game.
    ///</summary>
    [System.Serializable]
    public class Achievement
    {
        ///<summary>
        /// The name of the achievement.
        ///</summary>
        public string achievementName;

        ///<summary>
        /// Indicates if the achievement is unlocked.
        ///</summary>
        public bool isUnlocked;

        ///<summary>
        /// The image displayed when the achievement is locked.
        ///</summary>
        public GameObject lockedImage;

        ///<summary>
        /// The image displayed when the achievement is unlocked.
        ///</summary>
        public GameObject unlockedImage;
    }

    ///<summary>
    /// List of all achievements in the game.
    ///</summary>
    public List<Achievement> achievements;

    ///<summary>
    /// Singleton instance of the AchievementManager.
    ///</summary>
    public static AchievementManager instance;

    ///<summary>
    /// Called when the script instance is being loaded.
    ///</summary>
    private void Start()
    {
        InitializeAchievements();
    }

    ///<summary>
    /// Initializes all achievements.
    ///</summary>
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

    ///<summary>
    /// Unlocks the specified achievement.
    ///</summary>
    ///<param name="achievementName">The name of the achievement to unlock.</param>
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
