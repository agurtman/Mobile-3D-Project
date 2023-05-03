using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Race : MonoBehaviour
{
    [SerializeField] private GameObject raceUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            raceUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            ExitRace();
        }
    }

    public void StartRace()
    {
        SceneManager.LoadScene("Drift Track");
    }

    public void ExitRace()
    {
        raceUI.SetActive(false);
    }
}
