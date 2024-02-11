using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class CampaignChapter1Manager : MonoBehaviour
{
    public static CampaignChapter1Manager instance;

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
    [SerializeField] private GameObject checkpointFour;
    [SerializeField] private GameObject checkpointFinal;

    [Header("Chapter 1 Texts Checkpoints")]
    [SerializeField] private GameObject scytheCheckpoint;
    [SerializeField] private GameObject trowelCheckpoint;
    [SerializeField] private GameObject wateringCheckpoint;
    [SerializeField] private GameObject finalCheckpoint;

    [Header("Chapter 1 Growth Stages")]
    [SerializeField] private GameObject growthSprouts;
    [SerializeField] private GameObject middleToFinalStage;

    [Header("Chapter 1 Completion Canvas")]
    [SerializeField] private GameObject chapter1CompletionCanvas;

    public void ShowQuestGarden()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(true);
    }

    public void ShowQuestWalkthrough2()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(false);

        chapter1QuestWalkthrough1.SetActive(false);
        chapter1QuestWalkthrough2.SetActive(true);

        ShowCheckpointOneTrigger();
    }

    public void ShowQuestWalkthrough3()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(false);

        chapter1QuestWalkthrough2.SetActive(false);
        chapter1QuestWalkthrough3 .SetActive(true);
    }
    
    public void ShowCheckpointOneTrigger()
    {
        checkpointOne.SetActive(true); 
        checkpointTwo.SetActive(false);
        checkpointThree.SetActive(false);
        checkpointFour.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointTwoTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(false);
        checkpointFour.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointThreeTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(true);
        checkpointFour.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointFourTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(false);
        checkpointThree.SetActive(false);
        checkpointFour.SetActive(true);
        checkpointFinal.SetActive(false);
    }

    public void ShowCheckpointFinalTrigger()
    {
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(true);
        checkpointThree.SetActive(false);
        checkpointFour.SetActive(false);
        checkpointFinal.SetActive(false);
    }

    public void ShowTrowelText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(true);
        wateringCheckpoint.SetActive(false); 
        finalCheckpoint.SetActive(false);
    }

    public void ShowWateringCanText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(true);
        finalCheckpoint.SetActive(false);
    }

    public void ShowFinalText()
    {
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
        finalCheckpoint.SetActive(true);
    }

    public void ShowGrowthSprouts()
    {
        growthSprouts.SetActive(true);
        middleToFinalStage.SetActive(false);
    }

    public void ShowMiddleToFinalSprouts()
    {
        growthSprouts.SetActive(false);
        middleToFinalStage.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowCheckpointTwoTrigger();
            ShowQuestWalkthrough3();
        }

        if (other.CompareTag("Scythe"))
        {
            ShowCheckpointThreeTrigger();
            ShowTrowelText();
        }

        if (other.CompareTag("Trowel"))
        {
            ShowCheckpointFourTrigger();
            ShowWateringCanText();
            ShowGrowthSprouts();
        }

        if (other.CompareTag("WateringCan"))
        {
            ShowCheckpointFinalTrigger();
            ShowFinalText();
            ShowMiddleToFinalSprouts();
        }

        if (other.CompareTag("Bell Pepper"))
        {
           StartCoroutine(SpawnGoodJobUI());
        }
    }

    public IEnumerator SpawnGoodJobUI()
    {
        if (chapter1CompletionCanvas != null)
        {
            yield return new WaitForSeconds(5f);
            chapter1CompletionCanvas.SetActive(true);
            StartCoroutine(CompleteFirstChapterCoroutine());
        }
        else
        {
            Debug.LogError("It's not set!");
        }
    }

     private IEnumerator CompleteFirstChapterCoroutine()
     {
         yield return new WaitForSeconds(5f);

         SceneManager.LoadScene("MainMenu");
     }

    //S Method to deactivate all Chapter 1 elements except the completion canvas
    public void DeactivateChapter1Elements()
    {
        chapter1QuestWalkthrough1.SetActive(false);
        chapter1QuestWalkthrough2.SetActive(false);
        chapter1QuestWalkthrough3.SetActive(false);
        questIntro.SetActive(false);
        questGarden.SetActive(false);
        checkpointOne.SetActive(false);
        checkpointTwo.SetActive(false);
        checkpointThree.SetActive(false);
        checkpointFour.SetActive(false);
        checkpointFinal.SetActive(false);
        scytheCheckpoint.SetActive(false);
        trowelCheckpoint.SetActive(false);
        wateringCheckpoint.SetActive(false);
        finalCheckpoint.SetActive(false);
        growthSprouts.SetActive(false);
        middleToFinalStage.SetActive(false);
    }
}
