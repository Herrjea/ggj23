using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodePanel : MonoBehaviour
{
    [SerializeField] int player;

    int codeLength = globals.codeLength;
    Image[] code;
    GameObject[] wrongOutlines;
    GameObject[] rightOutlines;

    Sprite[] keyImages;
    int keyCount = globals.keyCount;

    float removeOutlinesDuration = .5f;
    Coroutine waitCoroutine = null;


    private void Awake()
    {
        code = new Image[codeLength];
        wrongOutlines = new GameObject[codeLength];
        rightOutlines = new GameObject[codeLength];

        Transform tmp;
        for (int i = 0; i < codeLength; i++)
        {
            tmp = transform.GetChild(i);
            rightOutlines[i] = tmp.GetChild(0).gameObject;
            rightOutlines[i].SetActive(false);
            wrongOutlines[i] = tmp.GetChild(1).gameObject;
            wrongOutlines[i].SetActive(false);
            code[i] = tmp.GetChild(2).GetComponent<Image>();
        }

        keyImages = new Sprite[keyCount];
        for (int i = 0; i < keyCount; i++)
            keyImages[i] = Resources.Load<Sprite>("CodeKeys/Key" + i);

        if (player == 1)
        {
            GameEvents.P1NewCode.AddListener(SetNewCode);
            GameEvents.P1RightPress.AddListener(RightPress);
            GameEvents.P1WrongPress.AddListener(WrongPress);
        }
        else if (player == 2)
        {
            GameEvents.P2NewCode.AddListener(SetNewCode);
            GameEvents.P2RightPress.AddListener(RightPress);
            GameEvents.P2WrongPress.AddListener(WrongPress);
        }
        else
            print("Undefined player number on object " + name + ": " + player);
    }


    void SetNewCode(int key1, int key2, int key3, int key4)
    {
        code[0].sprite = keyImages[key1];
        code[1].sprite = keyImages[key2];
        code[2].sprite = keyImages[key3];
        code[3].sprite = keyImages[key4];

        for (int i = 0; i < codeLength; i++)
        {
            rightOutlines[i].SetActive(false);
            wrongOutlines[i].SetActive(false);
        }
    }

    void RightPress(int position)
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
            RemoveOutlines();
        }

        rightOutlines[position].SetActive(true);
    }

    void WrongPress(int position)
    {
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
            RemoveOutlines();
        }

        wrongOutlines[position].SetActive(true);

        waitCoroutine = StartCoroutine(RemoveOutlinesCoroutine());
    }


    IEnumerator RemoveOutlinesCoroutine()
    {
        yield return new WaitForSeconds(removeOutlinesDuration);

        RemoveOutlines();

        waitCoroutine = null;
    }

    void RemoveOutlines()
    {
        for (int i = 0; i < codeLength; i++)
        {
            rightOutlines[i].SetActive(false);
            wrongOutlines[i].SetActive(false);
        }
    }
}
