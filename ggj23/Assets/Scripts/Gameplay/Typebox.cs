using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typebox : MonoBehaviour
{
    [SerializeField] int player;

    Sprite[] keyImages;
    int keyCount = globals.keyCount;

    int codeLength = globals.codeLength;

    Image[] typeHistory;
    int typeboxLength = 0;


    private void Awake()
    {
        keyImages = new Sprite[keyCount];
        for (int i = 0; i < keyCount; i++)
            keyImages[i] = Resources.Load<Sprite>("CodeKeys/Key" + i);

        if (player == 1)
        {
            GameEvents.P1KeyPress.AddListener(KeyPress);
            GameEvents.P1OwnWordCompleted.AddListener(ResetTypebox);
            GameEvents.P1EnemyWordCompleted.AddListener(ResetTypebox);
        }
        else if (player == 2)
        {
            GameEvents.P2KeyPress.AddListener(KeyPress);
            GameEvents.P2OwnWordCompleted.AddListener(ResetTypebox);
            GameEvents.P2EnemyWordCompleted.AddListener(ResetTypebox);
        }
        else
            print("Undefined player number on object " + name + ": " + player);


        typeHistory = new Image[codeLength];
        for (int i = 0; i < codeLength; i++)
        {
            typeHistory[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }

        ResetTypebox();
    }


    void KeyPress(int key)
    {
        if (typeboxLength < codeLength)
        {
            for (int i = codeLength - typeboxLength; i < codeLength; i++)
            {
                typeHistory[i - 1].sprite = typeHistory[i].sprite;
            }

            // Make the new one visible
            typeHistory[codeLength - typeboxLength - 1].color = new Vector4(1, 1, 1, 1);

            // Place the newly pressed key
            typeHistory[codeLength - 1].sprite = keyImages[key];

            typeboxLength++;
        }
        else
        {
            typeHistory[0].sprite = typeHistory[1].sprite;
            typeHistory[1].sprite = typeHistory[2].sprite;
            typeHistory[2].sprite = typeHistory[3].sprite;
            typeHistory[3].sprite = keyImages[key];
        }
    }


    void ResetTypebox()
    {
        for (int i = 0; i < codeLength; i++)
            typeHistory[i].color = new Vector4(1, 1, 1, 0);

        typeboxLength = 0;
    }

    void PrintFullTypebox()
    {
        print(
            (typeHistory[0].sprite == null ? "#" : typeHistory[0].sprite.name) + " " +
            (typeHistory[1].sprite == null ? "#" : typeHistory[1].sprite.name) + " " +
            (typeHistory[2].sprite == null ? "#" : typeHistory[2].sprite.name) + " " +
            (typeHistory[3].sprite == null ? "#" : typeHistory[3].sprite.name)
        );
    }
}
