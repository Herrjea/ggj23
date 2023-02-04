using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPanel : MonoBehaviour
{
    RectTransform rect;
    Vector2 hiddenPosition;
    Vector2 shownPosition;

    [SerializeField] AnimationCurve showAnimation;
    [SerializeField] AnimationCurve hideAnimation;
    [SerializeField] float animationDuration = 1;

    Coroutine coroutine = null;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        hiddenPosition = rect.anchoredPosition;
        shownPosition = Vector2.zero;

        GameEvents.ShowControls.AddListener(ShowControls);
        GameEvents.HideControls.AddListener(HideControls);
    }


    void ShowControls()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(MovePanel(rect.anchoredPosition, shownPosition, showAnimation));
    }

    void HideControls()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(MovePanel(rect.anchoredPosition, hiddenPosition, hideAnimation));
    }


    IEnumerator MovePanel(Vector2 from, Vector2 to, AnimationCurve ease)
    {
        float t = 0;

        while (t < animationDuration)
        {
            rect.anchoredPosition = Vector2.LerpUnclamped(
                from,
                to,
                ease.Evaluate(t / animationDuration)
            );

            t += Time.deltaTime;
            yield return null;
        }

        coroutine = null;
    }
}
