using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] int player;
    Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();

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
        //print("lvl " + level);

        if (level == globals.maxLevel)
        {
            //print("max lvl reached");
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

            //print("setting wedge " + wedge);

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
                    animator.SetTrigger("lvl2");
                    break;
                case 5:
                    animator.SetTrigger("lvl1");
                    break;
                case 6:
                    animator.SetTrigger("lvl0");
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
