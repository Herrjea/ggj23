using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typebox : MonoBehaviour
{
    [SerializeField] int player;

    int codeLength = globals.codeLength;

    Image[] typeHistory;


    private void Awake()
    {
        if (player == 1)
        {
            GameEvents.P1KeyPress.AddListener(KeyPress);
        }
        else if (player == 2)
        {
            GameEvents.P2KeyPress.AddListener(KeyPress);
        }
        else
            print("Undefined player number on object " + name + ": " + player);


        typeHistory = new Image[codeLength];
        for (int i = 0; i < codeLength; i++)
        {

        }
    }


    void KeyPress(int key)
    {

    }
}
