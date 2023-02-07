using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    bool showingControls = false;

    Input input;


    private void Awake()
    {
        input = new Input();
        input.Enable();
    }


    private void Update()
    {
        if (showingControls && input.UI.Cancel.WasPressedThisFrame())
        {
            HideControls();
        }
    }


    public void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Controls()
    {
        if (showingControls)
            HideControls();
        else
            ShowControls();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Leave()
    {
        Application.Quit();
    }


    void ShowControls()
    {
        if (!showingControls)
        {
            showingControls = true;
            GameEvents.ShowControls.Invoke();
        }
    }

    public void HideControls()
    {
        if (showingControls)
        {
            showingControls = false;
            GameEvents.HideControls.Invoke();
        }
    }
}
