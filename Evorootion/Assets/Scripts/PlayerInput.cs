using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public static bool InteractionRequested = false;

    Input input;

    PlayerMovement movement;


    private void Awake()
    {
        GameEvents.InputSet.AddListener(InputSet);

        movement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        if (input.Gameplay.P1K1.WasPressedThisFrame())
        {
            GameEvents.P1KeyPress.Invoke(0);
        }
        else if (input.Gameplay.P1K2.WasPressedThisFrame())
        {
            GameEvents.P1KeyPress.Invoke(1);
        }
        else if (input.Gameplay.P1K3.WasPressedThisFrame())
        {
            GameEvents.P1KeyPress.Invoke(2);
        }
        else if (input.Gameplay.P1K4.WasPressedThisFrame())
        {
            GameEvents.P1KeyPress.Invoke(3);
        }

        if (input.Gameplay.P2K1.WasPressedThisFrame())
        {
            GameEvents.P2KeyPress.Invoke(0);
        }
        else if (input.Gameplay.P2K2.WasPressedThisFrame())
        {
            GameEvents.P2KeyPress.Invoke(1);
        }
        else if (input.Gameplay.P2K3.WasPressedThisFrame())
        {
            GameEvents.P2KeyPress.Invoke(2);
        }
        else if (input.Gameplay.P2K4.WasPressedThisFrame())
        {
            GameEvents.P2KeyPress.Invoke(3);
        }
    }


    void InputSet(Input input)
    {
        this.input = input;
    }
}
