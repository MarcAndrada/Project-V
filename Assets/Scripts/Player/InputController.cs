using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerMovementController;

public class InputController : MonoBehaviour
{
    
    PlayerController pController;

    public Vector2 movementInput { get; private set; }

    [SerializeField]
    private Collider itemCollider;


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
        switch (pController.movementController.currentMovementState)
        {
            case PlayerMovementController.MovementState.WALKING:
            case PlayerMovementController.MovementState.GRABBING_HEAVY_S:
            case PlayerMovementController.MovementState.GRABBING_HEAVY_M:
            case PlayerMovementController.MovementState.GRABBING_LIGHT:
                if (obj.action.WasPerformedThisFrame())
                {
                    pController.objectsController.CheckCanTakeObject();
                    if (pController.objectsController.item != null)
                    {
                        switch (pController.objectsController.item.GetItem().GetType())
                        {
                            case MovingItems.ItemType.heavy:
                                pController.movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_HEAVY_S);
                                break;
                            case MovingItems.ItemType.light:                             
                            case MovingItems.ItemType.key:
                                pController.movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_LIGHT);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (obj.action.WasReleasedThisFrame())
                {
                    pController.objectsController.ReleaseObject();
                    pController.movementController.ChangeState(PlayerMovementController.MovementState.WALKING);
                }
                break;
            default:
                break;
        }

    }

    public void OnInteractAction(InputAction.CallbackContext obj) 
    {
        Debug.Log("UwU");
        switch (pController.movementController.currentMovementState)
        {
            case PlayerMovementController.MovementState.WALKING:
            case PlayerMovementController.MovementState.GRABBING_LIGHT:
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
                    case MovingItems.Item.Ladder:
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
                    case MovingItems.Item.Axe:
                    case MovingItems.Item.ScrewDriver:
                    case MovingItems.Item.Hammer:
                        //Aqui la animación
                        itemCollider.enabled = true;
                        Invoke("DisableCollider", 0.3f);
                        break;
                    default:
                        break;
                }
            }
            

        }
    }

    private void DisableCollider()
    {
        itemCollider.enabled = false;
    }
}
