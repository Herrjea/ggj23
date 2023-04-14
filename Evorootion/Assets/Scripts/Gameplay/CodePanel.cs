using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    [SerializeField] protected int player;

    protected int codeLength = globals.codeLength;
    protected Image[] code;
    protected int[] intCode;

    protected Sprite[] keyImages;
    protected int keyCount = globals.keyCount;

    protected float removeOutlinesDuration = .5f;
    protected Coroutine waitCoroutine = null;

    protected RectTransform rectTransform;
    protected Vector2 startingPos;
    [SerializeField] protected float shakeDuration = .2f, shakeAmplitude = 5;

    protected List<Ability> abilities;
    protected int currentAbility = 0;


    protected virtual void Awake()
    {
        keyImages = new Sprite[keyCount];
        for (int i = 0; i < keyCount; i++)
        {
            keyImages[i] = Resources.Load<Sprite>("CodeKeys/Key" + i);
            if (keyImages[i] == null)
                Debug.LogError("Key image not found: " + "CodeKeys/Key" + i);
        }


        intCode = new int[codeLength];
        ResetCode();

        code = new Image[codeLength];

        Transform tmp;
        for (int i = 0; i < codeLength; i++)
        {
            tmp = transform.GetChild(i);
            code[i] = tmp.GetChild(1).GetComponent<Image>();
        }


        // For shake purposes
        rectTransform = GetComponent<RectTransform>();
        startingPos = rectTransform.anchoredPosition;


        if (player == 1)
        {
            GameEvents.P1NewBasicCode.AddListener(SetNewCode);
            GameEvents.P1NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P2NewTypeHistory.AddListener(NewEnemyTypeHistory);

            GameEvents.P1NewOwnBasicWord.AddListener(SelfCompletedWord);
            GameEvents.P2NewEnemyBasicWord.AddListener(EnemyCompletedWord);
        }
        else if (player == 2)
        {
            GameEvents.P2NewBasicCode.AddListener(SetNewCode);
            GameEvents.P2NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P1NewTypeHistory.AddListener(NewEnemyTypeHistory);

            GameEvents.P2NewOwnBasicWord.AddListener(SelfCompletedWord);
            GameEvents.P1NewEnemyBasicWord.AddListener(EnemyCompletedWord);
        }
        else
            print("Undefined player number on object " + name + ": " + player);


        abilities = new List<Ability>();
        abilities.Add(new DefaultAbility(player));
    }


    protected void SelfCompletedWord()
    {
        abilities[currentAbility].SelfTrigger();
    }

    protected void EnemyCompletedWord()
    {
        abilities[currentAbility].EnemyTrigger();
    }


    protected void SetNewCode(int[] newCode)
    {
        for (int i = 0; i < codeLength; i++)
        {
            code[i].sprite = keyImages[newCode[i]];
            intCode[i] = newCode[i];
        }

        StartCoroutine(Shake());
    }


    protected void NewOwnTypeHistory(int[] pressed)
    {
        CheckCode(pressed, true);

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

    protected void NewEnemyTypeHistory(int[] pressed)
    {
        CheckCode(pressed, false);

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
    protected void CheckCode(int[] pressed, bool own)
    {
        int[] reversed = ShiftAndReverse(pressed);
        bool[] pressedKeys;

        // Check for 4 matches
        if (
            intCode[0] == reversed[0] &&
            intCode[1] == reversed[1] &&
            intCode[2] == reversed[2] &&
            intCode[3] == reversed[3]
        )
        {
            pressedKeys = new bool[] { true, true, true, true };
            if (player == 1)
            {
                if (own)
                    NewP1OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP1EnemyTypeHistoryDisplay(pressedKeys);
            }
            else
            {
                if (own)
                    NewP2OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP2EnemyTypeHistoryDisplay(pressedKeys);
            }

            return;
        }

        // Check for 3 matches
        if (
            intCode[0] == reversed[1] &&
            intCode[1] == reversed[2] &&
            intCode[2] == reversed[3]
        )
        {
            pressedKeys = new bool[] { true, true, true, false };
            if (player == 1)
            {
                if (own)
                    NewP1OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP1EnemyTypeHistoryDisplay(pressedKeys);
            }
            else
            {
                if (own)
                    NewP2OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP2EnemyTypeHistoryDisplay(pressedKeys);
            }

            return;
        }

        // Check for 2 matches
        if (
            intCode[0] == reversed[2] &&
            intCode[1] == reversed[3]
        )
        {
            pressedKeys = new bool[] { true, true, false, false };
            if (player == 1)
            {
                if (own)
                    NewP1OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP1EnemyTypeHistoryDisplay(pressedKeys);
            }
            else
            {
                if (own)
                    NewP2OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP2EnemyTypeHistoryDisplay(pressedKeys);
            }

            return;
        }

        // Check for 1 match
        if (
            intCode[0] == reversed[3]
        )
        {
            pressedKeys = new bool[] { true, false, false, false };
            if (player == 1)
            {
                if (own)
                    NewP1OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP1EnemyTypeHistoryDisplay(pressedKeys);
            }
            else
            {
                if (own)
                    NewP2OwnTypeHistoryDisplay(pressedKeys);
                else
                    NewP2EnemyTypeHistoryDisplay(pressedKeys);
            }

            return;
        }

        pressedKeys = new bool[] { false, false, false, false };
        if (player == 1)
        {
            if (own)
                NewP1OwnTypeHistoryDisplay(pressedKeys);
            else
                NewP1EnemyTypeHistoryDisplay(pressedKeys);
        }
        else
        {
            if (own)
                NewP2OwnTypeHistoryDisplay(pressedKeys);
            else
                NewP2EnemyTypeHistoryDisplay(pressedKeys);
        }
    }


    protected virtual void NewP1OwnTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P1OwnBasicTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected virtual void NewP1EnemyTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P1EnemyBasicTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected virtual void NewP2OwnTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P2OwnBasicTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected virtual void NewP2EnemyTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P2EnemyBasicTypeHistoryDisplay.Invoke(pressedKeys);
    }


    // Crappy code ahead
    protected int[] ShiftAndReverse(int[] typeHistory)
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


    protected void ResetCode()
    {
        for (int i = 0; i < codeLength; i++)
            intCode[i] = -1;
    }


    protected IEnumerator Shake()
    {
        float t = 0;
        while (t < shakeDuration)
        {
            rectTransform.anchoredPosition = new Vector2(
                startingPos.x + Random.Range(-shakeAmplitude, shakeAmplitude),
                startingPos.y + Random.Range(-shakeAmplitude, shakeAmplitude)
            );

            t += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = startingPos;
    }
}
