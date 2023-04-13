using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogeGeneration : MonoBehaviour
{
    int[] code;

    int codeLength = globals.codeLength;


    private void Awake()
    {
        GameEvents.P1OwnBasicWordCompleted.AddListener(P1BasicWordCompleted);
        GameEvents.P1OwnSpecialWordCompleted.AddListener(P1SpecialWordCompleted);
        GameEvents.P2EnemyBasicWordCompleted.AddListener(P1BasicWordCompleted);
        GameEvents.P2EnemySpecialWordCompleted.AddListener(P1SpecialWordCompleted);

        GameEvents.P2OwnBasicWordCompleted.AddListener(P2BasicWordCompleted);
        GameEvents.P2OwnSpecialWordCompleted.AddListener(P2SpecialWordCompleted);
        GameEvents.P1EnemyBasicWordCompleted.AddListener(P2BasicWordCompleted);
        GameEvents.P1EnemySpecialWordCompleted.AddListener(P2SpecialWordCompleted);

        code = new int[codeLength];
    }


    private void Start()
    {
        P1BasicWordCompleted();
        P1SpecialWordCompleted();
        P2BasicWordCompleted();
        P2SpecialWordCompleted();
    }


    void GenerateNewCode()
    {
        for (int i = 0; i < codeLength; i++)
        {
            code[i] = Random.Range(0, codeLength);
        }
    }

    void P1BasicWordCompleted()
    {
        GenerateNewCode();
        GameEvents.P1NewBasicCode.Invoke(code);
    }

    void P1SpecialWordCompleted()
    {
        GenerateNewCode();
        GameEvents.P1NewSpecialCode.Invoke(code);
    }

    void P2BasicWordCompleted()
    {
        GenerateNewCode();
        GameEvents.P2NewBasicCode.Invoke(code);
    }

    void P2SpecialWordCompleted()
    {
        GenerateNewCode();
        GameEvents.P2NewSpecialCode.Invoke(code);
    }
}
