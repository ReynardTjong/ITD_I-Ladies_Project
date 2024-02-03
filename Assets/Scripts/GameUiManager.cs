using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager instance;

    [Header("Firebase Script Reference")]
    public FirebaseManager firebaseManager;

    [Header("Game Menu")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameMenuBtn;
    [SerializeField] private GameObject gameMenuContents;
    [SerializeField] private GameObject gameMenuContentsBG;
    [SerializeField] private GameObject gameMenuBtns;

    [Header("Inventory Menu")]
    [SerializeField] private GameObject inventoryScreen;

    [Header("Quit Menu")]
    [SerializeField] private GameObject quitGame;

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

    public void BackToGameMenuScreen()
    {
        gameMenuBtn.SetActive(true);
        gameMenuContents.SetActive(false);
    }

    public void ShowGameMenuContents()
    {
        gameMenuBtn.SetActive(false);
        gameMenuContents.SetActive(true);
    }

    public void BackToGameMenuContents()
    {
        gameMenuContentsBG.SetActive(true);
        gameMenuBtns.SetActive(true);
        inventoryScreen.SetActive(false);
        quitGame.SetActive(false);
    }

    public void ShowInventoryScreen()
    {
        gameMenuContentsBG.SetActive(false);
        gameMenuBtns.SetActive(false);
        inventoryScreen.SetActive(true);
        quitGame.SetActive(false);
    }

    public void ShowQuitScreen()
    {
        gameMenuContentsBG.SetActive(true);
        gameMenuBtns.SetActive(false);
        inventoryScreen.SetActive(false);
        quitGame.SetActive(true);
    }
}
