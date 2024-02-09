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

    public GameObject finalTriggerArea;

    private bool finalTriggerAreaOccupied = false;

    [SerializeField] private GameObject firstTaskCompletionCanvas;

    private Animation anim;

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

        anim = GetComponent<Animation>();
    }

    public void SetTutorialUICompletion(GameObject firstTaskUI)
    {
        firstTaskCompletionCanvas = firstTaskUI;
    }

    public bool AreasOccupied()
    {
        return finalTriggerAreaOccupied;
    }

    public void SetTriggerAreaOccupied(int areaNumber, bool occupied)
    {
        if (areaNumber == 1)
        {
            finalTriggerAreaOccupied = occupied;
        }

        if (AreasOccupied())
        {
            SpawnGoodJobUI();
        }
    }

    public void ShowWalkthrough1()
    {
        chapter1QuestWalkthrough1.SetActive(true);
        chapter1QuestWalkthrough2.SetActive(false);
        chapter1QuestWalkthrough3.SetActive(false);

        finalTriggerArea.SetActive(false);

    }

    public void ShowGardenQuest()
    {
        questIntro.SetActive(false);
        questGarden.SetActive(true);
    }

    public void ShowWalkthrough2()
    {
        chapter1QuestWalkthrough1.SetActive(false);
        chapter1QuestWalkthrough2.SetActive(true);
        chapter1QuestWalkthrough3.SetActive(false);

        finalTriggerArea.SetActive(false);

        if (anim != null)
        {
            // Play the animation clip named "MyAnimation"
            anim.Play("ArrowGuidance");
        }
    }

    public void ShowWalkthrough3()
    {
        chapter1QuestWalkthrough1.SetActive(false);
        chapter1QuestWalkthrough2.SetActive(false);
        chapter1QuestWalkthrough3.SetActive(true);

        finalTriggerArea.SetActive(true);
    }

    private void SpawnGoodJobUI()
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
    }

    private IEnumerator CompleteFirstTaskCoroutine()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }
}
