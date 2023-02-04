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

    int[] p1typeHistory;
    int[] p2typeHistory;
    int p1typed = 0;
    int p2typed = 0;

    int p1lvl = globals.startingLevel;
    int p2lvl = globals.startingLevel;
    int maxLevel = globals.maxLevel;
    int codeLength = globals.codeLength;

    [SerializeField] GameObject menuCanvas;
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
        menuCanvas.SetActive(false);

        p1Code = new int[codeLength];
        p2Code = new int[codeLength];
        p1typeHistory = new int[codeLength];
        p2typeHistory = new int[codeLength];
        for (int i = 0; i < codeLength; i++)
        {
            p1typeHistory[i] = -1;
            p2typeHistory[i] = -1;
        }

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
        menuCanvas.SetActive(true);

        optionsPanel.SetActive(true);
        optionsDefaultButton.Select();
        confirmLeavePanel.SetActive(false);

        input.Gameplay.Disable();
        input.UI.Enable();

        Time.timeScale = 0;
    }

    void HideMenu()
    {
        menuCanvas.SetActive(false);

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
        if (p1typed < codeLength)
        {
            p1typeHistory[p1typed] = key;
            p1typed++;
        }
        else
        {
            p1typeHistory[0] = p1typeHistory[1];
            p1typeHistory[1] = p1typeHistory[2];
            p1typeHistory[2] = p1typeHistory[3];
            p1typeHistory[3] = key;
        }

        if (CheckAgainsP1(p1typeHistory))
        {
            p1lvl++;
            GameEvents.P1LvlChange.Invoke(p1lvl);
            if (p1lvl == maxLevel)
                GameEvents.P1Wins.Invoke();
            else
                GameEvents.P1OwnWordCompleted.Invoke();

            ResetP1Typebox();
        }

        if (CheckAgainsP2(p1typeHistory))
        {
            p2lvl--;
            GameEvents.P2LvlChange.Invoke(p2lvl);
            if (p2lvl == 0)
                GameEvents.P1Wins.Invoke();
            else
                GameEvents.P1EnemyWordCompleted.Invoke();

            ResetP1Typebox();
        }

        //if (p1Code[p1RightMatches] == key)
        //{
        //    GameEvents.P1RightPress.Invoke(p1RightMatches);

        //    p1RightMatches++;

        //    if (p1RightMatches == codeLength)
        //    {
        //        GameEvents.P1WordCompleted.Invoke();
        //        p1RightMatches = 0;

        //        p1lvl++;
        //        if (p1lvl == maxLevel)
        //        {
        //            GameEvents.P1Wins.Invoke();
        //            input.Gameplay.Disable();
        //        }
        //        else
        //            GameEvents.P1LvlChange.Invoke(p1lvl);
        //    }
        //}
        //else
        //{
        //    GameEvents.P1WrongPress.Invoke(p1RightMatches);
        //    p1RightMatches = 0;
        //}
    }

    void P2KeyPress(int key)
    {
        if (p2typed < codeLength)
        {
            p2typeHistory[p2typed] = key;
            p2typed++;
        }
        else
        {
            p2typeHistory[0] = p2typeHistory[1];
            p2typeHistory[1] = p2typeHistory[2];
            p2typeHistory[2] = p2typeHistory[3];
            p2typeHistory[3] = key;
        }

        if (CheckAgainsP2(p2typeHistory))
        {
            p2lvl++;
            GameEvents.P2LvlChange.Invoke(p2lvl);
            if (p2lvl == maxLevel)
                GameEvents.P2Wins.Invoke();
            else
                GameEvents.P2OwnWordCompleted.Invoke();

            ResetP2Typebox();
        }

        if (CheckAgainsP1(p2typeHistory))
        {
            p1lvl--;
            GameEvents.P1LvlChange.Invoke(p1lvl);
            if (p1lvl == 0)
                GameEvents.P2Wins.Invoke();
            else
                GameEvents.P2EnemyWordCompleted.Invoke();

            ResetP2Typebox();
        }

        //if (p2Code[p2RightMatches] == key)
        //{
        //    GameEvents.P2RightPress.Invoke(p2RightMatches);

        //    p2RightMatches++;

        //    if (p2RightMatches == codeLength)
        //    {
        //        GameEvents.P2WordCompleted.Invoke();
        //        p2RightMatches = 0;

        //        p2lvl++;
        //        if (p2lvl == maxLevel)
        //        {
        //            GameEvents.P2Wins.Invoke();
        //            input.Gameplay.Disable();
        //        }
        //        else
        //            GameEvents.P2LvlChange.Invoke(p2lvl);
        //    }
        //}
        //else
        //{
        //    GameEvents.P2WrongPress.Invoke(p2RightMatches);
        //    p2RightMatches = 0;
        //}
    }


    bool CheckAgainsP1(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p1Code[i])
                return false;

        return true;
    }

    bool CheckAgainsP2(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p2Code[i])
                return false;

        return true;
    }


    void ResetP1Typebox()
    {
        p1typed = 0;
        for (int i = 0; i < codeLength; i++)
            p1typeHistory[i] = -1;
    }

    void ResetP2Typebox()
    {
        p2typed = 0;
        for (int i = 0; i < codeLength; i++)
            p2typeHistory[i] = -1;
    }
}
