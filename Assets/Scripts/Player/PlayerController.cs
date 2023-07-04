using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum MovementState { WALKING, GRABBING, STUNNED }
    public MovementState currentMovementState = MovementState.WALKING;

    //Input
    InputController inputSystem;

    [Header("Movement")]
    [SerializeField]
    private float walkingSpeed;
    [SerializeField]
    private float grabbingSpeed;
    private float speed;
    [SerializeField]
    private float acceleration;
    public float speedPenalty { private get; set; }

    [SerializeField]
    private Vector2 movementDirection;


    private Rigidbody rb;

    [SerializeField]
    private float knockback;

    BoxCollider objectCollider;

    


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputController>();
        objectCollider = GetComponent<BoxCollider>();
        objectCollider.enabled = false;
        ChangeState(MovementState.WALKING);
    }

    void Update()
    {
        movementDirection = inputSystem.movementInput;
    }

    private void FixedUpdate()
    {
        switch (currentMovementState)
        {
            case MovementState.WALKING:
                Walking();
                break;
            case MovementState.GRABBING:
                Grabbing();
                break;
            case MovementState.STUNNED:
                break;
            default:
                break;
        }
    }

    private void Walking()
    {
        Move();
    }

    private void Grabbing()
    {
        Move();
    }

    private void Stunned()
    {
        if (rb.velocity.magnitude < 1f)
            ChangeState(MovementState.WALKING);
        else
        {
            Invoke("Stunned", 0.2f);
        }
    }

    private void Acceleration()
    {
        if (movementDirection.x >= 0.01f || movementDirection.y >= 0.01f)
        {
            acceleration += Time.deltaTime + 0.06f;
            if (acceleration >= 1f)
                acceleration = 1f;

        }
        else if (movementDirection.x <= -0.1f || movementDirection.y <= -0.1f)
        {
            acceleration += Time.deltaTime;
            if (acceleration >= 1f)
                acceleration = 1f;
        }
        else if (movementDirection.x <= 0.01f && movementDirection.y <= 0.01f || movementDirection.x >= 0.01f && movementDirection.y >= 0.01f)
        {
            acceleration -= Time.deltaTime;
            if (acceleration <= 0f)
                acceleration = 0f;
        }
    }

    private void Move()
    {
        Acceleration();
        rb.velocity = new Vector3(speed * movementDirection.x  * acceleration * Time.deltaTime, 0f, speed * movementDirection.y * acceleration * Time.deltaTime);
    }
    private void Move(float speedToReduce)
    {
        float reducedSpeed = speed - speedToReduce;
        Acceleration();
        rb.velocity = new Vector3(reducedSpeed * movementDirection.x * Time.deltaTime, 0f, reducedSpeed * movementDirection.y * Time.deltaTime);
    }
    

    private void HitByFire(Vector3 fireCenter)
    {
        Vector3 vectorToKnockBack = fireCenter - transform.position;
        ChangeState(MovementState.STUNNED);
        rb.velocity = Vector3.zero;
        rb.AddForce(vectorToKnockBack.normalized * knockback, ForceMode.Impulse);
        Invoke("Stunned", 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Vector3 fireCenter = other.transform.position;
            HitByFire(fireCenter);
        }
    }

    public void ChangeState(MovementState currentState)
    {
        switch (currentMovementState)
        {
            case MovementState.WALKING:
                break;
            case MovementState.GRABBING:
                objectCollider.enabled = false;
                break;
            case MovementState.STUNNED:

                break;
            default:
                break;
        }

        switch (currentState)
        {
            case MovementState.WALKING:
                speed = walkingSpeed;
                break;
            case MovementState.GRABBING:
                speed = grabbingSpeed;
                objectCollider.enabled = true;
                break;
            case MovementState.STUNNED:

                break;
            default:
                break;
        }
        currentMovementState = currentState;
    }

    public void Interact()
    {
        if (currentMovementState == MovementState.WALKING)
        {
            ChangeState(MovementState.GRABBING);
        }
        else if (currentMovementState == MovementState.GRABBING)
        {
            ChangeState(MovementState.WALKING);
        }
    }
}
