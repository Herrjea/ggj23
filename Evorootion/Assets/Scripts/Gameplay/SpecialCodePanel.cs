using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCodePanel : CodePanel
{
    Vector3 originalSize;
    [SerializeField] AnimationCurve animationEase;
    [SerializeField] float animationDuration = .5f;


    protected override void Awake()
    {
        //base.Awake();

        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.localScale;

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


        abilityIcon = transform.GetChild(4).transform.GetChild(0).GetComponent<Image>();


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

            GameEvents.P1HideSpecialAbility.AddListener(HidePanel);
            GameEvents.P1ShowSpecialAbility.AddListener(ShowPanel);
        }
        else if (player == 2)
        {
            GameEvents.P2NewSpecialCode.AddListener(SetNewCode);
            GameEvents.P2NewTypeHistory.AddListener(NewOwnTypeHistory);
            GameEvents.P1NewTypeHistory.AddListener(NewEnemyTypeHistory);

            GameEvents.P2OwnSpecialWordCompleted.AddListener(SelfCompletedWord);
            GameEvents.P1EnemySpecialWordCompleted.AddListener(EnemyCompletedWord);

            GameEvents.P2HideSpecialAbility.AddListener(HidePanel);
            GameEvents.P2ShowSpecialAbility.AddListener(ShowPanel);
        }
        else
            print("Undefined player number on object " + name + ": " + player);


        abilities = new List<Ability>();
        abilities.Add(new MicroplasticSludgeAbility(player));
        abilities.Add(new PoisonousDietAbility(player));
        abilities.Add(new DnaDebuggingAbility(player));
        abilities.Add(new DnaRemovalAbility(player));
        abilities.Add(new QuillsAbility(player));
        abilities.Add(new NicheDietAbility(player));
        abilities.Add(new GenomeDuplicationAbility(player));


        // Start hidden
        rectTransform.localScale = Vector3.zero;
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


    void HidePanel()
    {
        StartCoroutine(AnimationCoroutine(false));
    }

    void ShowPanel()
    {
        StartCoroutine(AnimationCoroutine(true));

        currentAbility = Random.Range(
            0,
            abilities.Count
        );
        abilityIcon.sprite = abilities[currentAbility].icon;

        print("Setting " + abilities[currentAbility].GetType().ToString() + " for player " + player);
    }

    IEnumerator AnimationCoroutine(bool showing)
    {
        float t = 0;

        while (t < animationDuration)
        {
            t += Time.deltaTime;

            rectTransform.localScale = Vector3.LerpUnclamped(
                Vector3.zero,
                originalSize,
                animationEase.Evaluate(
                    showing ?
                        t / animationDuration
                        :
                        1 - t / animationDuration
                )
            );

            yield return null;
        }
    }
}
