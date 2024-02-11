using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignChapter2Manager : MonoBehaviour
{
    public static CampaignChapter2Manager instance;

    [Header("Chapter 2 Quest Dialogues")]
    [SerializeField] private GameObject chapter2QuestWalkthrough1;
    [SerializeField] private GameObject chapter2QuestWalkthrough2;

    [Header("Chapter 2 QuestWalkthrough1")]
    [SerializeField] private GameObject questIntro;
    [SerializeField] private GameObject questKitchen;

    [Header("Chapter 2 QuestWalkthrough2")]
    [SerializeField] private GameObject kitchenInfo;
    [SerializeField] private GameObject recipeInstruction;
    [SerializeField] private GameObject gatherInstruction;
    [SerializeField] private GameObject ingredientsInstruction;
    [SerializeField] private GameObject bowlInstruction;
    [SerializeField] private GameObject finalInstruction;

    [Header("Chapter 2 Main Trigger Checkpoints")]
    [SerializeField] private GameObject putDownIngredients;
    [SerializeField] private GameObject foodsPlacement;
    [SerializeField] private GameObject cuttingPlacement;
    [SerializeField] private GameObject finalCheckpoint;

    [Header("Chapter 2 Ingredients Trigger")]
    [SerializeField] private GameObject bellPepper;
    [SerializeField] private GameObject carrotSliced;
    [SerializeField] private GameObject onionSliced;
    [SerializeField] private GameObject tomatoSliced;
    [SerializeField] private GameObject lettuce;
    [SerializeField] private GameObject salt;
    [SerializeField] private GameObject pepper;
    [SerializeField] private GameObject sugar;

    [Header("Chapter 2 Foods Placement Trigger")]
    [SerializeField] private GameObject tomatoPlacement;
    [SerializeField] private GameObject carrotPlacement;
    [SerializeField] private GameObject onionPlacement;

    [Header("Chapter 2 Cutting Placement Trigger")]
    [SerializeField] private GameObject tomatoCut;
    [SerializeField] private GameObject carrotCut;
    [SerializeField] private GameObject onionCut;

    [Header("Chapter 2 Food Slices")]
    [SerializeField] private GameObject slicedTomato;
    [SerializeField] private GameObject pileOfDicedCarrots;
    [SerializeField] private GameObject slicedOnions;

    private int ingredientCount = 0; // Counter for ingredients

    [Header("Chapter 2 Completion Canvas")]
    [SerializeField] private GameObject chapter2CompletionCanvas;

    public void ShowQuestKitchen()
    {
        questIntro.SetActive(false);
        questKitchen.SetActive(true);
    }

    public void ShowQuestWalkthrough2()
    {
        questIntro.SetActive(false);
        questKitchen.SetActive(false);

        chapter2QuestWalkthrough1.SetActive(false);
        chapter2QuestWalkthrough2.SetActive(true);
    }

    public void ShowRecipeInstruction()
    {
        kitchenInfo.SetActive(false);
        recipeInstruction.SetActive(true);
        gatherInstruction.SetActive(false);
        ingredientsInstruction.SetActive(false);
        bowlInstruction.SetActive(false);
        finalInstruction.SetActive(false);  
    }

    public void ShowGatherInstruction()
    {
        kitchenInfo.SetActive(false);
        recipeInstruction.SetActive(false);
        gatherInstruction.SetActive(true);
        ingredientsInstruction.SetActive(false);
        bowlInstruction.SetActive(false);
        finalInstruction.SetActive(false);
    }

    public void ShowIngredientsInstruction()
    {
        kitchenInfo.SetActive(false);
        recipeInstruction.SetActive(false);
        gatherInstruction.SetActive(false);
        ingredientsInstruction.SetActive(true);
        bowlInstruction.SetActive(false);
        finalInstruction.SetActive(false);
    }

    public void ShowBowlInstruction()
    {
        kitchenInfo.SetActive(false);
        recipeInstruction.SetActive(false);
        gatherInstruction.SetActive(false);
        ingredientsInstruction.SetActive(false);
        bowlInstruction.SetActive(true);
        finalInstruction.SetActive(false);
    }

    public void ShowFinalInstruction()
    {
        kitchenInfo.SetActive(false);
        recipeInstruction.SetActive(false);
        gatherInstruction.SetActive(false);
        ingredientsInstruction.SetActive(false);
        bowlInstruction.SetActive(false);
        finalInstruction.SetActive(true);
    }

    public void ShowPutDownIngredients()
    {
        putDownIngredients.SetActive(true);
        cuttingPlacement.SetActive(false);
        finalCheckpoint.SetActive(false);
    }

    public void ShowFoodsPlacement()
    {
        foodsPlacement.SetActive(true);
        cuttingPlacement.SetActive(false);
        finalCheckpoint.SetActive(false);
    }

    public void ShowCuttingPlacement()
    {
        foodsPlacement.SetActive(false);
        cuttingPlacement.SetActive(true);
        finalCheckpoint.SetActive(false);
    }

    public void ShowFinalCheckpoint()
    {
        putDownIngredients.SetActive(false);
        foodsPlacement.SetActive(false);
        cuttingPlacement.SetActive(false);
        finalCheckpoint.SetActive(true);

        ShowFinalInstruction();
    }

    public void ShowSlicedTomato()
    {
        slicedTomato.SetActive(true);
    }

    public void ShowPileOfDicedCarrots()
    {
        pileOfDicedCarrots.SetActive(true);
    }

    public void ShowSlicedOnions()
    {
        slicedOnions.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        #region Ingredients Trigger  
        if (IsIngredient(other.tag))
        {
            ingredientCount++; 
            CheckIngredientsCompletion(); 
        }
        #endregion

        #region Foods Placement Trigger
        if (other.CompareTag("Tomato"))
        {
            ShowCuttingPlacement();
        }

        if (other.CompareTag("Carrot"))
        {
            ShowCuttingPlacement();
        }

        if (other.CompareTag("Onion"))
        {
            ShowCuttingPlacement();
        }
        #endregion

        if (other.CompareTag("Knife"))
        {
            ShowSlicedTomato();
            ShowPileOfDicedCarrots();
            ShowSlicedOnions();
        }

        if (other.CompareTag("GrabbableObject"))
        {
            StartCoroutine(SpawnGoodJobUI());
        }
    }

    // This method is called when an object exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is one of the ingredients
        if (IsIngredient(other.tag))
        {
            ingredientCount--; // Decrement the ingredient count
        }
    }

    // Check if the object tag corresponds to an ingredient
    private bool IsIngredient(string tag)
    {
        return tag == "Bell Pepper" ||
               tag == "SlicedCarrot" ||
               tag == "SlicedOnion" ||
               tag == "SlicedTomato" ||
               tag == "Lettuce" ||
               tag == "Salt" ||
               tag == "Pepper" ||
               tag == "Sugar";
    }

    private void CheckIngredientsCompletion()
    {
        if (ingredientCount >= 8) 
        {
            ShowFinalCheckpoint();
        }
    }

    public IEnumerator SpawnGoodJobUI()
    {
        if (chapter2CompletionCanvas != null)
        {
            yield return new WaitForSeconds(5f);
            chapter2CompletionCanvas.SetActive(true);
            StartCoroutine(CompleteSecondChapterCoroutine());
        }
        else
        {
            Debug.LogError("It's not set!");
        }
    }

    private IEnumerator CompleteSecondChapterCoroutine()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenu");
    }
}
