using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    InputActionReference moveAction;
    
    PlayerController controller;

    public Vector2 movementInput { get; private set; }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();

        moveAction.action.started += MoveAction;
        moveAction.action.performed += MoveAction;
        moveAction.action.canceled += MoveAction;

    }

    private void OnDestroy()
    {
        moveAction.action.started -= MoveAction;
        moveAction.action.performed -= MoveAction;
        moveAction.action.canceled -= MoveAction;
    }

    private void MoveAction(InputAction.CallbackContext obj)
    {
        movementInput = moveAction.action.ReadValue<Vector2>();
    }
}
