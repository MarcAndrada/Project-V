using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovementController movementController { get; private set; }
    public ObjectController objectsController { get; private set; }
    public InputController inputController { get; private set; }
    public PlayerLadderController ladderController { get; private set; }

    public Rigidbody rb { get; private set; }


    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        objectsController = GetComponent<ObjectController>();
        inputController = GetComponent<InputController>();
        ladderController = GetComponent<PlayerLadderController>();


        rb = GetComponent<Rigidbody>();
    }

    
}
