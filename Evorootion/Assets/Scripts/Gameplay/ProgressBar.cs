using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    GameObject[] p1Bar;
    Image[] p1Images;
    GameObject[] p2Bar;
    Image[] p2Images;

    int wedges = globals.levelWedges;


    private void Awake()
    {
        GameEvents.P1LvlChange.AddListener(P1LvlChange);
        GameEvents.P2LvlChange.AddListener(P2LvlChange);

        Transform tmp;
        

        p1Bar = new GameObject[wedges];
        p1Images = new Image[wedges];
        tmp = transform.GetChild(0);
        for (int i = 0; i < wedges; i++)
        {
            p1Bar[i] = tmp.GetChild(wedges - i - 1).gameObject;
            p1Images[i] = p1Bar[i].GetComponent<Image>();
        }

        p2Bar = new GameObject[wedges];
        p2Images = new Image[wedges];
        tmp = transform.GetChild(1);
        for (int i = 0; i < wedges; i++)
        {
            p2Bar[i] = tmp.GetChild(wedges - i - 1).gameObject;
            p2Images[i] = p2Bar[i].GetComponent<Image>();
        }

        P1LvlChange(globals.startingLevel);
        P2LvlChange(globals.startingLevel);
    }


    void P1LvlChange(int lvl)
    {
        for(int i = 0; i < wedges; i++)
        {
            if (i <= lvl / globals.stepsPerLevel)
            {
                p1Bar[i].gameObject.SetActive(true);
                p1Images[i].fillAmount = 1;
            }
            else
                p1Bar[i].gameObject.SetActive(false);

        }


        if (lvl % globals.stepsPerLevel != 0)
        {
            p1Images[lvl / globals.stepsPerLevel].fillAmount =
                NormalizeCutoff(1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel));
            //print(
            //    "lvl " + lvl + ": " +
            //    (lvl / globals.stepsPerLevel) +
            //    " al " +
            //    1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel)
            //);
        }
        else
        {
            if (lvl / globals.stepsPerLevel < wedges)
            {
                p1Images[lvl / globals.stepsPerLevel].fillAmount = 0;
                //print(
                //    "lvl " + lvl + ": " +
                //    (lvl / globals.stepsPerLevel) +
                //    " al " +
                //    (0.1f)
                //);
            }
        }
    }

    void P2LvlChange(int lvl)
    {
        for (int i = 0; i < wedges; i++)
        {
            if (i <= lvl / globals.stepsPerLevel)
            {
                p2Bar[i].gameObject.SetActive(true);
                p2Images[i].fillAmount = 1;
            }
            else
                p2Bar[i].gameObject.SetActive(false);

        }


        if (lvl % globals.stepsPerLevel != 0)
        {
            p2Images[lvl / globals.stepsPerLevel].fillAmount =
                NormalizeCutoff(1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel));
            //print(
            //    "lvl " + lvl + ": " +
            //    (lvl / globals.stepsPerLevel) +
            //    " al " +
            //    (1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel))
            //);
        }
        else
        {
            if (lvl / globals.stepsPerLevel < wedges)
            {
                p2Images[lvl / globals.stepsPerLevel].fillAmount = 0;
                //print(
                //    "lvl " + lvl + ": " +
                //    (lvl / globals.stepsPerLevel) +
                //    " al " +
                //    (0.1f)
                //);
            }
        }
    }


    float NormalizeCutoff(float cutoff)
    {
        float margin = 0.15f;

        return cutoff * (1 - margin * 2) + margin;
    }
}
