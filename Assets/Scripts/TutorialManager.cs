using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Quest Dialogues")]
    [SerializeField] private GameObject questIntro;
    [SerializeField] private GameObject questWalkTut;
    [SerializeField] private GameObject questLookTut;
    [SerializeField] private GameObject questMenuRayPt1;
    [SerializeField] private GameObject questMenuRayPt2;
    [SerializeField] private GameObject questMenuRayPt3;
    [SerializeField] private GameObject questMenuRayPt4;
    [SerializeField] private GameObject questRayObjPt1;
    [SerializeField] private GameObject questRayObjPt2;
    [SerializeField] private GameObject questGrabObjTut;
    [SerializeField] private GameObject questSnapObjPt1;
    [SerializeField] private GameObject questSnapObjPt2;
    [SerializeField] private GameObject questDistanceGrabObjTut;
    [SerializeField] private GameObject questFinalPt1;
    [SerializeField] private GameObject questFinalPt2;
    [SerializeField] private GameObject questFinalPt3;
    [SerializeField] private GameObject questFinalPt4;
    [SerializeField] private GameObject questEnd;

    private Animation anim;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get the Animation component attached to the GameObject
        anim = GetComponent<Animation>();
    }
    /// <summary>
    /// Show the walking tutorial.
    /// </summary>
    public void ShowWalkTut()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(true);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);    
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the looking tutorial.
    /// </summary>
    public void ShowLookTut()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(true);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the menu part 1.
    /// </summary>
    public void ShowMenuRayPart1()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(true);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the menu part 2.
    /// </summary>
    public void ShowMenuRayPart2()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(true);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the menu part 3.
    /// </summary>
    public void ShowMenuRayPart3()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(true);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the menu part 4.
    /// </summary>
    public void ShowMenuRayPart4()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(true);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the ray object part 1.
    /// </summary>
    public void ShowRayObjPart1()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(true);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the ray object part 2.
    /// </summary>
    public void ShowRayObjPart2()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(true);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the grab object tutorial.
    /// </summary>
    public void ShowGrabObjTut()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(true);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the snap object part 1.
    /// </summary>
    public void ShowSnapObjPart1()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(true);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);

        if (anim != null)
        {
            // Play the animation clip named "MyAnimation"
            anim.Play("ArrowGuidance");
        }
    }
    /// <summary>
    /// Show the snap object part 2.
    /// </summary>
    public void ShowSnapObjPart2()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(true);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the distance grab tutorial.
    /// </summary>
    public void ShowDistanceGrabObjTut()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(true);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the final part 1.
    /// </summary>
    public void ShowFinalPart1()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(true);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the final part 2.
    /// </summary>
    public void ShowFinalPart2()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(true);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the final part 3.
    /// </summary>
    public void ShowFinalPart3()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(true);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the final part 4.
    /// </summary>
    public void ShowFinalPart4()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(true);
        questEnd.SetActive(false);
    }
    /// <summary>
    /// Show the end tutorial.
    /// </summary>
    public void ShowEndTut()
    {
        questIntro.SetActive(false);
        questWalkTut.SetActive(false);
        questLookTut.SetActive(false);
        questMenuRayPt1.SetActive(false);
        questMenuRayPt2.SetActive(false);
        questMenuRayPt3.SetActive(false);
        questMenuRayPt4.SetActive(false);
        questRayObjPt1.SetActive(false);
        questRayObjPt2.SetActive(false);
        questGrabObjTut.SetActive(false);
        questSnapObjPt1.SetActive(false);
        questSnapObjPt2.SetActive(false);
        questDistanceGrabObjTut.SetActive(false);
        questFinalPt1.SetActive(false);
        questFinalPt2.SetActive(false);
        questFinalPt3.SetActive(false);
        questFinalPt4.SetActive(false);
        questEnd.SetActive(true);
    }
}
