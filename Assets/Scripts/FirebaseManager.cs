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
        playerReference = FirebaseDatabase.DefaultInstance.GetReference("Players");
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void SignUpButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(SignUp(emailSignUpField.text, passwordSignUpField.text, usernameSignUpField.text));
    }

    private void StartCoroutine(Task<IEnumerator> task)
    {
        throw new NotImplementedException();
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

    private IEnumerator Login(string email, string password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
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
                    message = "Account Does Not exist!";
                    break;
            }
            warningLoginText.text = message;

            ResetInputFields();
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In Successfully!";

            UiManager.instance.MainMenuScreen();
        }
    }

    private async Task<IEnumerator> SignUp(string email, string password, string username)
    {
        if (username == "")
        {
            //If the username field is blank show a warning
            warningSignUpText.text = "Username Missing!";
            return (IEnumerator)Task.CompletedTask;
        }
        else if (passwordSignUpField.text != passwordSignUpConfirmField.text)
        {
            //If the password does not match show a warning
            warningSignUpText.text = "Password Does Not Match!";
            return (IEnumerator)Task.CompletedTask;
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
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
                }
                warningSignUpText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);


                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningSignUpText.text = "Username Set Failed!";
                    }
                    else
                    {
                        // Username is now set
                        // Create player data structure with initial values
                        Player newPlayer = new Player(username, 0, 0, 0, 0); // Adjust initial values as needed

                        // Access user-specific player data reference
                        DatabaseReference playerDataRef = DBreference.Child("playerData").Child(User.UserId);

                        await Task.Run(async () =>
                        {
                            // Create the nested data structure with the username as a parent node
                            await playerDataRef.Child("Username").SetValueAsync(username);
                            await playerDataRef.Child("Username/ChaptersCompleted").SetValueAsync(newPlayer.chaptersCompleted);
                            await playerDataRef.Child("Username/AchievementsAcquired").SetValueAsync(newPlayer.achievementsAcquired);
                            await playerDataRef.Child("Username/BooksUnlocked").SetValueAsync(newPlayer.booksUnlocked);
                            await playerDataRef.Child("Username/GardenAreasUnlocked").SetValueAsync(newPlayer.gardenAreasUnlocked);

                            Debug.Log($"Player data saved for user: {User.UserId}");
                            UiManager.instance.LoginScreen();

                            return Task.CompletedTask;
                        });

                        warningSignUpText.text = "";
                        ResetInputFields();
                        return null;
                    }
                }
            }
        }
    }
}