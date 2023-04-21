using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    Input input;

    int[] p1BasicCode;
    int[] p2BasicCode;
    int[] p1SpecialCode;
    int[] p2SpecialCode;

    int[] p1TypeHistory;
    int[] p2TypeHistory;

    int p1lvl = globals.startingLevel;
    int p2lvl = globals.startingLevel;
    int maxLevel = globals.maxLevel;
    int codeLength = globals.codeLength;

    [SerializeField] float specialAbilityMinCDTime = 3;
    [SerializeField] float specialAbilityMaxCDTime = 6;

    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject confirmLeavePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] Button optionsDefaultButton;
    [SerializeField] Button optionsLeaveButton;
    [SerializeField] Button leaveDefaultButton;
    [SerializeField] Button winDefaultButton;
    [SerializeField] TMPro.TextMeshProUGUI pointsText;
    [SerializeField] TMPro.TextMeshProUGUI idleText;

    [SerializeField] GameObject p1WinLights;
    [SerializeField] GameObject p2WinLights;
    GameObject winLights;


    [SerializeField] float winWait = 3;
    [SerializeField] float curtainFadeInDuration = 3;
    [SerializeField] float menuShowupWait = 3;

    int winner = -1;

    [HideInInspector] public static int p1ConsecutiveVictories = 0;
    [HideInInspector] public static int p2ConsecutiveVictories = 0;

    [SerializeField] float maxIdleTimeAfterWictory;


    private void Awake()
    {
        input = new Input();
        input.Enable();
        input.Gameplay.Enable();
        input.UI.Disable();
        GameEvents.InputSet.Invoke(input);

        confirmLeavePanel.SetActive(false);
        menuCanvas.SetActive(false);

        p1BasicCode = new int[codeLength];
        p2BasicCode = new int[codeLength];
        p1SpecialCode = new int[codeLength];
        p2SpecialCode = new int[codeLength];
        p1TypeHistory = new int[codeLength];
        p2TypeHistory = new int[codeLength];
        for (int i = 0; i < codeLength; i++)
        {
            p1TypeHistory[i] = -1;
            p2TypeHistory[i] = -1;
        }

        GameEvents.P1NewBasicCode.AddListener(NewP1BasicCode);
        GameEvents.P2NewBasicCode.AddListener(NewP2BasicCode);
        GameEvents.P1NewSpecialCode.AddListener(NewP1SpecialCode);
        GameEvents.P2NewSpecialCode.AddListener(NewP2SpecialCode);

        GameEvents.P1KeyPress.AddListener(P1KeyPress);
        GameEvents.P2KeyPress.AddListener(P2KeyPress);

        GameEvents.P1Heal.AddListener(P1Heal);
        GameEvents.P2Heal.AddListener(P2Heal);
        GameEvents.P1TakeDamage.AddListener(P1TakeDamage);
        GameEvents.P2TakeDamage.AddListener(P2TakeDamage);

        p1WinLights.SetActive(false);
        p2WinLights.SetActive(false);

        StartCoroutine(P1SpecialAbilityCDHandler());
        StartCoroutine(P2SpecialAbilityCDHandler());
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


    #region UI

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

    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }


    void ShowMenu()
    {
        menuCanvas.SetActive(true);

        optionsPanel.SetActive(true);
        optionsDefaultButton.Select();
        confirmLeavePanel.SetActive(false);
        winPanel.SetActive(false);

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


    void ShowWinScreen()
    {
        if (winner == 0)
        {
            p1ConsecutiveVictories++;

            p2ConsecutiveVictories = 0;
        }
        else if (winner == 1)
        {
            p2ConsecutiveVictories++;
            p1ConsecutiveVictories = 0;
        }
        else
            print("Unrecognized winner id: " + winner);

        int highestPoints = Mathf.Max(p1ConsecutiveVictories, p2ConsecutiveVictories);
        pointsText.text = highestPoints + " win" + (highestPoints == 1 ? "!" : "s in a row!");

        input.Gameplay.Disable();
        input.UI.Enable();
        idleText.gameObject.SetActive(false);
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(winWait);

        float t = 0;
        CanvasGroup canvas = winLights.GetComponent<CanvasGroup>();
        winLights.SetActive(true);
        while (t < curtainFadeInDuration)
        {
            t += Time.deltaTime;
            canvas.alpha = t / curtainFadeInDuration;
            yield return null;
        }

        yield return new WaitForSeconds(menuShowupWait);

        menuCanvas.SetActive(true);
        optionsPanel.SetActive(false);
        confirmLeavePanel.SetActive(false);

        winPanel.SetActive(true);
        RectTransform rect = winPanel.GetComponent<RectTransform>();
        Vector2 desiredPosition = new Vector2(1 - winner, 0.5f);
        rect.anchorMin = desiredPosition;
        rect.anchorMax = desiredPosition;
        rect.pivot = desiredPosition;
        rect.anchoredPosition = Vector3.zero;
        winDefaultButton.Select();

        yield return new WaitForSeconds(maxIdleTimeAfterWictory);

        idleText.gameObject.SetActive(true);
        for (int i = 10; i >= 0; i--)
        {
            idleText.text = "Leaving in " + i + "...";
            yield return new WaitForSeconds(2);
        }

        ConfirmLeave();

        yield return null;
    }

    #endregion


    #region Code handling

    void NewP1BasicCode(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            p1BasicCode[i] = code[i];
    }

    void NewP2BasicCode(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            p2BasicCode[i] = code[i];
    }

    void NewP1SpecialCode(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            p1SpecialCode[i] = code[i];
    }

    void NewP2SpecialCode(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            p2SpecialCode[i] = code[i];
    }


    void P1KeyPress(int key)
    {
        p1TypeHistory[3] = p1TypeHistory[2];
        p1TypeHistory[2] = p1TypeHistory[1];
        p1TypeHistory[1] = p1TypeHistory[0];
        p1TypeHistory[0] = key;

        GameEvents.P1NewTypeHistory.Invoke(p1TypeHistory);

        if (CheckAgainsP1Basic(p1TypeHistory))
        {
            GameEvents.P1NewOwnBasicWord.Invoke();

            ResetP1Typebox();
        }

        if (CheckAgainsP1Special(p1TypeHistory))
        {
            GameEvents.P1OwnSpecialWordCompleted.Invoke();

            ResetP1Typebox();
            HideSpecialAbilityP1();
        }

        if (CheckAgainsP2Basic(p1TypeHistory))
        {
            GameEvents.P1NewEnemyBasicWord.Invoke();
            GameEvents.P2NewTypeHistory.Invoke(p2TypeHistory);

            ResetP1Typebox();
        }

        if (CheckAgainsP2Special(p1TypeHistory))
        {
            GameEvents.P1EnemySpecialWordCompleted.Invoke();
            GameEvents.P2NewTypeHistory.Invoke(p2TypeHistory);

            ResetP1Typebox();
            HideSpecialAbilityP2();
        }
    }

    void P2KeyPress(int key)
    {
        p2TypeHistory[3] = p2TypeHistory[2];
        p2TypeHistory[2] = p2TypeHistory[1];
        p2TypeHistory[1] = p2TypeHistory[0];
        p2TypeHistory[0] = key;

        GameEvents.P2NewTypeHistory.Invoke(p2TypeHistory);

        if (CheckAgainsP2Basic(p2TypeHistory))
        {
            GameEvents.P2NewOwnBasicWord.Invoke();

            ResetP2Typebox();
        }

        if (CheckAgainsP2Special(p2TypeHistory))
        {
            GameEvents.P2OwnSpecialWordCompleted.Invoke();

            ResetP2Typebox();
            HideSpecialAbilityP2();
        }

        if (CheckAgainsP1Basic(p2TypeHistory))
        {
            GameEvents.P2NewEnemyBasicWord.Invoke();
            GameEvents.P1NewTypeHistory.Invoke(p1TypeHistory);

            ResetP2Typebox();
        }

        if (CheckAgainsP1Special(p2TypeHistory))
        {
            GameEvents.P2EnemySpecialWordCompleted.Invoke();
            GameEvents.P1NewTypeHistory.Invoke(p1TypeHistory);

            ResetP2Typebox();
            HideSpecialAbilityP1();
        }
    }


    void P1Heal(int amount)
    {
        p1lvl = Mathf.Min(p1lvl + amount, maxLevel);

        GameEvents.P1LvlChange.Invoke(p1lvl);

        if (p1lvl == maxLevel)
        {
            GameEvents.P1Wins.Invoke();

            winLights = p1WinLights;
            winner = 0;
            ShowWinScreen();
        }
    }

    void P1TakeDamage(int amount)
    {
        p1lvl = Mathf.Max(p1lvl - amount, 0);

        GameEvents.P1LvlChange.Invoke(p1lvl);

        if (p1lvl == 0)
        {
            GameEvents.P2Wins.Invoke();

            winLights = p2WinLights;
            winner = 1;
            ShowWinScreen();
        }
    }

    void P2Heal(int amount)
    {
        p2lvl = Mathf.Min(p2lvl + amount, maxLevel);

        GameEvents.P2LvlChange.Invoke(p2lvl);

        if (p2lvl == maxLevel)
        {
            GameEvents.P2Wins.Invoke();

            winLights = p2WinLights;
            winner = 1;
            ShowWinScreen();
        }
    }

    void P2TakeDamage(int amount)
    {
        p2lvl = Mathf.Max(p2lvl - amount, 0);

        GameEvents.P2LvlChange.Invoke(p2lvl);

        if (p2lvl == 0)
        {
            GameEvents.P1Wins.Invoke();

            winLights = p1WinLights;
            winner = 0;
            ShowWinScreen();
        }
    }


    bool CheckAgainsP1Basic(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p1BasicCode[codeLength - i - 1])
                return false;

        return true;
    }

    bool CheckAgainsP1Special(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p1SpecialCode[codeLength - i - 1])
                return false;

        return true;
    }

    bool CheckAgainsP2Basic(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p2BasicCode[codeLength - i - 1])
                return false;

        return true;
    }

    bool CheckAgainsP2Special(int[] code)
    {
        for (int i = 0; i < codeLength; i++)
            if (code[i] != p2SpecialCode[codeLength - i - 1])
                return false;

        return true;
    }


    void ResetP1Typebox()
    {
        for (int i = 0; i < codeLength; i++)
            p1TypeHistory[i] = -1;

        GameEvents.P1NewTypeHistory.Invoke(p1TypeHistory);
    }

    void ResetP2Typebox()
    {
        for (int i = 0; i < codeLength; i++)
            p2TypeHistory[i] = -1;

        GameEvents.P2NewTypeHistory.Invoke(p2TypeHistory);
    }

    #endregion


    IEnumerator P1SpecialAbilityCDHandler()
    {
        yield return new WaitForSeconds(Random.Range(specialAbilityMinCDTime, specialAbilityMaxCDTime));

        if (winner == -1)
            ShowSpecialAbilityP1();
    }

    IEnumerator P2SpecialAbilityCDHandler()
    {
        yield return new WaitForSeconds(Random.Range(specialAbilityMinCDTime, specialAbilityMaxCDTime));

        if (winner == -1)
            ShowSpecialAbilityP2();
    }


    void ShowSpecialAbilityP1()
    {
        GameEvents.P1ShowSpecialAbility.Invoke();
    }

    void HideSpecialAbilityP1()
    {
        GameEvents.P1HideSpecialAbility.Invoke();

        StartCoroutine(P1SpecialAbilityCDHandler());
    }

    void ShowSpecialAbilityP2()
    {
        GameEvents.P2ShowSpecialAbility.Invoke();
    }

    void HideSpecialAbilityP2()
    {
        GameEvents.P2HideSpecialAbility.Invoke();

        StartCoroutine(P2SpecialAbilityCDHandler());
    }
}
