using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    void Start()
    {
        // Ensure AudioSource and Slider are properly set in the Inspector
        if (audioSource == null || slider == null)
        {
            Debug.LogError("AudioSource or Slider is not assigned in the inspector!");
            return;
        }

        // Set initial volume to slider's value
        audioSource.volume = slider.value;
    }

    public void SetVolume()
    {
        // Adjust volume based on the slider's value
        audioSource.volume = slider.value;
    }
}
