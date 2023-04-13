using UnityEngine;
using UnityEngine.UI;

public class CodeKey : MonoBehaviour
{
    [SerializeField] protected int player;
    [SerializeField] protected int key;

    protected bool ownPressed = false;
    protected bool enemyPressed = false;

    protected Sprite bgDefault, bgPressed;
    [SerializeField] protected Image bg;

    protected Sprite enemyMarkDefault, enemyMarkPressed;
    [SerializeField] protected Image enemyMark;


    protected virtual void Awake()
    {
        if (player == 1)
        {
            GameEvents.P1OwnBasicTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P1EnemyBasicTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
        }
        else if (player == 2)
        {
            GameEvents.P2OwnBasicTypeHistoryDisplay.AddListener(OwnKeyUpdate);
            GameEvents.P2EnemyBasicTypeHistoryDisplay.AddListener(EnemyKeyUpdate);
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


    protected void OwnKeyUpdate(bool[] currentlyPressed)
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

    protected void EnemyKeyUpdate(bool[] currentlyPressed)
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


    protected void TurnOwnOn()
    {
        bg.sprite = bgPressed;
        //print("p" + player + " own " + key + " on");
    }

    protected void TurnOwnOff()
    {
        bg.sprite = bgDefault;
        //print("p" + player + " own " + key + " off");
    }


    protected void TurnEnemyOn()
    {
        enemyMark.sprite = enemyMarkPressed;
        //print("p" + player + " enemy " + key + " on");
    }

    protected void TurnEnemyOff()
    {
        enemyMark.sprite = enemyMarkDefault;
        //print("p" + player + " enemy " + key + " off");
    }
}
