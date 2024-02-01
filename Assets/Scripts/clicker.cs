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
    }

    void ShowPanel()
    {
        panel.SetActive(true); // Show the panel when the click count reaches 10
        isPanelActive = true;
    }

    void ChangeScene()
    {
        // Change scene when the timer expires
        SceneManager.LoadScene("Rey'sTestingMenuArea");
    }
}
