using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WallInteractions : MonoBehaviour
{
    public enum WallType { DESTRUCTIBLE, STAIRS };

    public WallType wallType;

    [Space, SerializeField]
    public MovingItems.Item necessaryItem;

    [SerializeField]
    private GameObject necessaryObjectCanvas;

    [SerializeField]
    private Transform LadderDestination;

    [Space, Header("Ladder Climbing"), SerializeField]
    public Transform PlayerUpDestination;
    [SerializeField]
    public Transform[] PlayerDownDestination;
    public bool isLadderPlaced { get; private set; }

    private PlayerController player;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
       
    }
 
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && isLadderPlaced)
        {
            //StartClimbLadder();
        } 

    } 

    public void PlaceLadder(MoveItem item)
    {
        if (!isLadderPlaced)
        {
            item.GetComponent<Collider>().enabled = false;
            necessaryObjectCanvas.SetActive(false);
            item.transform.position = LadderDestination.position;
            isLadderPlaced = true;
            player.objectsController.ReleaseObject();
            necessaryObjectCanvas.SetActive(true);
            item.StopPhysics();
        }
        

    }

    private void DestroyWall()
    {
        necessaryObjectCanvas.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLadderPlaced)
        {
            necessaryObjectCanvas.SetActive(true);
        }
        if(other.CompareTag("InteractionObject"))
        {
            if(player.objectsController.item.GetItem().GetItem() == necessaryItem)
            {
                DestroyWall();
            }         
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            necessaryObjectCanvas.SetActive(false);
        }
    }

}
