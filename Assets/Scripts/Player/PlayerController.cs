using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovementController movementController { get; private set; }
    public TakeObjects objectsController { get; private set; }
    public InputController inputController { get; private set; }
    public PlayerLadderController ladderController { get; private set; }

    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        objectsController = GetComponent<TakeObjects>();
        inputController = GetComponent<InputController>();
        ladderController = GetComponent<PlayerLadderController>();
    }

    
}
