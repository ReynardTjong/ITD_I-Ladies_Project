using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;

public class StatusPageManager : MonoBehaviour
{
    [Header("Status UI Elements")]
    public TMP_Text usernameText;
    public TMP_Text chaptersCompletedText;
    public TMP_Text achievementsAcquiredText;
    public TMP_Text booksUnlockedText;
    public TMP_Text gardenAreasUnlockedText;

    private DatabaseReference playerDataReference;

    void Start()
    {
        // Get a reference to the Firebase database
        playerDataReference = FirebaseDatabase.DefaultInstance.GetReference("playerData");

        // Display user status when the status page is loaded
        DisplayUserStatus();
    }

    private async void DisplayUserStatus()
    {
        // Check if the user is logged in
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

            // Get the reference to the current user's data
            DatabaseReference userReference = playerDataReference.Child(userId);

            // Retrieve user data from the database
            DataSnapshot snapshot = await userReference.GetValueAsync();

            if (snapshot.Exists)
            {
                // Update UI with user data
                usernameText.text = "Username: " + snapshot.Child("Username").Value.ToString();
                chaptersCompletedText.text = "No. Missions Completed: " + snapshot.Child("MissionsCompleted").Value.ToString();
                achievementsAcquiredText.text = "No. Achievements Acquired: " + snapshot.Child("AchievementsAcquired").Value.ToString();
                booksUnlockedText.text = "No. Books Unlocked: " + snapshot.Child("BooksUnlocked").Value.ToString();
                gardenAreasUnlockedText.text = "No. Garden Areas Unlocked: " + snapshot.Child("GardenAreasUnlocked").Value.ToString();
            }
            else
            {
                Debug.LogWarning("User data not found in the database.");
            }
        }
        else
        {
            Debug.LogWarning("User not logged in.");
        }
    }
}