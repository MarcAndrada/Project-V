using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    
    PlayerController pController;

    //ObjectController oController;

    public Vector2 movementInput { get; private set; }


    private void Awake()
    {
        pController = GetComponent<PlayerController>();



    }



    public void OnMoveAction(InputAction.CallbackContext obj)
    {
        movementInput = obj.ReadValue<Vector2>();
    }

    public void OnGrabAction(InputAction.CallbackContext obj)
    {
        if (obj.action.WasPerformedThisFrame())
        {
            pController.Interact();
        }
    }
}
