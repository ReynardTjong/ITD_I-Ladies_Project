using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the UI flow and functionality of the game.
/// </summary>
public class UiManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the UiManager.
    /// </summary>
    public static UiManager instance;

    [Header("Firebase Script Reference")]
    public FirebaseManager firebaseManager;

    // Login/SignUp
    public GameObject loginPage;
    public GameObject signUpPage;

    // Main Menu
    public GameObject mainMenu;

    // Play Menu
    public GameObject playContents;
    public GameObject returnToPlayContents;
    public GameObject campaignBtn;
    public GameObject tutorialBtn;

    // Play (Campaign)
    public GameObject chapterContents;
    public GameObject chapterBtns;
    public GameObject chapter1Contents;
    public GameObject chapter2Contents;
    public GameObject chapter1ReturnBtn;
    public GameObject chapter2ReturnBtn;

    // Play (Tutorial)
    public GameObject tutorialContents;
    public GameObject tutorialReturnBtn;

    // Achievements Menu
    public GameObject achievementsContents;

    // Book Menu
    public GameObject bookContents;
    public GameObject gardeningBtn;
    public GameObject cookingBtn;
    public GameObject gardeningContents;
    public GameObject gardeningSkill1;
    public GameObject gardeningSkill2;
    public GameObject gardeningSkill3;
    public GameObject gardeningSkill4;
    public GameObject cookingContents;
    public GameObject cookingRecipe1;
    public GameObject cookingRecipe2;
    public GameObject cookingRecipe3;
    public GameObject cookingRecipe4;

    // Volume Menu
    public GameObject volumeContents;

    // How To Play Menu
    public GameObject howToPlayContents;
    public GameObject instructionsBtn;
    public GameObject vrControllerBindingsBtn;
    public GameObject creditsBtn;
    public GameObject instructionsBtns;
    public GameObject campaignInstructionsContents;
    public GameObject vrControllerBindingsContents;
    public GameObject creditsContents;

    // Status Menu
    public GameObject statusContents;

    // Logout Menu
    public GameObject logoutContents;

    // Quit Menu
    public GameObject quitBtn;
    public GameObject quitContents;

    /// <summary>
    /// Initializes the UiManager instance.
    /// </summary>
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

    /// <summary>
    /// Sets up the initial state of the UI.
    /// </summary>
    private void Start()
    {
        firebaseManager = FirebaseManager.instance;
        loginPage.SetActive(true);
        mainMenu.SetActive(false);
    }

    #region Login/Sign Up Page Functions

    /// <summary>
    /// Displays the login screen.
    /// </summary>
    public void LoginScreen()
    {
        loginPage.SetActive(true);
        signUpPage.SetActive(false);
        mainMenu.SetActive(false);
        quitBtn.SetActive(true);
        firebaseManager.ResetInputFields();
    }

    /// <summary>
    /// Displays the sign up screen.
    /// </summary>
    public void SignUpScreen()
    {
        signUpPage.SetActive(true);
        loginPage.SetActive(false);
        mainMenu.SetActive(false);
        quitBtn.SetActive(true);
        firebaseManager.ResetInputFields();
    }

    #endregion

    #region Main Menu Functions
    /// <summary>
    /// Displays the main menu screen.
    /// </summary>
    public void MainMenuScreen()
    {
        loginPage.SetActive(false);
        signUpPage.SetActive(false);
        mainMenu.SetActive(true);
        quitBtn.SetActive(false);
    }
    #endregion

    #region Play Menu Functions

    /// <summary>
    /// Displays the play menu contents.
    /// </summary>
    public void PlayContentsScreen()
    {
        playContents.SetActive(true);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }

    /// <summary>
    /// Navigates back to the main play menu contents.
    /// </summary>
    public void BackToPlayContentsScreen()
    {
        campaignBtn.SetActive(true);
        tutorialBtn.SetActive(true);

        chapterContents.SetActive(false);
        tutorialContents.SetActive(false);
    }

    /// <summary>
    /// Displays the submenu for selecting campaign chapters.
    /// </summary>
    public void CampaignChaptersScreen()
    {
        campaignBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        chapterContents.SetActive(true);
    }

    /// <summary>
    /// Navigates back to the campaign chapter selection screen.
    /// </summary>
    public void BackToCampaignChaptersScreen()
    {
        chapterBtns.SetActive(true);
        returnToPlayContents.SetActive(true);

        chapter1Contents.SetActive(false);
        chapter2Contents.SetActive(false);

        chapter1ReturnBtn.SetActive(false);
        chapter2ReturnBtn.SetActive(false);
    }

    /// <summary>
    /// Displays the submenu for the first campaign chapter.
    /// </summary>
    public void Chapter1Screen()
    {
        chapterBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        chapter1Contents.SetActive(true);
        chapter1ReturnBtn.SetActive(true);
    }

    /// <summary>
    /// Displays the submenu for the second campaign chapter.
    /// </summary>
    public void Chapter2Screen()
    {
        chapterBtns.SetActive(false);
        returnToPlayContents.SetActive(false);
        chapter2Contents.SetActive(true);
        chapter2ReturnBtn.SetActive(true);
    }

    /// <summary>
    /// Displays the submenu for the tutorial.
    /// </summary>
    public void TutorialScreen()
    {
        campaignBtn.SetActive(false);
        tutorialBtn.SetActive(false);

        tutorialContents.SetActive(true);
        tutorialReturnBtn.SetActive(true);
    }

    #endregion

    #region Achievements Menu Functions

    /// <summary>
    /// Displays the achievements menu contents.
    /// </summary>
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

    /// <summary>
    /// Displays the book menu contents.
    /// </summary>
    public void BookScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(true);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(false);
    }

    /// <summary>
    /// Navigates back to the book menu.
    /// </summary>
    public void BackToBookScreen()
    {
        gardeningBtn.SetActive(true);
        cookingBtn.SetActive(true);

        gardeningContents.SetActive(false);
        cookingContents.SetActive(false);
    }

    /// <summary>
    /// Displays the gardening book contents.
    /// </summary>
    public void GardeningBookScreen()
    {
        gardeningBtn.SetActive(false);
        cookingBtn.SetActive(false);

        gardeningContents.SetActive(true);

        // Optionally, display the first page of gardening content
        // GardeningSkillOneScreen();
    }

    /// <summary>
    /// Displays the cooking book contents.
    /// </summary>
    public void CookingBookScreen()
    {
        gardeningBtn.SetActive(false);
        cookingBtn.SetActive(false);

        cookingContents.SetActive(true);

        // Optionally, display the first page of cooking content
        // CookingRecipeOneScreen();
    }

    /// <summary>
    /// Displays the content of the first gardening skill.
    /// </summary>
    public void GardeningSkillOneScreen()
    {
        gardeningSkill1.SetActive(true);
        gardeningSkill2.SetActive(false);
        gardeningSkill3.SetActive(false);
        gardeningSkill4.SetActive(false);
    }

    /// <summary>
    /// Displays the content of the second gardening skill.
    /// </summary>
    public void GardeningSkillTwoScreen()
    {
        gardeningSkill1.SetActive(false);
        gardeningSkill2.SetActive(true);
        gardeningSkill3.SetActive(false);
        gardeningSkill4.SetActive(false);
    }

    // Methods for displaying other gardening skills follow...

    /// <summary>
    /// Displays the content of the first cooking recipe.
    /// </summary>
    public void CookingRecipeOneScreen()
    {
        cookingRecipe1.SetActive(true);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(false);
    }

    /// <summary>
    /// Displays the content of the second cooking recipe.
    /// </summary>
    public void CookingRecipeTwoScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(true);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(false);
    }

    /// <summary>
    /// Displays the content of the third cooking recipe.
    /// </summary>
    public void CookingRecipeThreeScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(true);
        cookingRecipe4.SetActive(false);
    }

    /// <summary>
    /// Displays the content of the fourth cooking recipe.
    /// </summary>
    public void CookingRecipeFourScreen()
    {
        cookingRecipe1.SetActive(false);
        cookingRecipe2.SetActive(false);
        cookingRecipe3.SetActive(false);
        cookingRecipe4.SetActive(true);
    }
    #endregion

    #region Volume Menu Functions

    /// <summary>
    /// Displays the volume menu contents.
    /// </summary>
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

    /// <summary>
    /// Navigates back to the how-to-play menu.
    /// </summary>
    public void BackToHowToPlayScreen()
    {
        instructionsBtn.SetActive(true);
        vrControllerBindingsBtn.SetActive(true);
        creditsBtn.SetActive(true);

        campaignInstructionsContents.SetActive(false);
        vrControllerBindingsContents.SetActive(false);
        creditsContents.SetActive(false);
    }

    /// <summary>
    /// Displays the campaign instructions.
    /// </summary>
    public void CampaignInstructionsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        campaignInstructionsContents.SetActive(true);
    }

    /// <summary>
    /// Displays the VR controller bindings.
    /// </summary>
    public void VRControllerBindingsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        vrControllerBindingsContents.SetActive(true);
    }

    /// <summary>
    /// Displays the credits.
    /// </summary>
    public void CreditsScreen()
    {
        instructionsBtn.SetActive(false);
        vrControllerBindingsBtn.SetActive(false);
        creditsBtn.SetActive(false);

        creditsContents.SetActive(true);
    }

    #endregion

    #region Status Menu Functions

    /// <summary>
    /// Displays the status menu contents.
    /// </summary>
    public void StatusScreen()
    {
        playContents.SetActive(false);
        achievementsContents.SetActive(false);
        bookContents.SetActive(false);
        volumeContents.SetActive(false);
        howToPlayContents.SetActive(false);
        statusContents.SetActive(true);

        UpdatePlayerStatus();
    }

    #endregion

    #region Logout Menu Functions

    /// <summary>
    /// Displays the logout menu contents.
    /// </summary>
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

    /// <summary>
    /// Disables the logout menu.
    /// </summary>
    public void DisableLogoutScreen()
    {
        logoutContents.SetActive(false);
    }

    #endregion

    #region Quit Menu Functions

    /// <summary>
    /// Displays the quit menu contents.
    /// </summary>
    public void QuitScreen()
    {
        quitContents.SetActive(true);
    }

    /// <summary>
    /// Disables the quit menu.
    /// </summary>
    public void DisableQuitScreen()
    {
        quitContents.SetActive(false);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion

    /// <summary>
    /// Loads the specified tutorial scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void TutorialLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads the specified chapter scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void GameLoadSceneChapter(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Updates the player's status asynchronously.
    /// </summary>
    private async void UpdatePlayerStatus()
    {
        // Ensure FirebaseManager instance is available
        if (FirebaseManager.instance != null)
        {
            // Get the current user's ID from Firebase authentication
            string userId = FirebaseManager.instance.User.UserId;

            // Call the method to update player data UI in FirebaseManager
            await FirebaseManager.instance.UpdatePlayerDataUI(userId);
        }
    }
}
