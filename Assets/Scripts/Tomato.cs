using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    [Header("First Task Completion")]

    [SerializeField] private GameObject tomato;

    private void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tomato.SetActive(true);
            CampaignChapter1Manager.Instance.ShowWalkthrough3();
        }
    }
}
