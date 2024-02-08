using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject imageToShow;

    void Start()
    {
        // Initially set the image to be inactive
        SetImageVisibility(false);

        // Check visibility based on the achievement status
        CheckVisibility();
    }

    void CheckVisibility()
    {
        // Check if achievement 1 is unlocked
        if (PersistentManager.Instance != null && PersistentManager.Instance.achievement1Unlocked)
        {
            // If the achievement is unlocked, show the image
            SetImageVisibility(true);
        }
    }

    void SetImageVisibility(bool isVisible)
    {
        if (imageToShow != null)
        {
            imageToShow.SetActive(isVisible);
        }
        else
        {
            Debug.LogError("Image GameObject is not assigned to the ImageController script.");
        }
    }
}