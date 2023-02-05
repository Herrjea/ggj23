using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] int player;
    Animator animator;

    //Image playerAvatarUI;
    //Sprite[] playerAvatars;
    int wedges = globals.levelWedges;


    private void Awake()
    {
        print(globals.maxLevel);

        animator = GetComponent<Animator>();
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
        print("lvl " + level);

        if (level == globals.maxLevel)
        {
            print("max lvl reached");
            animator.SetTrigger("lvl8");
            return;
        }
        else
        {
            int wedge =
            level % globals.stepsPerLevel == 0
            ?
            level / globals.stepsPerLevel
            :
            level / globals.stepsPerLevel + 1;

            //playerAvatarUI.sprite = playerAvatars[wedge];
            print("setting wedge " + wedge);

            switch (wedge)
            {
                case 0:
                    animator.SetTrigger("lvl7");
                    break;
                case 1:
                    animator.SetTrigger("lvl6");
                    break;
                case 2:
                    animator.SetTrigger("lvl5");
                    break;
                case 3:
                    animator.SetTrigger("lvl4");
                    break;
                case 4:
                    animator.SetTrigger("lvl3");
                    break;
                case 5:
                    animator.SetTrigger("lvl2");
                    break;
                case 6:
                    animator.SetTrigger("lvl1");
                    break;
                case 7:
                    animator.SetTrigger("lvl0");
                    break;
                default:
                    break;
            }
        }

        
    }
}
