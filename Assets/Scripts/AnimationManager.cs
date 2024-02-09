using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Door Animation")]
    public Animator doorAnim;     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        doorAnim.SetBool("IsOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnim.SetBool("IsOpen", false);
    }
}
