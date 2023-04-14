using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogeGeneration : MonoBehaviour
{
    int[] code;

    int codeLength = globals.codeLength;


    private void Awake()
    {
        GameEvents.P1NewOwnBasicWord.AddListener(P1NewBasicWord);
        GameEvents.P2NewEnemyBasicWord.AddListener(P1NewBasicWord);

        GameEvents.P2NewOwnBasicWord.AddListener(P2NewBasicWord);
        GameEvents.P1NewEnemyBasicWord.AddListener(P2NewBasicWord);

        GameEvents.P1ShowSpecialAbility.AddListener(P1NewSpecialWord);
        GameEvents.P2ShowSpecialAbility.AddListener(P2NewSpecialWord);

        code = new int[codeLength];
    }


    private void Start()
    {
        P1NewBasicWord();
        P2NewBasicWord();
    }


    void GenerateNewCode()
    {
        for (int i = 0; i < codeLength; i++)
        {
            code[i] = Random.Range(0, codeLength);
        }
    }

    void P1NewBasicWord()
    {
        GenerateNewCode();
        GameEvents.P1NewBasicCode.Invoke(code);
    }

    void P1NewSpecialWord()
    {
        GenerateNewCode();
        GameEvents.P1NewSpecialCode.Invoke(code);
    }

    void P2NewBasicWord()
    {
        GenerateNewCode();
        GameEvents.P2NewBasicCode.Invoke(code);
    }

    void P2NewSpecialWord()
    {
        GenerateNewCode();
        GameEvents.P2NewSpecialCode.Invoke(code);
    }
}
