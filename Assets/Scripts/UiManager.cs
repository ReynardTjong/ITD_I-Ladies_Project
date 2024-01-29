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

    [Header("Play Buttons")]
    public GameObject campaignBtn;
    public GameObject freeRoamBtn;
    public GameObject tutorialBtn;

    [Header("Play (Campaign)")]
    public GameObject chapterContents;
    public GameObject chapterBtns;
    public GameObject chapter1Contents;
    public GameObject chapter2Contents;
    public GameObject chapter3Contents;
    public GameObject chapter1ReturnBtn;
    public GameObject chapter2ReturnBtn;
    public GameObject chapter3ReturnBtn;

    [Header("Play (FreeRoam)")]
    public GameObject freeRoamContents;
    public GameObject freeRoamReturnBtn;

    [Header("Play (Tutorial)")]
    public GameObject tutorialContents;
    public GameObject tutorialReturnBtn;

    [Header("Achievements Menu")]
    public GameObject achievementsContents;

    [Header("Book Menu")]
    public GameObject bookContents;

    [Header("Book Buttons")]
    public GameObject gardeningBtn;
    public GameObject cookingBtn;

    [Header("Book (Gardening)")]
    public GameObject gardeningContents;
    public GameObject gardeningSkill1;
    public GameObject gardeningSkill2;
    public GameObject gardeningSkill3;
    public GameObject gardeningSkill4;

    [Header("Book (Cooking)")]
    public GameObject cookingContents;
    public GameObject cookingRecipe1;
    public GameObject cookingRecipe2;
    public GameObject cookingRecipe3;
    public GameObject cookingRecipe4;

    [Header("Volume Menu")]
    public GameObject volumeContents;

    [Header("How To Play Menu")]
    public GameObject howToPlayContents;

    [Header("How To Play Buttons")]
    public GameObject instructionsBtn;
    public GameObject vrControllerBindingsBtn;
    public GameObject creditsBtn;

    [Header("How To Play (Instructions)")]
    public GameObject instructionsContents;
    public GameObject instructionsBtns;
    public GameObject campaignInstructionsContents;
    public GameObject freeRoamInstructionsContents;

    [Header("How To Play (VR Controller Bindings)")]
    public GameObject vrControllerBindingsContents;

    [Header("How To Play (Credits)")]
    public GameObject creditsContents;

    [Header("Status Menu")]
    public GameObject statusContents;

    [Header("Logout")]
    public GameObject logoutContents;

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

    #region Login/Sign Up Page Functions
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
    #endregion


    #region Play Menu Functions
    public void PlayContentsScreen()
    {
        playContents.SetActive(true);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }

    public void BackToPlayContentsScreen()
    {
        campaignBtn.SetActive(true);
        freeRoamBtn.SetActive(true);
        tutorialBtn.SetActive(true);

        chapterContents.SetActive(false);
        freeRoamContents.SetActive(false);
        tutorialContents.SetActive(false);
    }

    public void CampaignChaptersScreen()
    {
        campaignBtn.SetActive(false);
        freeRoamBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        chapterContents.SetActive(true);
    }

    public void BackToCampaignMissionsScreen()
    {
        chapterBtns.SetActive(true);
        returnToPlayContents.SetActive(true);

        chapter1Contents.SetActive(false);
        chapter2Contents.SetActive(false);
        chapter3Contents.SetActive(false);

        chapter1ReturnBtn.SetActive(false);
        chapter2ReturnBtn.SetActive(false);
        chapter3ReturnBtn.SetActive(false);
    }

    public void Chapter1Screen()
    {
        chapterBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        chapter1Contents.SetActive(true);
        chapter1ReturnBtn.SetActive(true);
    }

    public void Chapter2Screen()
    {
        chapterBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        chapter2Contents.SetActive(true);
        chapter2ReturnBtn.SetActive(true);
    }

    public void Chapter3Screen()
    {
        chapterBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        chapter3Contents.SetActive(true);
        chapter3ReturnBtn.SetActive(true);
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
    #endregion

    #region Achievements Menu Functions
    public void AchievementsScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(true);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }
    #endregion

    #region Book Menu Functions

    public void BookScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(true);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }

    public void BackToBookScreen()
    {
        gardeningBtn.SetActive(true);
        cookingBtn.SetActive(true);

        gardeningContents.SetActive(false);
        cookingContents.SetActive(false);
    }

    public void GardeningBookScreen()
    {
        gardeningBtn.SetActive(false);
        cookingBtn.SetActive(false);

        gardeningContents.SetActive(true);

        GardeningSkillOneScreen();
    }

    public void GardeningSkillOneScreen()
    {
        gardeningSkill1.SetActive(true);
        gardeningSkill2.SetActive(false);
        gardeningSkill3.SetActive(false);
        gardeningSkill4.SetActive(false);
    }

    public void GardeningSkillTwoScreen()
    {
        gardeningSkill1.SetActive(false);
        gardeningSkill2.SetActive(true);
        gardeningSkill3.SetActive(false);
        gardeningSkill4.SetActive(false);
    }

    public void GardeningSkillThreeScreen()
    {
        gardeningSkill1.SetActive(false);
        gardeningSkill2.SetActive(false);
        gardeningSkill3.SetActive(true);
        gardeningSkill4.SetActive(false);
    }

    public void GardeningSkillFourScreen()
    {
        gardeningSkill1.SetActive(false);
        gardeningSkill2.SetActive(false);
        gardeningSkill3.SetActive(false);
        gardeningSkill4.SetActive(true);
    }

    public void CookingBookScreen()
    {
        gardeningBtn.SetActive(false);
        cookingBtn.SetActive(false);

        cookingContents.SetActive(true);

        CookingRecipeOneScreen();
    }

    public void CookingRecipeOneScreen()
    {
        cookingRecipe1.SetActive(true);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(false);
    }

    public void CookingRecipeTwoScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(true);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(false);
    }

    public void CookingRecipeThreeScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(true);
        cookingRecipe4.SetActive(false);
    }

    public void CookingRecipeFourScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(true);
    }
    #endregion

    #region Volume Menu Functions
    public void VolumeScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(true);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }
    #endregion

    #region How To Play Menu Functions
    public void HowToPlayScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(true);
        statusContents.SetActive(false);
    }

    public void BackToHowToPlayScreen()
    {
        instructionsBtn.SetActive(true);
        vrControllerBindingsBtn.SetActive(true);
        creditsBtn.SetActive(true);

        instructionsContents.SetActive(false);
        vrControllerBindingsContents.SetActive(false);
        creditsContents.SetActive(false);
    }

    public void InstructionsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        instructionsContents.SetActive(true);
    }

    public void BackToInstructionsScreen()
    {
        instructionsBtns.SetActive(true);

        campaignInstructionsContents.SetActive(false);
        freeRoamInstructionsContents.SetActive(false);
    }

    public void CampaignInstructionsScreen()
    {
        instructionsBtns.SetActive(false);

        campaignInstructionsContents.SetActive(true);
    }

    public void FreeRoamInstructionsScreen()
    {
        instructionsBtns.SetActive(false);

        freeRoamInstructionsContents.SetActive(true);
    }

    public void VRControllerBindingsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        vrControllerBindingsContents.SetActive(true);
    }

    public void CreditsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        creditsContents.SetActive(true);
    }
    #endregion

    #region Status Menu Functions
    public void StatusScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(true);
    }
    #endregion

    #region Logout Menu Functions
    public void LogoutScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
        logoutContents.SetActive(true);
    }

    public void DisableLogoutScreen()
    {
        logoutContents.SetActive(false);
    }
    #endregion
}
