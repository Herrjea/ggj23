using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    Input input;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject confirmLeavePanel;
    [SerializeField] Button optionsDefaultButton;
    [SerializeField] Button optionsLeaveButton;
    [SerializeField] Button leaveDefaultButton;


    private void Awake()
    {
        input = new Input();
        input.Enable();
        input.Movement.Enable();
        input.UI.Disable();
        GameEvents.InputSet.Invoke(input);

        confirmLeavePanel.SetActive(false);
        canvas.SetActive(false);
    }


    private void Update()
    {
        if (input.Movement.OpenMenu.WasPressedThisFrame())
        {
            ShowMenu();
        }
        else if (input.UI.Cancel.WasPressedThisFrame() || input.UI.CloseMenu.WasPressedThisFrame())
        {
            HideMenu();
        }

    }


    public void Resume()
    {
        HideMenu();
    }

    public void Leave()
    {
        optionsPanel.SetActive(false);
        confirmLeavePanel.SetActive(true);

        leaveDefaultButton.Select();
    }

    public void ConfirmLeave()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }

    public void CancelLeave()
    {
        confirmLeavePanel.SetActive(false);
        optionsPanel.SetActive(true);

        optionsLeaveButton.Select();
    }


    void ShowMenu()
    {
        canvas.SetActive(true);

        optionsPanel.SetActive(true);
        optionsDefaultButton.Select();
        confirmLeavePanel.SetActive(false);

        input.Movement.Disable();
        input.UI.Enable();

        Time.timeScale = 0;
    }

    void HideMenu()
    {
        canvas.SetActive(false);

        input.Movement.Enable();
        input.UI.Disable();

        Time.timeScale = 1;
    }
}
