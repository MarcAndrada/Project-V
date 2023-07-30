using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    
    private PlayerController playerController;

    public Vector2 movementInput { get; private set; }


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }



    public void OnMoveAction(InputAction.CallbackContext obj)
    {
        movementInput = obj.ReadValue<Vector2>();
    }

    public void OnGrabAction(InputAction.CallbackContext obj)
    {

        if (obj.action.WasPerformedThisFrame())
        {
            playerController.objectsController.GrabItem();
        }
        else if (obj.action.WasReleasedThisFrame())
        {
            playerController.objectsController.ReleaseItem();
        }



       

    }

    public void OnInteractAction(InputAction.CallbackContext obj) 
    {
        if (obj.action.WasPerformedThisFrame())
        {
            switch (playerController.movementController.currentMovementState)
            {
                case PlayerMovementController.MovementState.WALKING:
                case PlayerMovementController.MovementState.GRABBING_LIGHT:
                    if (playerController.ladderController.nearToLadder && playerController.ladderController.nearestLadder.isLadderPlaced)
                    {
                        //Subir a la escalera
                        playerController.ladderController.StartClimbLadder();
                        break;
                    }
                    break;
                case PlayerMovementController.MovementState.STUNNED:
                    break;
                case PlayerMovementController.MovementState.INTERACTING:
                    break;
                case PlayerMovementController.MovementState.CLIMBING_LADDER:
                    break;
                default:
                    break;
            }
        }
    }

    public void OnUseItemAction(InputAction.CallbackContext obj) 
    {
        
        if (obj.action.WasPerformedThisFrame())
        {
            playerController.objectsController.UseKeyItem();
        }
    }

   

}
