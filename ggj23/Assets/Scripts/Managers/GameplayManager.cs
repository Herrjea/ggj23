using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    Input input;

    int[] p1Code;
    int[] p2Code;

    public int p1RightMatches = 0;
    public int p2RightMatches = 0;

    int codeLength = globals.codeLength;

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
        input.Gameplay.Enable();
        input.UI.Disable();
        GameEvents.InputSet.Invoke(input);

        confirmLeavePanel.SetActive(false);
        canvas.SetActive(false);

        p1Code = new int[codeLength];
        p2Code = new int[codeLength];

        GameEvents.P1NewCode.AddListener(NewP1Code);
        GameEvents.P2NewCode.AddListener(NewP2Code);

        GameEvents.P1KeyPress.AddListener(P1KeyPress);
        GameEvents.P2KeyPress.AddListener(P2KeyPress);
    }


    private void Update()
    {
        if (input.Gameplay.OpenMenu.WasPressedThisFrame())
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

        input.Gameplay.Disable();
        input.UI.Enable();

        Time.timeScale = 0;
    }

    void HideMenu()
    {
        canvas.SetActive(false);

        input.Gameplay.Enable();
        input.UI.Disable();

        Time.timeScale = 1;
    }


    void NewP1Code(int key1, int key2, int key3, int key4)
    {
        p1Code[0] = key1;
        p1Code[1] = key2;
        p1Code[2] = key3;
        p1Code[3] = key4;
    }

    void NewP2Code(int key1, int key2, int key3, int key4)
    {
        p2Code[0] = key1;
        p2Code[1] = key2;
        p2Code[2] = key3;
        p2Code[3] = key4;
    }


    void P1KeyPress(int key)
    {
        if (p1Code[p1RightMatches] == key)
        {
            GameEvents.P1RightPress.Invoke(p1RightMatches);

            p1RightMatches++;

            if (p1RightMatches == codeLength)
            {
                GameEvents.P1WordCompleted.Invoke();
                p1RightMatches = 0;
            }
        }
        else
        {
            GameEvents.P1WrongPress.Invoke(p1RightMatches);
            p1RightMatches = 0;
        }
    }

    void P2KeyPress(int key)
    {
        if (p2Code[p2RightMatches] == key)
        {
            GameEvents.P2RightPress.Invoke(p2RightMatches);

            p2RightMatches++;

            if (p2RightMatches == codeLength)
            {
                GameEvents.P2WordCompleted.Invoke();
                p2RightMatches = 0;
            }
        }
        else
        {
            GameEvents.P2WrongPress.Invoke(p2RightMatches);
            p2RightMatches = 0;
        }
    }
}
