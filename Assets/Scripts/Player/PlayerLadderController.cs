using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderController : MonoBehaviour
{
    private PlayerController playerController;

    [HideInInspector]
    public WallInteractions nearestLadder;

    [HideInInspector]
    public bool nearToLadder = false;
    private bool goingDown;

    private Vector3 upPos;
    private Vector3[] downPos;

    private int nextPosIndex;
    [SerializeField]
    private float climbSpeed;
    private float climbProcess = 0;
    private float playerY;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        playerY = capsuleCollider.height / 2;
    }

    private void SetLadderPositions(Vector3 _nextUpPos, Transform[] _nextDownPos)
    {
        upPos = new Vector3
              (
              _nextUpPos.x,
              _nextUpPos.y + playerY,
              _nextUpPos.z
              );
        downPos = new Vector3[2];

        downPos[0] = new Vector3
            (
            _nextDownPos[0].position.x,
            _nextDownPos[0].position.y + playerY,
            _nextDownPos[0].position.z
            );
        downPos[1] = new Vector3
            (
            _nextDownPos[1].position.x,
            _nextDownPos[1].position.y + playerY,
            _nextDownPos[1].position.z
            );
    }

    public void StartClimbLadder()
    {
        //Congelamos el movimiento por input del player
        playerController.movementController.ChangeState(PlayerMovementController.MovementState.CLIMBING_LADDER);
        SetLadderPositions(nearestLadder.PlayerUpDestination.position, nearestLadder.PlayerDownDestination);

        //Decidimos a que punto iremos
        float distance1 = Vector3.Distance(transform.position, downPos[0]);
        float distance2 = Vector3.Distance(transform.position, downPos[1]);

        if (distance1 >= distance2)
        {
            nextPosIndex = 1;
        }
        else
        {
            nextPosIndex = 0;
        }

        transform.position = downPos[nextPosIndex];
        climbProcess = 0;
        goingDown = false;
    }
    public void ClimbLadder()
    {
            if (!goingDown)
            {
                climbProcess += climbSpeed * Time.fixedDeltaTime;
                //Movemos al player
                transform.position = Vector3.Lerp(downPos[nextPosIndex], upPos, climbProcess);

                //Comprobamos si ha llegado a su posicion
                if (climbProcess >= 1)
                {
                    //En caso de que este subiendo
                    ChangeDestinyIndex();
                }
            }
            else
            {
                climbProcess += climbSpeed * Time.fixedDeltaTime;
                //Movemos al player
                transform.position = Vector3.Lerp(upPos, downPos[nextPosIndex], climbProcess);

                //Comprobamos si ha llegado a su posicion
                if (climbProcess >= 1)
                {
                    StopClimb();
                }
            }
        
    }
    private void ChangeDestinyIndex() 
    {
        //Cambiaremso el index para que se mueva al siguiente
        if (nextPosIndex == 1)
        {
            nextPosIndex = 0;
        }
        else
        {
            nextPosIndex = 1;
        }
        climbProcess = 0;
        goingDown = true;
    }
    private void StopClimb()
    {
        //Dejamos de escalar
        goingDown = false;
        //Desbloqueamos el movimiento del player
        playerController.movementController.ChangeState(PlayerMovementController.MovementState.WALKING);

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LadderWall"))
        {
            nearestLadder = other.GetComponent<WallInteractions>();
            nearToLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LadderWall"))
        {
            if (nearestLadder == other.GetComponent<WallInteractions>())
            {
                nearestLadder = null;
                nearToLadder = false;
            }
        }
    }
}
