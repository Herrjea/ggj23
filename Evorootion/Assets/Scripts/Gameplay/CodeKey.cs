using UnityEngine;
using UnityEngine.UI;

public class CodeKey : MonoBehaviour
{
    [SerializeField] int player;
    [SerializeField] int key;

    bool ownPressed = false;
    bool enemyPressed = false;

    Sprite bgDefault, bgPressed;
    [SerializeField] Image bg;

    Sprite enemyMarkDefault, enemyMarkPressed;
    [SerializeField] Image enemyMark;


    private void Awake()
    {
        if (player == 1)
        {
            GameEvents.P1OwnTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P1EnemyTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
        }
        else if (player == 2)
        {
            GameEvents.P2OwnTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P2EnemyTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
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


    void OwnKeyUpdate(bool[] currentlyPressed)
    {
        if (currentlyPressed[key] != ownPressed)
        {
            if (currentlyPressed[key])
                TurnOwnOn();
            else
                TurnOwnOff();
        }

        ownPressed = currentlyPressed[key];
        bg.sprite = ownPressed ? bgPressed : bgDefault;
    }

    void EnemyKeyUpdate(bool[] currentlyPressed)
    {
        if (currentlyPressed[key] != enemyPressed)
        {
            if (currentlyPressed[key])
                TurnEnemyOn();
            else
                TurnEnemyOff();
        }

        enemyPressed = currentlyPressed[key];
        enemyMark.sprite = enemyPressed ? enemyMarkPressed : enemyMarkDefault;
    }


    void TurnOwnOn()
    {
        bg.sprite = bgPressed;
        //print("p" + player + " own " + key + " on");
    }

    void TurnOwnOff()
    {
        bg.sprite = bgDefault;
        //print("p" + player + " own " + key + " off");
    }


    void TurnEnemyOn()
    {
        enemyMark.sprite = enemyMarkPressed;
        //print("p" + player + " enemy " + key + " on");
    }

    void TurnEnemyOff()
    {
        enemyMark.sprite = enemyMarkDefault;
        //print("p" + player + " enemy " + key + " off");
    }
}
