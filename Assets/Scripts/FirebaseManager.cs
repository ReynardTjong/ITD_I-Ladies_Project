using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
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

    [Header("Status Statistics")]
    public string username;
    public int chaptersCompleted;
    public int achievementsUnlocked;
    public int booksUnlocked;
    public Dictionary<string, string> gardenAreas;

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

    public void LogoutButton()
    {
        auth.SignOut();
        UiManager.instance.LoginScreen();
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

            // **Retrieve user data**
            yield return StartCoroutine(GetUserdata(User.UserId));

            // After receiving user data:
            UiManager.instance.UpdateUsernameText(username);
            UiManager.instance.UpdateChaptersCompletedText(chaptersCompleted, 3);
            UiManager.instance.UpdateAchievementsUnlockedText(achievementsUnlocked, 8);
            UiManager.instance.UpdateBooksUnlockedText(booksUnlocked, 6);
            UiManager.instance.UpdateGardenAreasText(gardenAreas["area1"], gardenAreas["area2"]);

            UiManager.instance.MainMenuScreen();
        }
    }

    private IEnumerator SignUp(string email, string password, string username)
    {
        if (username == "")
        {
            //If the username field is blank show a warning
            warningSignUpText.text = "Username Missing!";
        }
        else if (passwordSignUpField.text != passwordSignUpConfirmField.text)
        {
            //If the password does not match show a warning
            warningSignUpText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

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
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

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
                        //Username is now set
                        //Now return to login screen
                        UiManager.instance.LoginScreen();
                        warningSignUpText.text = "";
                    }

                    ResetInputFields();
                }
            }
        }
    }

    private IEnumerator GetUserdata(string userId)
    {
        // Get user data reference
        DatabaseReference userDataRef = DBreference.Child($"users/{userId}");

        // Read user data asynchronously
        Task<DataSnapshot> dataSnapshotTask = userDataRef.GetValueAsync();
        yield return dataSnapshotTask;

        // Check for errors
        if (dataSnapshotTask.Exception != null)
        {
            Debug.LogError($"Error reading user data: {dataSnapshotTask.Exception}");
        }
        else
        {
            // Get data snapshot
            DataSnapshot snapshot = dataSnapshotTask.Result;

            // Extract user data values with proper conversion
            username = (string)snapshot.Child("username").GetValue(true);
            chaptersCompleted = int.Parse(snapshot.Child("chaptersCompleted").GetValue(true).ToString());
            achievementsUnlocked = int.Parse(snapshot.Child("achievementsUnlocked").GetValue(true).ToString());
            booksUnlocked = (int)snapshot.Child("booksUnlocked").GetValue(true);
            gardenAreas = (Dictionary<string, string>)snapshot.Child("gardenAreas").GetValue(true); // Assuming 'gardenAreas' is a Dictionary<string, string>

            // Update UI elements in main menu
            UiManager.instance.UpdateUsernameText(username);
            UiManager.instance.UpdateChaptersCompletedText(int.Parse(snapshot.Child("chaptersCompleted").GetValue(true).ToString()), 3); // Modify for your actual chapter count
            UiManager.instance.UpdateAchievementsUnlockedText(int.Parse(snapshot.Child("achievementsUnlocked").GetValue(true).ToString()), 8); // Modify for your actual achievement count
            UiManager.instance.UpdateBooksUnlockedText(booksUnlocked, 6); // Modify for your actual book count
            UiManager.instance.UpdateGardenAreasText(gardenAreas["area1"], gardenAreas["area2"]);
        }
    }
}