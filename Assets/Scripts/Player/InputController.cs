using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    
    PlayerController pController;

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
            pController.objectsController.CheckCanTakeObject();
        }
        else if (obj.action.WasReleasedThisFrame())
        {
            pController.objectsController.ReleaseObject();
        }


    }

    public void OnInteractAction(InputAction.CallbackContext obj) 
    {
        Debug.Log("UwU");
        switch (pController.movementController.currentMovementState)
        {
            case PlayerMovementController.MovementState.WALKING:
            case PlayerMovementController.MovementState.GRABBING:
                if (pController.ladderController.nearToLadder)
                {
                    //Subir a la escalera
                    pController.ladderController.StartClimbLadder();
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

    public void OnUseItemAction(InputAction.CallbackContext obj) 
    {
        if (obj.action.WasPerformedThisFrame())
        {

            //Si es un key Object
            if (pController.objectsController.item != null)
            {
                //Comprobar que objeto llevo en la 
                switch (pController.objectsController.item.GetItem().GetItem())
                {
                    case KeyItems.keyItem.Ladder:
                        //Si hay un sitio para poener una escalera y no hay ninguna puesta
                        if (
                            pController.ladderController.nearestLadder != null
                            && !pController.ladderController.nearestLadder.isLadderPlaced
                            )
                        {
                            //colocar la escalera
                            pController.ladderController.nearestLadder.PlaceLadder(pController.objectsController.item);
                        }
                        break;
                    case KeyItems.keyItem.Axe:
                    case KeyItems.keyItem.ScrewDriver:
                    case KeyItems.keyItem.Hammer:
                        //Hacer la animacion de ataque
                        break;
                    default:
                        break;
                }
            }
            

        }
    }

}
