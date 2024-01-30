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
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public DatabaseReference playerReference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //SignUp variables
    [Header("SignUp")]
    public TMP_InputField usernameSignUpField;
    public TMP_InputField emailSignUpField;
    public TMP_InputField passwordSignUpField;
    public TMP_InputField passwordSignUpConfirmField;
    public TMP_Text warningSignUpText;

    void Awake()
    {
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

    public void CreateNewPlayer(DatabaseReference DBreference, string playerUsername, int chaptersCompleted, int achievementsAcquired, int booksUnlocked, int gardenAreasUnlocked)
    {
        Player p = new Player(playerUsername, chaptersCompleted, achievementsAcquired, booksUnlocked, gardenAreasUnlocked);

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

    private async Task SignUp(string email, string password, string username)
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

                    // Create player data structure with initial values
                    Player newPlayer = new Player(username, 0, 0, 0, 0); // Adjust initial values as needed

                    DatabaseReference playerDataRef = DBreference.Child("playerData").Child(User.UserId);

                    await playerDataRef.Child("Username").SetValueAsync(username);
                    await playerDataRef.Child("ChaptersCompleted").SetValueAsync(newPlayer.chaptersCompleted);
                    await playerDataRef.Child("AchievementsAcquired").SetValueAsync(newPlayer.achievementsAcquired);
                    await playerDataRef.Child("BooksUnlocked").SetValueAsync(newPlayer.booksUnlocked);
                    await playerDataRef.Child("GardenAreasUnlocked").SetValueAsync(newPlayer.gardenAreasUnlocked);

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
}