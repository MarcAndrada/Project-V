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
    private GameObject prohibited;

    [SerializeField]
    private Transform LadderDestination;

    [SerializeField]
    private Transform PlayerUpDestination;

    [SerializeField]
    private Transform PlayerDownDestination;

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

    private void Awake()
    {
        objectTaken = GameObject.FindObjectOfType<TakeObjects>();
        //keyItems = GameObject.FindObjectOfType<KeyItems>();
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
        else if(Input.GetKeyDown("e") && canTakeLadder && canInteract)
        {
            TakeLadder();
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
        if(!isUpLadder)
        {
            Player.transform.position = PlayerUpDestination.position;
            isUpLadder = true;
        }
        else if (isUpLadder)
        {
            Player.transform.position = PlayerDownDestination.position;
            isUpLadder = false;
        }
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
}
