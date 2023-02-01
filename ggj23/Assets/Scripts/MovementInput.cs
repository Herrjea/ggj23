using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementInput : MonoBehaviour
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
        movement.MoveDirection = input.Movement.Move.ReadValue<Vector2>();

        //if (input.Movement.Dash.WasPressedThisFrame())
        //    movement.DashRequested();

        if (input.Movement.Interact.WasPerformedThisFrame())
            GameEvents.InteractionRequested.Invoke();
    }


    void InputSet(Input input)
    {
        this.input = input;
    }
}
