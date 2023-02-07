using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    Input input;


    private void Awake()
    {
        input = new Input();
        input.Enable();
        input.Gameplay.Enable();
    }


    private void Update()
    {
        if (input.Gameplay.Any.WasPressedThisFrame())
            SceneManager.LoadScene("Gameplay");
    }
}
