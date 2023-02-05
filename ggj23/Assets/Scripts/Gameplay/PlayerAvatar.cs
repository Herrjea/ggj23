using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] int player;

    //Image playerAvatarUI;
    //Sprite[] playerAvatars;
    int wedges = globals.levelWedges;


    private void Awake()
    {
        //playerAvatarUI = transform.GetChild(1).GetComponent<Image>();

        //playerAvatars = new Sprite[wedges + 1];
        //for (int i = 0; i < wedges + 1; i++)
        //{
        //    playerAvatars[i] = Resources.Load<Sprite>("PlayerAvatars/PlayerAvatar" + i);
        //}

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


    void SetAvatar(int level)
    {
        int wedge =
            level % globals.stepsPerLevel == 0
            ?
            level / globals.stepsPerLevel
            :
            level / globals.stepsPerLevel + 1;

        //playerAvatarUI.sprite = playerAvatars[wedge];
        print("setting wedge " + wedge);
    }
}
