using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("Firebase Script Reference")]
    public FirebaseManager firebaseManager;

    [Header("Login/SignUp")]
    public GameObject loginPage;
    public GameObject signUpPage;

     private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        firebaseManager = GetComponent<FirebaseManager>();
    }

    public void LoginScreen()
    {
        loginPage.SetActive(true);
        signUpPage.SetActive(false);
        firebaseManager.ResetInputFields();
    }

    public void SignUpScreen()
    {
        signUpPage.SetActive(true);
        loginPage.SetActive(false);
        firebaseManager.ResetInputFields();
    }
}
