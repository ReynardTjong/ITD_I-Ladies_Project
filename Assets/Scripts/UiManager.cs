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

    [Header("Main Menu")]
    public GameObject mainMenu;

    [Header("Play Menu")]
    public GameObject playContents;
    public GameObject returnToPlayContents;

    [Header("Play")]
    public GameObject campaignBtn;
    public GameObject freeRoamBtn;
    public GameObject tutorialBtn;

    [Header("Play (Campaign)")]
    public GameObject missionContents;
    public GameObject missionBtns;
    public GameObject mission1Contents;
    public GameObject mission2Contents;
    public GameObject mission3Contents;
    public GameObject mission1ReturnBtn;
    public GameObject mission2ReturnBtn;
    public GameObject mission3ReturnBtn;

    [Header("Play (FreeRoam)")]
    public GameObject freeRoamContents;
    public GameObject freeRoamReturnBtn;

    [Header("Play (Tutorial)")]
    public GameObject tutorialContents;
    public GameObject tutorialReturnBtn;

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

    public void PlayContentsScreen()
    {
        playContents.SetActive(true);
    }

    public void BackToPlayContentsScreen()
    {
        campaignBtn.SetActive(true);
        freeRoamBtn.SetActive(true);
        tutorialBtn.SetActive(true);

        missionContents.SetActive(false);
        freeRoamContents.SetActive(false);
        tutorialContents.SetActive(false);
    }

    public void CampaignMissionsScreen()
    {
        campaignBtn.SetActive(false);
        freeRoamBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        missionContents.SetActive(true);
    }

    public void BackToCampaignMissionsScreen()
    {
        missionBtns.SetActive(true);
        returnToPlayContents.SetActive(true);

        mission1Contents.SetActive(false);
        mission2Contents.SetActive(false);
        mission3Contents.SetActive(false);

        mission1ReturnBtn.SetActive(false);
        mission2ReturnBtn.SetActive(false);
        mission3ReturnBtn.SetActive(false);
    }

    public void Mission1Screen()
    {
        missionBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        mission1Contents.SetActive(true);
        mission1ReturnBtn.SetActive(true);
    }

    public void Mission2Screen()
    {
        missionBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        mission2Contents.SetActive(true);
        mission2ReturnBtn.SetActive(true);
    }

    public void Mission3Screen()
    {
        missionBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        mission3Contents.SetActive(true);
        mission3ReturnBtn.SetActive(true);
    }

    public void FreeRoamScreen()
    {
        campaignBtn.SetActive(false);
        freeRoamBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        freeRoamContents.SetActive(true);
        freeRoamReturnBtn.SetActive(true);
    }

    public void TutorialScreen()
    {
        campaignBtn.SetActive(false);
        freeRoamBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        tutorialContents.SetActive(true);
        tutorialReturnBtn.SetActive(true);
    }
}
