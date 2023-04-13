using UnityEngine;

public class SpecialCodeKey : CodeKey
{
    protected override void Awake()
    {
        if (player == 1)
        {
            GameEvents.P1OwnSpecialTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P1EnemySpecialTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
        }
        else if (player == 2)
        {
            GameEvents.P2OwnSpecialTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P2EnemySpecialTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
        }
        else
            print(name + ": Unrecognized player index: " + player);


        bgDefault = Resources.Load<Sprite>("CodeKeys/P" + player + "Bg");
        if (bgDefault == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");

        bgPressed = Resources.Load<Sprite>("CodeKeys/P" + player + "BgPressed");
        if (bgPressed == null)
            print("Key image not found: " + "CodeKeys/P" + player + "BgPressed");

        enemyMarkDefault = Resources.Load<Sprite>("CodeKeys/P" + player + "Bg");
        if (enemyMarkDefault == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");

        enemyMarkPressed = Resources.Load<Sprite>("CodeKeys/P" + (3 - player) + "Bg");
        if (enemyMarkPressed == null)
            print("Key image not found: " + "CodeKeys/P" + player + "Bg");
    }
}
