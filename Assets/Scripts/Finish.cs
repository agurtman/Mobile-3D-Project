using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] Text finishUI;
    private int checkpoints;
    private int count;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyCar"))
        {
            count++;
        }

        if (other.CompareTag("Car"))
        {
            checkpoints = FindObjectOfType<CollectCheck>().GetCheck();

            if (checkpoints >= 8)
            {
                count++;
                finishUI.text = "Ты занял " + count.ToString() + " место";
                Invoke("ExitRace", 5);
            }
        }
    }

    private void ExitRace()
    {
        SceneManager.LoadScene(1);
    }
}
