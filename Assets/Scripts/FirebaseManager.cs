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

///<summary>
/// Manages Firebase functionality in the game.
///</summary>
public class FirebaseManager : MonoBehaviour
{
    ///<summary>
    /// Singleton instance of the FirebaseManager.
    ///</summary>
    public static FirebaseManager instance;

    ///<summary>
    /// Status of Firebase dependencies.
    ///</summary>
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;

    ///<summary>
    /// Firebase authentication instance.
    ///</summary>
    public FirebaseAuth auth;

    ///<summary>
    /// Current Firebase user.
    ///</summary>
    public FirebaseUser User;

    ///<summary>
    /// Reference to the Firebase database.
    ///</summary>
    public DatabaseReference DBreference;

    ///<summary>
    /// Reference to the player data in the Firebase database.
    ///</summary>
    public DatabaseReference playerReference;

    ///<summary>
    /// Input field for email during login.
    ///</summary>
    [Header("Login")]
    [SerializeField] private TMP_InputField emailLoginField;

    ///<summary>
    /// Input field for password during login.
    ///</summary>
    [SerializeField] private TMP_InputField passwordLoginField;

    ///<summary>
    /// Text for displaying login warnings.
    ///</summary>
    [SerializeField] private TMP_Text warningLoginText;

    ///<summary>
    /// Text for confirming successful login.
    ///</summary>
    [SerializeField] private TMP_Text confirmLoginText;

    ///<summary>
    /// Input field for username during signup.
    ///</summary>
    [Header("SignUp")]
    [SerializeField] private TMP_InputField usernameSignUpField;

    ///<summary>
    /// Input field for email during signup.
    ///</summary>
    [SerializeField] private TMP_InputField emailSignUpField;

    ///<summary>
    /// Input field for password during signup.
    ///</summary>
    [SerializeField] private TMP_InputField passwordSignUpField;

    ///<summary>
    /// Input field for confirming password during signup.
    ///</summary>
    [SerializeField] private TMP_InputField passwordSignUpConfirmField;

    ///<summary>
    /// Text for displaying signup warnings.
    ///</summary>
    [SerializeField] private TMP_Text warningSignUpText;

    ///<summary>
    /// Text displaying player username.
    ///</summary>
    [Header("Status")]
    [SerializeField] private TMP_Text usernameText;

    ///<summary>
    /// Text displaying chapters completed.
    ///</summary>
    [SerializeField] private TMP_Text chaptersCompletedText;

    ///<summary>
    /// Text displaying achievements achieved.
    ///</summary>
    [SerializeField] private TMP_Text achievementsAchievedText;

    ///<summary>
    /// Text displaying books unlocked.
    ///</summary>
    [SerializeField] private TMP_Text booksUnlockedText;

    ///<summary>
    /// Text displaying garden areas unlocked.
    ///</summary>
    [SerializeField] private TMP_Text gardenAreasUnlockedText;

    ///<summary>
    /// Called when the script instance is being loaded.
    ///</summary>
    void Awake()
    {
        instance = this;

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are available, initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    ///<summary>
    /// Initializes Firebase components.
    ///</summary>
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        playerReference = FirebaseDatabase.DefaultInstance.GetReference("playerData");
    }

    ///<summary>
    /// Handles the login process.
    ///</summary>
    public async void LoginButton()
    {
        //Call the login coroutine passing the email and password
        await Login(emailLoginField.text, passwordLoginField.text);
    }

    ///<summary>
    /// Handles the signup process.
    ///</summary>
    public async void SignUpButton()
    {
        //Call the register coroutine passing the email, password, and username
        await SignUp(emailSignUpField.text, passwordSignUpField.text, usernameSignUpField.text);
    }

    ///<summary>
    /// Logs out the current user.
    ///</summary>
    public void LogoutButton()
    {
        auth.SignOut();
        UiManager.instance.LoginScreen();
        ResetInputFields();
    }

    ///<summary>
    /// Resets all input fields.
    ///</summary>
    public void ResetInputFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
        usernameSignUpField.text = "";
        emailSignUpField.text = "";
        passwordSignUpField.text = "";
        passwordSignUpConfirmField.text = "";
    }

    ///<summary>
    /// Handles the login process.
    ///</summary>
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

    ///<summary>
    /// Handles the signup process.
    ///</summary>
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

    ///<summary>
    /// Handles login errors.
    ///</summary>
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

    ///<summary>
    /// Handles signup errors.
    ///</summary>
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

    ///<summary>
    /// Updates UI with player data.
    ///</summary>
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
