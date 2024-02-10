using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignChapter1Manager : MonoBehaviour
{
    public static CampaignChapter1Manager Instance { get; private set; }

    [Header("Chapter 1 Quest Dialogues")]
    [SerializeField] private GameObject chapter1QuestWalkthrough1;
    [SerializeField] private GameObject chapter1QuestWalkthrough2;
    [SerializeField] private GameObject chapter1QuestWalkthrough3;

    [SerializeField] private GameObject questIntro;
    [SerializeField] private GameObject questGarden;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    

    /*private void SpawnGoodJobUI()
    {
        if (firstTaskCompletionCanvas != null)
        {
            firstTaskCompletionCanvas.SetActive(true);
            StartCoroutine(CompleteFirstTaskCoroutine());
        }
        else
        {
            Debug.LogError("It's not set!");
        }
    }*/

   /* private IEnumerator CompleteFirstTaskCoroutine()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }*/
}
