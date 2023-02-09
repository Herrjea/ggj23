using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    Input input;


    private void Awake()
    {
        input = new Input();
        input.UI.Enable();
    }


    private void Update()
    {
        if (input.UI.Cancel.WasPerformedThisFrame() || input.UI.Submit.WasPerformedThisFrame())
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
