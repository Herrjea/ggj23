using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals
{
    public static int codeLength = 4;

    public static int levelWedges = 7;
    public static int stepsPerLevel = 2;
    public static int maxLevel = levelWedges * stepsPerLevel;
    public static int startingLevel = maxLevel - 1;// maxLevel / 2;

    public static int keyCount = 4;
}
