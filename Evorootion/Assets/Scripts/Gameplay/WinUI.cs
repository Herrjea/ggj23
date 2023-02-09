using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] int player;


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        if (player == 1)
        {
            GameEvents.P1Wins.AddListener(ShowUI);
        }
        else if (player == 2)
        {
            GameEvents.P2Wins.AddListener(ShowUI);
        }
        else
            print("Undefined player number on object " + name + ": " + player);
    }


    void ShowUI()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);
    }
}
