using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogeGeneration : MonoBehaviour
{
    Input input;

    int[] code;

    int codeLength = globals.codeLength;


    private void Awake()
    {
        GameEvents.InputSet.AddListener(InputSet);

        GameEvents.P1OwnWordCompleted.AddListener(P1WordCompleted);
        GameEvents.P2EnemyWordCompleted.AddListener(P1WordCompleted);

        GameEvents.P2OwnWordCompleted.AddListener(P2WordCompleted);
        GameEvents.P1EnemyWordCompleted.AddListener(P2WordCompleted);

        code = new int[codeLength];
    }


    private void Start()
    {
        GenerateCodeFor(1);
        GenerateCodeFor(2);
    }


    void GenerateCodeFor(int player)
    {
        for (int i = 0; i < codeLength; i++)
        {
            code[i] = Random.Range(0, codeLength);
        }

        if (player == 1)
            GameEvents.P1NewCode.Invoke(code[0], code[1], code[2], code[3]);
        else
            GameEvents.P2NewCode.Invoke(code[0], code[1], code[2], code[3]);
    }

    void P1WordCompleted()
    {
        GenerateCodeFor(1);
        GameEvents.P1NewCode.Invoke(code[0], code[1], code[2], code[3]);
    }

    void P2WordCompleted()
    {
        GenerateCodeFor(2);
        GameEvents.P2NewCode.Invoke(code[0], code[1], code[2], code[3]);
    }


    void InputSet(Input input)
    {
        this.input = input;
    }
}
