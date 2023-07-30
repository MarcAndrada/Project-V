using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private Collider itemCollider;
    [Space, SerializeField]
    private Transform handPoint;

    private bool pickedObject = false;

    [Space, SerializeField]
    private float areaToSearch;

    [Space, SerializeField]
    private LayerMask objectLayer; 
    private Rigidbody pickedObjectRB;
    private Collider pickedObjectCollider;
    [Space, SerializeField]
    private float throwForce;

    private Vector3 scale;


    private PlayerController playerController;
    

    public MoveItem handItem { get; private set; }

    private void Awake()
    {
        //interactions = GameObject.FindObjectOfType<WallInteractions>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && pickedObject)
        {
            playerController.movementController.ChangeState(PlayerMovementController.MovementState.THROWNING_ITEM);       
        }
        if(Input.GetKeyUp(KeyCode.C) && pickedObject) 
        {
            playerController.movementController.ChangeState(PlayerMovementController.MovementState.WALKING);
            pickedObjectRB.transform.SetParent(null);
            pickedObjectRB.isKinematic = false;
            pickedObjectRB.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            pickedObjectCollider.enabled = true;
            pickedObjectRB.transform.localScale = scale;
            pickedObject = false;
        }
    }
    private void CheckCanTakeObject() 
    {
        Collider nearestItemCollider = CheckNearestObject();
        if (nearestItemCollider != null)
        {
            GameObject nearestItem = nearestItemCollider.gameObject;
            TakeNearestObject(nearestItem, nearestItemCollider);
        }
    }
    private Collider CheckNearestObject()
    {
        Collider _nearestItem = null;
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Collider[] items = Physics.OverlapSphere(transform.position, areaToSearch, objectLayer);

        float smallestDistance = areaToSearch;
        foreach (Collider item in items)
        {
            Vector2 itemPos = new Vector2(item.transform.position.x, item.transform.position.z);
            float currentDistance = Vector2.Distance(itemPos, playerPos);
            if (smallestDistance > currentDistance)
            {
                smallestDistance = currentDistance;
                _nearestItem = item;

            }
        }
        return _nearestItem;
    }
    private void CheckLadderAttached(MoveItem _nearestItem)
    {
        if(_nearestItem.GetItem().GetItem() == MovingItems.Item.Ladder && handItem.AtachedWall()  != null)
        {
            handItem.AtachedWall().SetLadder(false);
            handItem.SetWall(null);
        }
    }

    private void TakeNearestObject(GameObject _nearestItem, Collider _nearestItemCollider)
    {
        MoveItem currentItem;
        if (_nearestItem.TryGetComponent<MoveItem>(out currentItem))
        {
            handItem = currentItem;
        }
        else
        {
            handItem = _nearestItem.GetComponentInParent<MoveItem>();
        }

        if (!handItem.picked)
        {
            pickedObject = true;
            CheckLadderAttached(handItem);

            switch (handItem.GetItem().GetItemType())
            {
                case MovingItems.ItemType.light:
                case MovingItems.ItemType.key:
                    _nearestItem.transform.position = handPoint.position;
                    // Desactivar collision y fisicas
                    pickedObjectRB = _nearestItem.GetComponent<Rigidbody>();
                    pickedObjectCollider = _nearestItem.GetComponent<Collider>();
                    pickedObjectRB.isKinematic = true;
                    pickedObjectCollider.enabled = false;
                    scale = _nearestItem.transform.localScale;
                    pickedObjectRB.rotation = handPoint.rotation;
                    handItem.picked = true;
                    // Hacerlo hijo
                    _nearestItem.transform.SetParent(handPoint.transform);
                    break;
                case MovingItems.ItemType.heavy:
                    HeavyItemsController heavyItem = (HeavyItemsController)handItem;
                    heavyItem.AddPlayer(playerController.rb, _nearestItemCollider);
                    
                    break;
                default:
                    break;
            }
        }
    }

    public void ReleaseObject()
    {
        if (pickedObject)
        {
            handItem.picked = false;
            pickedObject = false;

            switch (handItem.GetItem().GetItemType())
            {
                case MovingItems.ItemType.light:
                case MovingItems.ItemType.key:
                    // Activar collision y fisicas y dejar de ser hijo 
                    pickedObjectRB.isKinematic = false;
                    pickedObjectCollider.enabled = true;
                    pickedObjectRB.transform.localScale = scale;
                    pickedObjectRB.transform.SetParent(null);
                    break;
                case MovingItems.ItemType.heavy:
                    HeavyItemsController heavyItem = (HeavyItemsController)handItem;
                    heavyItem.RemovePlayer(playerController.rb);
                    break;

                default:
                    break;
            }
            
            pickedObjectRB = null;
            pickedObjectCollider = null;
            handItem = null;
        }

    }

    public bool GetPickedObject()
    {
        return pickedObject; 
    }

    public MoveItem GetItems()
    {
        return handItem;
    }

    public void GrabItem() 
    {
        switch (playerController.movementController.currentMovementState)
        {
            case PlayerMovementController.MovementState.WALKING:
                CheckCanTakeObject();
                if (playerController.objectsController.handItem != null)
                {
                    switch (playerController.objectsController.handItem.GetItem().GetItemType())
                    {
                        case MovingItems.ItemType.heavy:
                            //playerController.movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_HEAVY_S);
                            break;
                        case MovingItems.ItemType.light:
                        case MovingItems.ItemType.key:
                            playerController.movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_LIGHT);
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void ReleaseItem()
    {
        switch (playerController.movementController.currentMovementState)
        {
            case PlayerMovementController.MovementState.GRABBING_HEAVY_S:
            case PlayerMovementController.MovementState.GRABBING_HEAVY_M:
            case PlayerMovementController.MovementState.GRABBING_LIGHT:

                playerController.objectsController.ReleaseObject();
                playerController.movementController.ChangeState(PlayerMovementController.MovementState.WALKING);
                break;
            default:
                break;
        }
    }

    public void UseKeyItem() 
    {
        //Si es un key Object
        if (handItem != null)
        {
            //Comprobar que objeto llevo en la 
            switch (handItem.GetItem().GetItem())
            {
                case MovingItems.Item.Ladder:
                    //Si hay un sitio para poener una escalera y no hay ninguna puesta
                    if (
                        playerController.ladderController.nearestLadder != null
                        && !playerController.ladderController.nearestLadder.isLadderPlaced
                        )
                    {
                        //colocar la escalera
                        playerController.ladderController.nearestLadder.PlaceLadder(handItem);
                    }
                    break;
                case MovingItems.Item.Axe:
                case MovingItems.Item.ScrewDriver:
                case MovingItems.Item.Hammer:
                    //Aqui la animación
                    EnableKeyItemCollider();

                    break;
                default:
                    break;
            }
        }
    }

    public void EnableKeyItemCollider() 
    {
        itemCollider.enabled = true;
        Invoke("DisableKeyItemCollider", 0.3f);
    }

    private void DisableKeyItemCollider()
    {
        itemCollider.enabled = false;
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaToSearch);
    }

}
