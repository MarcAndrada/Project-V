using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    InputActionReference moveAction;

    [SerializeField]
    InputActionReference grabbing;
    
    PlayerController pController;

    //ObjectController oController;

    public Vector2 movementInput { get; private set; }


    private void Awake()
    {
        pController = GetComponent<PlayerController>();

        moveAction.action.started += MoveAction;
        moveAction.action.performed += MoveAction;
        moveAction.action.canceled += MoveAction;

        grabbing.action.started += GrabbingAction;

    }

    private void OnDestroy()
    {
        moveAction.action.started -= MoveAction;
        moveAction.action.performed -= MoveAction;
        moveAction.action.canceled -= MoveAction;

        grabbing.action.started -= GrabbingAction;
    }

    private void MoveAction(InputAction.CallbackContext obj)
    {
        movementInput = moveAction.action.ReadValue<Vector2>();
    }

    private void GrabbingAction(InputAction.CallbackContext obj)
    {
        pController.Interact();
    }
}
