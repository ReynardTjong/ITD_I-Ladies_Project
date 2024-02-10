using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [System.Serializable]
    public class AnimatedObject
    {
        public Animator animator;
        public string parameterName;
    }

    public List<AnimatedObject> animatedObjects = new List<AnimatedObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAnimationStates(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAnimationStates(false);
        }
    }

    private void SetAnimationStates(bool state)
    {
        foreach (AnimatedObject obj in animatedObjects)
        {
            obj.animator.SetBool(obj.parameterName, state);
        }
    }
}
