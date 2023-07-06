using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WallInteractions : MonoBehaviour
{
    [SerializeField]
    KeyItems keyItems;

    [SerializeField]
    Keyitem item;

    [SerializeField]
    KeyItems stairs;

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

    private void OnTriggerEnter(Collider other)
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem") && !canInteract && !canTakeLadder) //Faltar�a pasarle que el player esta sujetando un objeto, porque si no simplemene podr� interecturar aun estando el objeto en el suelo
        {
            canInteract = true;
            allow.SetActive(true);
            prohibited.SetActive(false); //Por si acaso
        }
        else if (canTakeLadder && other.CompareTag("Player"))
        {
            canInteract = true;
            allow.SetActive(true);
        }
        else if(keyItems != item && other.CompareTag("KeyItem") && !canInteract)
        {
            prohibited.SetActive(true);
        }
        else
        {
            allow.SetActive(false);
            prohibited.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem") && canInteract && !canTakeLadder)
        {
            canInteract = false;
            allow.SetActive(false);
            prohibited.SetActive(false);
        }
        else if (canTakeLadder && other.CompareTag("Player")) 
        {
            canInteract= false;
            allow.SetActive(false);
        }
    }
    private void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.Q) && !canTakeLadder)
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
        else if(Input.GetKeyDown(KeyCode.E) && canTakeLadder && canInteract)
        {
            TakeLadder();
        }
    }

    private void MoveStairs()
    {
        allow.SetActive(false);
        prohibited.SetActive(false);
        item.transform.position = Vector3.MoveTowards(item.transform.position, LadderDestination.position, speed);
        canTakeLadder = true;
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
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerUpDestination.position, speed);
            isUpLadder = true;
        }
        else if (isUpLadder)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination.position, speed);
            isUpLadder = false;
        }
    }
}
