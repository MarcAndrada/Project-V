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
    private GameObject LadderDestination;

    [SerializeField]
    private GameObject PlayerUpDestination;

    [SerializeField]
    private GameObject PlayerDownDestination;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private bool isUpLadder = false; //Esto estara en el player controller

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool canTakeLadder = false;

    [SerializeField]
    private bool canInteract = false;
    private void OnTriggerEnter(Collider other)
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem") && !canInteract) //Faltaría pasarle que el player esta sujetando un objeto, porque si no simplemene podrá interecturar aun estando el objeto en el suelo
        {
            canInteract = true;
            allow.SetActive(true);
            prohibited.SetActive(false);
        }
        else if (canTakeLadder && other.CompareTag("Player"))
        {
            allow.SetActive(true);
        }
        else
        {
            prohibited.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem") && canInteract)
        {
            canInteract = false;
            allow.SetActive(false);
            prohibited.SetActive(false);
        }
        else if (canTakeLadder && other.CompareTag("Player")) 
        {
            allow.SetActive(false);
        }
    }
    private void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.Q) && !canTakeLadder)
        {
            if(keyItems == stairs && !canTakeLadder)
            {
                MoveStairs();
               
            }
            else
            {
                DestroyWall();
            }
        }
        else if(Input.GetKeyDown(KeyCode.E) && canTakeLadder)
        {
            TakeLadder();
        }
    }

    private void MoveStairs()
    {
        allow.SetActive(false);
        prohibited.SetActive(false);
        item.transform.position = Vector3.MoveTowards(item.transform.position, LadderDestination.transform.position, speed);
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
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerUpDestination.transform.position, speed);
            isUpLadder = true;
        }
        else if (isUpLadder)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerDownDestination.transform.position, speed);
            isUpLadder = false;
        }
       
    }


}
