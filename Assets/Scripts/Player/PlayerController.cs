using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum MovementState { IDLE, WALKING, INTERACTING, STUNNED}
    public MovementState currentMovementState = MovementState.IDLE;

    //Input
    InputController inputSystem;

    [Header("Movement")]
    [SerializeField]
    private float speed;

    private Vector2 movementDirection;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputController>();
    }
 
    void Update()
    {
        movementDirection = inputSystem.movementInput;
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
        //rb.AddForce(new Vector3(speed * movementDirection.x, 0f, speed * movementDirection.y));
        rb.velocity = new Vector3(speed * movementDirection.x * Time.deltaTime, 0f, speed * movementDirection.y * Time.deltaTime);
    }
}
