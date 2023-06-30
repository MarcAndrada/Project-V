using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum MovementState { WALKING, INTERACTING, STUNNED}
    public MovementState currentMovementState = MovementState.WALKING;

    //Input
    InputController inputSystem;

    [Header("Movement")]
    [SerializeField]
    private float speed;

    private Vector2 movementDirection;


    private Rigidbody rb;

    [SerializeField]
    private float knockback;

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
            case MovementState.WALKING:
                Move();
                break;
            case MovementState.INTERACTING:
                break; 
            case MovementState.STUNNED:
                Stunned();
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

    private void HitByFire(Vector3 fireCenter)
    {
        Vector3 vectorToKnockBack = fireCenter - this.transform.position;
        //this.transform.position = this.transform.position - vectorToKnockBack;
        currentMovementState = MovementState.STUNNED;
        rb.AddForce(vectorToKnockBack.normalized * knockback, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Fire"))
        {
            Vector3 fireCenter = collision.transform.position;
            HitByFire(fireCenter);
        }
    }

    private void Stunned()
    {
        if(rb.velocity.sqrMagnitude < 0.1f)
        {
            currentMovementState = MovementState.WALKING;
        }
    }
}
