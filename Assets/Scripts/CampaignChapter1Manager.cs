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

    [Header("Chapter 1 QuestWalkthrough1")]
    [SerializeField] private GameObject questIntro;
    [SerializeField] private GameObject questGarden;

    [Header("Chapter 1 Trigger Checkpoints")]
    [SerializeField] private GameObject checkpointOne;
    [SerializeField] private GameObject checkpointTwo;
    [SerializeField] private GameObject checkpointThree;
    [SerializeField] private GameObject checkpointFinal;

    [Header("Chapter 1 Texts Checkpoints")]
    [SerializeField] private GameObject scytheCheckpoint;
    [SerializeField] private GameObject trowelCheckpoint;
    [SerializeField] private GameObject wateringCheckpoint;
    [SerializeField] private GameObject finaleCheckpoint;

    [Header("Chapter 1 Growth Stages")]
    [SerializeField] private GameObject growthSprouts;
    [SerializeField] private GameObject middleStage;
    [SerializeField] private GameObject finalStage;

    [Header("Chapter 1 Completion Canvas")]
    [SerializeField] private GameObject chapter1CompletionCanvas;
    

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

    public void ShowQuestGarden()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(true);
    }

    public void ShowQuestWalkthrough2()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(false);

        chapter1QuestWalkthrough2.SetActive(true);

        ShowCheckpointOneTrigger();
    }

    public void ShowQuestWalkthrough3()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(false);

        chapter1QuestWalkthrough2.SetActive(false);
        chapter1QuestWalkthrough3 .SetActive(true);

        ShowCheckpointTwoTrigger();
    }
    
    public void ShowCheckpointOneTrigger()
    {
        checkpointOne.SetActive(true); 
        checkpointTwo.SetActive(false);
        checkpointThree.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointTwoTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointThreeTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointFinalTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowScytheText()
    {
        scytheCheckpoint.SetActive(true);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
    }

    public void ShowTrowelText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(true);
        wateringCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
    }

    public void ShowWateringCanText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(true);
        wateringCheckpoint.SetActive(false);
    }

    public void ShowFinalText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
        finaleCheckpoint.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowCheckpointOneTrigger();
        }

        else if (other.CompareTag("Scythe"))
        {
            ShowCheckpointOneTrigger();
        }

        else if (other.CompareTag("Trowel"))
        {
            ShowCheckpointOneTrigger();
        }

        else if (other.CompareTag("WateringCan"))
        {
            ShowCheckpointOneTrigger();
        }

        else if (other.CompareTag("Bell Pepper"))
        {
            ShowCheckpointOneTrigger();
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
