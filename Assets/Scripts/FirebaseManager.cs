using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public DatabaseReference playerReference;

    //Login variables
    [Header("Login")]
    [SerializeField] private TMP_InputField emailLoginField;
    [SerializeField] private TMP_InputField passwordLoginField;
    [SerializeField] private TMP_Text warningLoginText;
    [SerializeField] private TMP_Text confirmLoginText;

    //SignUp variables
    [Header("SignUp")]
    [SerializeField] private TMP_InputField usernameSignUpField;
    [SerializeField] private TMP_InputField emailSignUpField;
    [SerializeField] private TMP_InputField passwordSignUpField;
    [SerializeField] private TMP_InputField passwordSignUpConfirmField;
    [SerializeField] private TMP_Text warningSignUpText;

    [Header("Status")]
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text chaptersCompletedText;
    [SerializeField] private TMP_Text achievementsAchievedText;
    [SerializeField] private TMP_Text booksUnlockedText;
    [SerializeField] private TMP_Text gardenAreasUnlockedText;

    void Awake()
    {

        instance = this;

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        playerReference = FirebaseDatabase.DefaultInstance.GetReference("playerData");
    }

    //Function for the login button
    public async void LoginButton()
    {
        //Call the login coroutine passing the email and password
        await Login(emailLoginField.text, passwordLoginField.text);
    }
    //Function for the register button
    public async void SignUpButton()
    {
        //Call the register coroutine passing the email, password, and username
        await SignUp(emailSignUpField.text, passwordSignUpField.text, usernameSignUpField.text);
    }

    public void LogoutButton()
    {
        auth.SignOut();
        UiManager.instance.LoginScreen();
        ResetInputFields();
    }

    public void ResetInputFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
        usernameSignUpField.text = "";
        emailSignUpField.text = "";
        passwordSignUpField.text = "";
        passwordSignUpConfirmField.text = "";
    }

    public void CreateNewPlayer(DatabaseReference DBreference, string playerUsername, int chaptersCompleted, int achievementsAchieved, int booksUnlocked, int gardenAreasUnlocked)
    {
        Player p = new Player(playerUsername, chaptersCompleted, achievementsAchieved, booksUnlocked, gardenAreasUnlocked);

        var playerPath = DBreference.Push();
        
        playerPath.SetRawJsonValueAsync(JsonUtility.ToJson(p)); 
    }

    private async Task Login(string email, string password)
    {
        try
        {
            var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            await loginTask;

            User = loginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In Successfully!";

            // Get player data from database (if applicable)
            DatabaseReference playerDataRef = playerReference.Child(User.UserId);
            DataSnapshot playerDataSnapshot = await playerDataRef.GetValueAsync();
            Player player = playerDataSnapshot.Value as Player; // Use retrieved player data

            UiManager.instance.MainMenuScreen();
        }
        catch (Exception ex)
        {
            HandleLoginError(ex);
        }
    }

    public async Task SignUp(string email, string password, string username)
    {
        if (username == "")
        {
            warningSignUpText.text = "Username Missing!";
            return;
        }
        else if (passwordSignUpField.text != passwordSignUpConfirmField.text)
        {
            warningSignUpText.text = "Password Does Not Match!";
            return;
        }
        else
        {
            try
            {
                var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
                await registerTask;

                User = registerTask.Result.User;

                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = username };
                    await User.UpdateUserProfileAsync(profile);

                    // Set player data structure with initial values
                    DatabaseReference playerDataRef = DBreference.Child("Players").Child(User.UserId);

                    // Set child nodes for user data
                    await playerDataRef.Child("Username").SetValueAsync(username);
                    await playerDataRef.Child("ChaptersCompleted").SetValueAsync(0);
                    await playerDataRef.Child("AchievementsAchieved").SetValueAsync(0);
                    await playerDataRef.Child("BooksUnlocked").SetValueAsync(0);
                    await playerDataRef.Child("GardenAreasUnlocked").SetValueAsync(0);

                    Debug.Log($"Player data saved for user: {User.UserId}");
                    UiManager.instance.LoginScreen();

                    warningSignUpText.text = "";
                    ResetInputFields();
                    return;
                }
            }
            catch (Exception ex)
            {
                HandleSignUpError(ex);
            }
        }
    }

    // Error handling functions (replace with your actual error handling logic)
    private void HandleLoginError(Exception ex)
    {
        Debug.LogWarning(message: $"Failed to register task with {ex}");
        FirebaseException firebaseEx = ex.GetBaseException() as FirebaseException;
        if (firebaseEx != null)
        {
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Email Missing!";
                    break;
                case AuthError.MissingPassword:
                    message = "Password Missing!";
                    break;
                case AuthError.WrongPassword:
                    message = "Password Incorrect!";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email!";
                    break;
                case AuthError.UserNotFound:
                    message = "Account Does Not Exist!";
                    break;
                default:
                    Debug.LogError($"Login error: {firebaseEx.Message}");
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            // Handle other general errors
            warningLoginText.text = "An error occurred. Please try again later.";
            Debug.LogError($"Login error: {ex.Message}");
        }
    }

    private void HandleSignUpError(Exception ex)
    {
        Debug.LogWarning(message: $"Failed to register task with {ex}");
        FirebaseException firebaseEx = ex.GetBaseException() as FirebaseException;
        if (firebaseEx != null)
        {
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "SignUp Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Email Missing!";
                    break;
                case AuthError.MissingPassword:
                    message = "Password Missing!";
                    break;
                case AuthError.WeakPassword:
                    message = "Password Is Weak!";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use!";
                    break;
                default:
                    Debug.LogError($"Signup error: {firebaseEx.Message}");
                    break;
            }
            warningSignUpText.text = message;
        }
        else
        {
            // Handle other general errors
            warningSignUpText.text = "An error occurred. Please try again later.";
            Debug.LogError($"Signup error: {ex.Message}");
        }
    }

    // Function to update UI with player data
    public async Task UpdatePlayerDataUI(string userId)
    {
        try
        {
            // Get player data from database
            DatabaseReference playerDataRef = FirebaseDatabase.DefaultInstance
                .GetReference("Players").Child(userId);

            DataSnapshot snapshot = await playerDataRef.GetValueAsync();

            if (snapshot.Exists)
            {
                // Deserialize the player data from the snapshot
                Player player = new Player();
                IDictionary<string, object> dictPlayer = (IDictionary<string, object>)snapshot.Value;

                if (dictPlayer.ContainsKey("Username"))
                {
                    player.playerUsername = dictPlayer["Username"].ToString();
                }

                if (dictPlayer.ContainsKey("ChaptersCompleted"))
                {
                    player.chaptersCompleted = Convert.ToInt32(dictPlayer["ChaptersCompleted"]);
                }

                if (dictPlayer.ContainsKey("AchievementsAchieved"))
                {
                    player.chaptersCompleted = Convert.ToInt32(dictPlayer["AchievementsAchieved"]);
                }

                if (dictPlayer.ContainsKey("BooksUnlocked"))
                {
                    player.chaptersCompleted = Convert.ToInt32(dictPlayer["BooksUnlocked"]);
                }

                if (dictPlayer.ContainsKey("GardenAreasUnlocked"))
                {
                    player.chaptersCompleted = Convert.ToInt32(dictPlayer["GardenAreasUnlocked"]);
                }

                // Update UI text fields with player data
                usernameText.text = player.playerUsername;
                chaptersCompletedText.text = player.chaptersCompleted.ToString();
                achievementsAchievedText.text = player.achievementsAchieved.ToString();
                booksUnlockedText.text = player.booksUnlocked.ToString();
                gardenAreasUnlockedText.text = player.gardenAreasUnlocked.ToString();
                // Update other UI elements similarly
            }
            else
            {
                Debug.LogWarning("Player data not found in the database for user ID: " + userId);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error fetching player data: " + ex.Message);
        }
    }
}