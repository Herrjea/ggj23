using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    GameObject[] p1Bar;
    GameObject[] p2Bar;

    int maxLevel = globals.maxLevel;


    private void Awake()
    {
        GameEvents.P1LvlChange.AddListener(P1LvlChange);
        GameEvents.P2LvlChange.AddListener(P2LvlChange);

        Transform tmp;

        p1Bar = new GameObject[maxLevel];
        tmp = transform.GetChild(0);
        for (int i = 0; i < maxLevel; i++)
        {
            p1Bar[i] = tmp.GetChild(maxLevel - i - 1).gameObject;
        }

        p2Bar = new GameObject[maxLevel];
        tmp = transform.GetChild(1);
        for (int i = 0; i < maxLevel; i++)
        {
            p2Bar[i] = tmp.GetChild(maxLevel - i - 1).gameObject;
        }

        P1LvlChange(globals.startingLevel);
        P2LvlChange(globals.startingLevel);
    }


    void P1LvlChange(int lvl)
    {
        for(int i = 0; i < maxLevel; i++)
        {
            p1Bar[i].gameObject.SetActive(i < lvl);
        }
    }

    void P2LvlChange(int lvl)
    {
        for (int i = 0; i < maxLevel; i++)
        {
            p2Bar[i].gameObject.SetActive(i < lvl);
        }
    }
}
