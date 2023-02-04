using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] int player;

    Image playerAvatarUI;
    Sprite[] playerAvatars;
    int levels = globals.maxLevel;


    private void Awake()
    {
        playerAvatarUI = transform.GetChild(1).GetComponent<Image>();

        playerAvatars = new Sprite[levels + 1];
        for (int i = 0; i < levels + 1; i++)
        {
            playerAvatars[i] = Resources.Load<Sprite>("PlayerAvatars/PlayerAvatar" + i);
        }

        SetAvatar(globals.startingLevel);

        if (player == 1)
        {
            GameEvents.P1LvlChange.AddListener(SetAvatar);
        }
        else if (player == 2)
        {
            GameEvents.P2LvlChange.AddListener(SetAvatar);
        }
        else
            print("Undefined player number on object " + name + ": " + player);
    }


    void SetAvatar(int index)
    {
        playerAvatarUI.sprite = playerAvatars[index];
        transform.localScale = Vector3.one * (1 + 0.1f * index);
    }
}
