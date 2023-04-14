using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviour
{
    [SerializeField] int player;
    Animator animator;
    int previousLevel;

    float platypusPlatychance = 0.05f;

    [SerializeField] ParticleSystem lvlUpParticles;
    [SerializeField] ParticleSystem lvlDownParticles;
    ParticleSystem.EmissionModule lvlUpEmission;
    ParticleSystem.EmissionModule lvlDownEmission;
    int initialEmission = 8;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        previousLevel = globals.startingLevel;
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

        lvlUpEmission = lvlUpParticles.emission;
        lvlDownEmission = lvlDownParticles.emission;
    }


    void SetAvatar(int level)
    {
        //print("lvl " + level);
        int difference = Mathf.Abs(level - previousLevel);

        if (level > previousLevel)
        {
            lvlUpEmission.rateOverTime = initialEmission * difference;
            lvlUpParticles.Play();
        }
        else if (level < previousLevel)
        {
            lvlDownEmission.rateOverTime = initialEmission * difference;
            lvlDownParticles.Play();
        }

        previousLevel = level;


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

            if (Random.Range(0.0f, 1.0f) < platypusPlatychance)
            {

                animator.SetTrigger("platypus");
                return;
            }

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
