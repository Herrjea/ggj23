using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCodePanel : CodePanel
{
    protected override void Awake()
    {
        //base.Awake();

        keyImages = new Sprite[keyCount];
        for (int i = 0; i < keyCount; i++)
        {
            keyImages[i] = Resources.Load<Sprite>("CodeKeys/Key" + i);
            if (keyImages[i] == null)
                Debug.LogError("Key image not found: " + "CodeKeys/Key" + i);
        }


        intCode = new int[codeLength];
        ResetCode();

        code = new Image[codeLength];

        Transform tmp;
        for (int i = 0; i < codeLength; i++)
        {
            tmp = transform.GetChild(i);
            code[i] = tmp.GetChild(1).GetComponent<Image>();
        }

        // For shake purposes
        rectTransform = GetComponent<RectTransform>();
        startingPos = rectTransform.anchoredPosition;


        if (player == 1)
        {
            GameEvents.P1NewSpecialCode.AddListener(SetNewCode);
            GameEvents.P1NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P2NewTypeHistory.AddListener(NewEnemyTypeHistory);

            GameEvents.P1OwnSpecialWordCompleted.AddListener(SelfCompletedWord);
            GameEvents.P2EnemySpecialWordCompleted.AddListener(EnemyCompletedWord);
        }
        else if (player == 2)
        {
            GameEvents.P2NewSpecialCode.AddListener(SetNewCode);
            GameEvents.P2NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P1NewTypeHistory.AddListener(NewEnemyTypeHistory);

            GameEvents.P2OwnSpecialWordCompleted.AddListener(SelfCompletedWord);
            GameEvents.P1EnemySpecialWordCompleted.AddListener(EnemyCompletedWord);
        }
        else
            print("Undefined player number on object " + name + ": " + player);


        abilities = new List<Ability>();
        abilities.Add(new ThrashAbility(player));
    }


    protected override void NewP1OwnTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P1OwnSpecialTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected override void NewP1EnemyTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P1EnemySpecialTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected override void NewP2OwnTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P2OwnSpecialTypeHistoryDisplay.Invoke(pressedKeys);
    }

    protected override void NewP2EnemyTypeHistoryDisplay(bool[] pressedKeys)
    {
        GameEvents.P2EnemySpecialTypeHistoryDisplay.Invoke(pressedKeys);
    }
}
