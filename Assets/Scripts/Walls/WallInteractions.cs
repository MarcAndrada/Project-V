using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WallInteractions : MonoBehaviour
{
    public enum WallType { DESTRUCTIBLE, STAIRS };

    public WallType wallType;

    [SerializeField]
    KeyItems necesaryItem;

    [SerializeField]
    private GameObject necesaryObjectCanvas;

    [SerializeField]
    private GameObject walls;

    [SerializeField]
    private Transform LadderDestination;

    [SerializeField]
    private Transform PlayerUpDestination;

    [SerializeField]
    private Transform PlayerDownDestination;

    [SerializeField]
    private Transform PlayerDownDestination2;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private bool isUpLadder; //Esto estara en el player controller

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool isLadderPlaced;

    [SerializeField]
    private bool canInteract;

    private TakeObjects player;

    private Keyitem item;

    private bool takingLadder = false;
    private bool takefirstRoute = false;
    private bool takeSecondRoute = false;
    private bool takeThirdRoute = false;

    private void Awake()
    {
        player = FindObjectOfType<TakeObjects>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterTheWall();
        }
        
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            ExitTheWall();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canInteract)
        {
            switch (wallType)
            {
                case WallType.DESTRUCTIBLE:
                    DestroyWall();
                    break;
                case WallType.STAIRS:
                    if (!isLadderPlaced)
                    {
                        PlaceStairs();
                        item.StopPhysics();
                    }
                    break;
                default:
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && isLadderPlaced && canInteract && !takingLadder)
        {
            TakeLadder();
        } 

        if(takingLadder)
        {
            Moving();
        }
    } 

    private void PlaceStairs()
    {
        necesaryObjectCanvas.SetActive(false);
        item.transform.position = LadderDestination.position;
        isLadderPlaced = true;
        player.ReleaseObject();
        necesaryObjectCanvas.SetActive(true);
    }

    private void DestroyWall()
    {
        necesaryObjectCanvas.SetActive(false);
        Destroy(gameObject);
    }

    private void TakeLadder()   
    {
        takingLadder = true;
        takefirstRoute = true;
    }

    private void EnterTheWall()
    {
        item = player.GetItems();
        necesaryObjectCanvas.SetActive(true);

        if (necesaryItem.GetItem() == item.GetItem().GetItem())
        {
            switch (necesaryItem.GetItem())
            {

                case KeyItems.keyItem.Ladder:
                    if (!isLadderPlaced)
                    {
                        canInteract = true;
                    }
                    break;
                case KeyItems.keyItem.Axe:
                case KeyItems.keyItem.ScrewDriver:
                case KeyItems.keyItem.Hammer:
                    canInteract = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            //Ensenyar el canvas necesario en una posicion fija
            canInteract = false;
        }


    }
    
    private void ExitTheWall()
    {
        canInteract = false;
        necesaryObjectCanvas.SetActive(false);
    }
    public void SetLadder()
    {
        Debug.Log("False");
        isLadderPlaced = false;      
    }

    private void Moving()
    {
        if(!isUpLadder)
        {
            Player.transform.SetParent(walls.transform);
            if(takefirstRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination.position, speed * Time.deltaTime);
                if(Player.transform.position == PlayerDownDestination.position)
                {
                    takeSecondRoute = true;
                    takefirstRoute = false;
                }      
            }
            else if(takeSecondRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerUpDestination.position, speed * Time.deltaTime);
                if (Player.transform.position == PlayerUpDestination.position)
                {
                    takeSecondRoute = false;
                    takeThirdRoute = true;
                }                   
            }
            else if(takeThirdRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination2.position, speed * Time.deltaTime);
                if (Player.transform.position == PlayerDownDestination2.position)
                {
                   // takingLadder = false;
                    takeThirdRoute = false;
                }
            }                  
        }
        else
        {
            if (takefirstRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination2.position, speed * Time.deltaTime);
                takeSecondRoute = true;
                takefirstRoute = false;

            }
            else if (takeSecondRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination.position, speed * Time.deltaTime);
                takeSecondRoute = false;
                takeThirdRoute = true;
            }
            else if (takeThirdRoute)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination.position, speed * Time.deltaTime);
                //takingLadder = false;
                takeThirdRoute = false;
            }
        }
    }
}
