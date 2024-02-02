using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    // Example: persistent data
    public bool achievement1Unlocked;

    // Singleton pattern for PersistentManager
    public static PersistentManager Instance { get; private set; }

    // Reference to the AchievementManager (no need to make it public anymore)
    private AchievementManager achievementManager;

    void Awake()
    {
        // Singleton pattern to ensure only one instance of PersistentManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // Destroy duplicate instances in the same scene
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find the AchievementManager in the scene
        achievementManager = FindObjectOfType<AchievementManager>();

        if (achievementManager == null)
        {
            Debug.LogError("AchievementManager not found in the scene.");
        }
    }

    void Update()
    {
        // Check the AchievementManager for updates
        if (achievementManager != null)
        {
            // Call a method in AchievementManager to inform about the persistent data update
            achievementManager.OnPersistentDataUpdated();

            // Add a debug log to check if the Update method is being called
            Debug.Log("PersistentManager Update called.");
        }
    }
}
