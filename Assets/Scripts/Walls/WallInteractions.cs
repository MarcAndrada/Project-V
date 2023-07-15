using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WallInteractions : MonoBehaviour
{
    [SerializeField]
    Keyitem keyItems;

    [SerializeField]
    Keyitem stairs;

    [SerializeField]
    private GameObject allow;

    [SerializeField]
    private GameObject walls;

    [SerializeField]
    private GameObject prohibited;

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
    private bool canTakeLadder;

    [SerializeField]
    private bool canInteract;

    [SerializeField]
    private TakeObjects objectTaken;

    private Keyitem item;

    private bool takingLadder = false;
    private bool takefirstRoute = false;
    private bool takeSecondRoute = false;
    private bool takeThirdRoute = false;

    private void Awake()
    {
        objectTaken = GameObject.FindObjectOfType<TakeObjects>();
    }

    private void OnTriggerEnter(Collider other)
    {
        item = objectTaken.GetItems();
        Debug.Log(item);
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
        if (canInteract && Input.GetKeyDown("q") && !canTakeLadder)
        {
            if(keyItems == stairs)
            {
                MoveStairs();
                item.SetRb();
            }
            else
            {
                DestroyWall();
            }
        }
        else if(Input.GetKeyDown("e") && canTakeLadder && canInteract && !takingLadder)
        {
            TakeLadder();
        } 
        if(takingLadder)
        {
            Moving();
        }
    } 

    private void MoveStairs()
    {
        allow.SetActive(false);
        prohibited.SetActive(false);
        item.transform.position = LadderDestination.position;
        canTakeLadder = true;
        objectTaken.ReleaseObject();
        allow.SetActive(true);
    }

    private void DestroyWall()
    {
        allow.SetActive(false);
        prohibited.SetActive(false);
        Destroy(gameObject);
    }

    private void TakeLadder()   
    {
        takingLadder = true;
        takefirstRoute = true;
    }

    private void EnterTheWall()
    {
        canInteract = true;
        if (keyItems == item && !canTakeLadder && objectTaken.GetPickedObject())
        {
            allow.SetActive(true);
            prohibited.SetActive(false); //Por si acaso
        }
        else if (canTakeLadder)
        {  
            allow.SetActive(true);
        }
        else if (keyItems != item)
        {
            canInteract = false;
            prohibited.SetActive(true);
        }
        else
        {
            canInteract = false;
            allow.SetActive(false);
            prohibited.SetActive(false);
        }
    }
    
    private void ExitTheWall()
    {
        canInteract = false;
        allow.SetActive(false);
        prohibited.SetActive(false);
    }
    public void SetLadder()
    {
        Debug.Log("False");
        canTakeLadder = false;      
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
