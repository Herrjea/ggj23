using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    [SerializeField] int player;

    int codeLength = globals.codeLength;
    Image[] code;
    int[] intCode;

    Sprite[] keyImages;
    int keyCount = globals.keyCount;

    Sprite bgDefault, bgPressed;
    Image[] bg;

    Sprite enemyMarkDefault, enemyMarkTyped;
    Image[] enemyMark;

    float removeOutlinesDuration = .5f;
    Coroutine waitCoroutine = null;

    float timeSinceLastStep = 0;
    [SerializeField] float stepDuration = .4f;


    private void Awake()
    {
        keyImages = new Sprite[keyCount];
        for (int i = 0; i < keyCount; i++)
        {
            keyImages[i] = Resources.Load<Sprite>("CodeKeys/Key" + i);
            if (keyImages[i] == null)
                print("Key image not found: " + "CodeKeys/Key" + i);
        }

        bgDefault = Resources.Load<Sprite>("CodeKeys/P" + player + "Bg");
        if (bgDefault == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");

        bgPressed = Resources.Load<Sprite>("CodeKeys/P" + player + "BgPressed");
        if (bgPressed == null)
            print("Key image not found: " + "CodeKeys/P" + player + "BgPressed");

        enemyMarkDefault = Resources.Load<Sprite>("CodeKeys/P" + player + "Bg");
        if (enemyMarkDefault == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");

        enemyMarkTyped = Resources.Load<Sprite>("CodeKeys/P" + (3 - player) + "Bg");
        if (enemyMarkTyped == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");


        intCode = new int[codeLength];
        ResetCode();

        code = new Image[codeLength];
        bg = new Image[codeLength];
        enemyMark = new Image[codeLength];

        Transform tmp;
        for (int i = 0; i < codeLength; i++)
        {
            tmp = transform.GetChild(i);

            enemyMark[i] = tmp.GetChild(2).GetComponent<Image>();
            enemyMark[i].sprite = enemyMarkDefault;

            code[i] = tmp.GetChild(1).GetComponent<Image>();

            bg[i] = tmp.GetChild(0).GetComponent<Image>();
            bg[i].sprite = bgDefault;
        }

        
        if (player == 1)
        {
            GameEvents.P1NewCode.AddListener(SetNewCode);
            GameEvents.P1RightPress.AddListener(RightPress);
            GameEvents.P1WrongPress.AddListener(WrongPress);
            GameEvents.P1NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P2NewTypeHistory.AddListener(NewEnemyTypeHistory);
        }
        else if (player == 2)
        {
            GameEvents.P2NewCode.AddListener(SetNewCode);
            GameEvents.P2RightPress.AddListener(RightPress);
            GameEvents.P2WrongPress.AddListener(WrongPress);
            GameEvents.P2NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P1NewTypeHistory.AddListener(NewEnemyTypeHistory);
        }
        else
            print("Undefined player number on object " + name + ": " + player);
    }


    void SetNewCode(int[] newCode)
    {
        for (int i = 0; i < codeLength; i++)
        {
            code[i].sprite = keyImages[newCode[i]];
            intCode[i] = newCode[i];
            bg[i].sprite = bgDefault;
            enemyMark[i].sprite = enemyMarkDefault;
        }
    }

    
    void NewOwnTypeHistory(int[] pressed)
    {
        CheckCode(pressed, bg, bgDefault, bgPressed);

        //bool wellTyped = true;
        //bool[] debugWellTyped = new bool[4];

        //for (int i = 0; i < codeLength; i++)
        //{
        //    if (reversed[i] == intCode[i] && wellTyped)
        //    {
        //        bg[i].sprite = bgPressed;
        //        debugWellTyped[i] = true;
        //    }
        //    else
        //    {
        //        bg[i].sprite = bgDefault;
        //        wellTyped = false;
        //        debugWellTyped[i] = false;
        //    }
        //}

        //print(name + " P" + player + ": " + debugWellTyped[0] + " " + debugWellTyped[1] + " " + debugWellTyped[2] + " " + debugWellTyped[3]);
        //print("pressed: " + pressed[0] + " " + pressed[1] + " " + pressed[2] + " " + pressed[3]);
        //print("reversed: " + reversed[0] + " " + reversed[1] + " " + reversed[2] + " " + reversed[3]);
        //print("intCode: " + intCode[0] + " " + intCode[1] + " " + intCode[2] + " " + intCode[3]);

        //int[] shifted = ShiftToTail(pressed);
    }

    void NewEnemyTypeHistory(int[] pressed)
    {
        CheckCode(pressed, enemyMark, enemyMarkDefault, enemyMarkTyped);

        //bool wellTyped = true;
        //bool[] debugWellTyped = new bool[4];

        //for (int i = 0; i < codeLength; i++)
        //{
        //    if (reversed[i] == intCode[i] && wellTyped)
        //    {
        //        bg[i].sprite = bgPressed;
        //        debugWellTyped[i] = true;
        //    }
        //    else
        //    {
        //        bg[i].sprite = bgDefault;
        //        wellTyped = false;
        //        debugWellTyped[i] = false;
        //    }
        //}

        //print(name + " P" + player + ": " + debugWellTyped[0] + " " + debugWellTyped[1] + " " + debugWellTyped[2] + " " + debugWellTyped[3]);
        //print("pressed: " + pressed[0] + " " + pressed[1] + " " + pressed[2] + " " + pressed[3]);
        //print("reversed: " + reversed[0] + " " + reversed[1] + " " + reversed[2] + " " + reversed[3]);
        //print("intCode: " + intCode[0] + " " + intCode[1] + " " + intCode[2] + " " + intCode[3]);

        //int[] shifted = ShiftToTail(pressed);
    }

    // Crappy code ahead.
    // found a way to parametrize it and make it way smarter in way fewer lines,
    // but was completely unreadable.
    void CheckCode(int[] pressed, Image[] image, Sprite defaultSprite, Sprite pressedSprite)
    {
        int[] reversed = ShiftAndReverse(pressed);

        // Check for 4 matches
        if (
            intCode[0] == reversed[0] &&
            intCode[1] == reversed[1] &&
            intCode[2] == reversed[2] &&
            intCode[3] == reversed[3]
        )
        {
            image[0].sprite = image[1].sprite = image[2].sprite = image[3].sprite = pressedSprite;
            return;
        }

        // Check for 3 matches
        if (
            intCode[0] == reversed[1] &&
            intCode[1] == reversed[2] &&
            intCode[2] == reversed[3]
        )
        {
            image[0].sprite = image[1].sprite = image[2].sprite = pressedSprite;
            image[3].sprite = defaultSprite;
            return;
        }

        // Check for 2 matches
        if (
            intCode[0] == reversed[2] &&
            intCode[1] == reversed[3]
        )
        {
            image[0].sprite = image[1].sprite = pressedSprite;
            image[2].sprite = image[3].sprite = defaultSprite;
            return;
        }

        // Check for 1 match
        if (
            intCode[0] == reversed[3]
        )
        {
            image[0].sprite = pressedSprite;
            image[1].sprite = image[2].sprite = image[3].sprite = defaultSprite;
            return;
        }

        image[0].sprite = image[1].sprite = image[2].sprite = image[3].sprite = defaultSprite;
    }

    //bool CheckForMatches(int n, int[] code, int[] reversed)
    //{
    //    for (int i = 0; i < n; i++)
    //        if (code[i] != reversed[codeLength - n + i])
    //            return false;

    //    return true;
    //}

    // Crappy code ahead
    int[] ShiftAndReverse(int[] typeHistory)
    {
        int[] reversed = new int[codeLength];
        for (int i = 0; i < codeLength; i++)
            reversed[i] = -1;

        if (typeHistory[0] == -1)
        {
            return reversed;
        }

        if (typeHistory[1] == -1)
        {
            reversed[3] = typeHistory[0];
            return reversed;
        }

        if (typeHistory[2] == -1)
        {
            reversed[2] = typeHistory[1];
            reversed[3] = typeHistory[0];
            return reversed;
        }

        if (typeHistory[3] == -1)
        {
            reversed[1] = typeHistory[2];
            reversed[2] = typeHistory[1];
            reversed[3] = typeHistory[0];
            return reversed;
        }

        reversed[0] = typeHistory[3];
        reversed[1] = typeHistory[2];
        reversed[2] = typeHistory[1];
        reversed[3] = typeHistory[0];

        return reversed;
    }


    void RightPress(int position)
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
            RemoveOutlines();
        }

        //rightOutlines[position].SetActive(true);
    }

    void WrongPress(int position)
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
            RemoveOutlines();
        }

        //wrongOutlines[position].SetActive(true);

        waitCoroutine = StartCoroutine(RemoveOutlinesCoroutine());
    }

    void ResetCode()
    {
        for (int i = 0; i < codeLength; i++)
            intCode[i] = -1;
    }

    IEnumerator RemoveOutlinesCoroutine()
    {
        yield return new WaitForSeconds(removeOutlinesDuration);

        RemoveOutlines();

        waitCoroutine = null;
    }

    void RemoveOutlines()
    {
        for (int i = 0; i < codeLength; i++)
        {
            //rightOutlines[i].SetActive(false);
            //wrongOutlines[i].SetActive(false);
        }
    }
}
