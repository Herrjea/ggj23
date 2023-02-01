using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOnSelect : MonoBehaviour, ISelectHandler
{
    Button button;

    TitleScreenManager titleScreenManager;


    private void Awake()
    {
        button = GetComponent<Button>();

        titleScreenManager = GameObject.Find("TitleScreenManager").GetComponent<TitleScreenManager>();
    }


    public void OnSelect(BaseEventData eventData)
    {
        ((ISelectHandler)button).OnSelect(eventData);

        titleScreenManager.HideControls();
    }
}
