using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the collected object has the tag "chapter1finish"
        if (other.CompareTag("chapter1finish"))
        {
            // Update AchievementManager directly
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();

            if (achievementManager != null)
            {
                // Find the achievement with the specific name
                AchievementManager.Achievement chapter1FinishAchievement = achievementManager.achievements.Find(a => a.name == "Chapter 1 Finish");

                if (chapter1FinishAchievement != null)
                {
                    // Mark the achievement as completed
                    chapter1FinishAchievement.isCompleted = true;

                    // Update persistent data
                    achievementManager.OnPersistentDataUpdated();

                    // Log the achievement unlock
                    Debug.Log("Achievement Unlocked: Chapter 1 Finish");
                }
                else
                {
                    Debug.LogError("Achievement 'Chapter 1 Finish' not found in the AchievementManager.");
                }
            }
            else
            {
                Debug.LogError("AchievementManager not found in the scene.");
            }

            // Log collision with collectible
            Debug.Log("Collided with collectible");

            // Destroy the collected object (optional)
            Destroy(gameObject);
        }
    }
}
