using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class clicker : MonoBehaviour
{
    public TMP_Text clickCountText;
    public GameObject panel; // Reference to the UI panel

    private int clickCount = 0;
    private float panelDisplayTime = 3f;
    private bool isPanelActive = false;

    void Update()
    {
        // Check for "E" key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            Click();
        }

        // Check if the click count has reached 10
        if (clickCount >= 10 && !isPanelActive)
        {
            // Show the panel when the click count reaches 10
            ShowPanel();
        }

        // Update the timer
        if (isPanelActive)
        {
            panelDisplayTime -= Time.deltaTime;

            if (panelDisplayTime <= 0)
            {
                ChangeScene();
            }
        }
    }

    void Click()
    {
        clickCount++;
        UpdateClickCountText();
    }

    void UpdateClickCountText()
    {
        clickCountText.text = "Clicks: " + clickCount.ToString();

        // Check and update the achievement progress
        if (clickCount >= 10 && !isPanelActive)
        {
            AchievementManager achievementManager = FindObjectOfType<AchievementManager>();

            if (achievementManager != null)
            {
                achievementManager.achievements[0].isAchieved = () => clickCount >= 10;

                // Update the achievement data in Firebase
                FirebaseManager.instance.UpdateAchievementData("ClickerPro");
            }
            else
            {
                Debug.LogError("AchievementManager not found in the scene.");
            }
        }
    }

    void ShowPanel()
    {
        panel.SetActive(true);
        isPanelActive = true;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Rey'sTestingMenuArea");
    }
}
