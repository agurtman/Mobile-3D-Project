using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    PlayerLook playerLook;
    bool pause;

    private void Start()
    {
        playerLook = FindObjectOfType<PlayerLook>();
    }

    public void OnPause()
    {
        if (!pause)
        {
            pause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            if (playerLook != null)
                playerLook.enabled = false;
        }
        else
        {
            pause = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
            if (playerLook != null)
                playerLook.enabled = true;
        }
    }
}
