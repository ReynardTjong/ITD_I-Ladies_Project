using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager instance;

    [Header("Firebase Script Reference")]
    public FirebaseManager firebaseManager;

    [Header("Game Menu")]
    public GameObject gameMenu;



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
    }

    private void Start()
    {
        firebaseManager = FirebaseManager.instance;
    }
}
