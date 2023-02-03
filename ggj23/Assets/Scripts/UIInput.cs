using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UIInput : MonoBehaviour
{
    public static bool InteractionRequested = false;

    Input input;

    PlayerMovement movement;


    private void Awake()
    {
        input = new Input();
        input.UI.Disable();

        movement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (input.UI.Up.WasPerformedThisFrame())
            ;
        else if (input.UI.Down.WasPerformedThisFrame())
            ;
        else if (input.UI.Left.WasPerformedThisFrame())
            ;
        else if (input.UI.Right.WasPerformedThisFrame())
            ;

        else if (input.UI.Interact.WasPerformedThisFrame())
            GameEvents.UIInteractionRequested.Invoke();

        else if (input.UI.Back.WasPerformedThisFrame())
            GameEvents.UIBackRequested.Invoke();
    }
}
