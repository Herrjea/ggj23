using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    GameObject[] p1Bar;
    Material[] p1Materials;
    GameObject[] p2Bar;
    Material[] p2Materials;

    int wedges = globals.levelWedges;


    private void Awake()
    {
        GameEvents.P1LvlChange.AddListener(P1LvlChange);
        GameEvents.P2LvlChange.AddListener(P2LvlChange);

        Transform tmp;
        

        p1Bar = new GameObject[wedges];
        p1Materials = new Material[wedges];
        tmp = transform.GetChild(0);
        for (int i = 0; i < wedges; i++)
        {
            p1Bar[i] = tmp.GetChild(wedges - i - 1).gameObject;
            p1Materials[i] = p1Bar[i].GetComponent<RawImage>().material;
        }

        p2Bar = new GameObject[wedges];
        p2Materials = new Material[wedges];
        tmp = transform.GetChild(1);
        for (int i = 0; i < wedges; i++)
        {
            p2Bar[i] = tmp.GetChild(wedges - i - 1).gameObject;
            p2Materials[i] = p2Bar[i].GetComponent<RawImage>().material;
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
                p1Materials[i].SetFloat("_Cutoff", 1);
            }
            else
                p1Bar[i].gameObject.SetActive(false);

        }


        if (lvl % globals.stepsPerLevel != 0)
        {
            p1Materials[lvl / globals.stepsPerLevel].SetFloat("_Cutoff", NormalizeCutoff(1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel)));
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
                p1Materials[lvl / globals.stepsPerLevel].SetFloat("_Cutoff", 0f);
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
                p2Materials[i].SetFloat("_Cutoff", 1);
            }
            else
                p2Bar[i].gameObject.SetActive(false);

        }


        if (lvl % globals.stepsPerLevel != 0)
        {
            p2Materials[lvl / globals.stepsPerLevel].SetFloat("_Cutoff", NormalizeCutoff(1.0f / globals.stepsPerLevel * ((lvl) % globals.stepsPerLevel)));
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
                p2Materials[lvl / globals.stepsPerLevel].SetFloat("_Cutoff", 0f);
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
