using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum MovementState { IDLE, WALKING, INTERACTING, STUNNED}
    MovementState currentMovementState = MovementState.IDLE;

    //Input
    //InputController inputSystem;

    [Header("Movement")]
    [SerializeField]
    private float speed;
    private float movementDirection;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (currentMovementState)
        {
            case MovementState.IDLE:
                break;
            case MovementState.WALKING:
                Move();
                break;
            case MovementState.INTERACTING:
                break; 
            case MovementState.STUNNED:
                break;
            default:
                break;
                
        }
    }

    private void Move()
    {
        movementDirection = inputSystem.movementInput;
        rb.AddForce(new Vector3(speed * movementDirection, 0f, speed * movementDirection));
    }
}
