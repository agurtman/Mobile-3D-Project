using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject optionsUI;
    [SerializeField] GameObject mainPos;
    [SerializeField] GameObject optionsPos;
    GameObject cam;
    bool isOptions;

    public enum State { Main, Settings }
    State current;

    void Start()
    {
        current = State.Main;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (!isOptions)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, mainPos.transform.position, 0.05f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, mainPos.transform.rotation, 0.05f);
        }
        else
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, optionsPos.transform.position, 0.05f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, optionsPos.transform.rotation, 0.05f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchState(State state)
    {
        current = state;

        switch(state)
        {
            case State.Main:
                mainUI.SetActive(true);
                optionsUI.SetActive(false);
                isOptions = false;
                break;
            case State.Settings:
                mainUI.SetActive(false);
                optionsUI.SetActive(true);
                isOptions = true;
                break;
            default:
                break;
        }
    }

    public void OpenSettings()
    {
        if (!isOptions) SwitchState(State.Settings);
        else SwitchState(State.Main);
    }
}
