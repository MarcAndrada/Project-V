using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeObjects : MonoBehaviour
{
    [SerializeField]
    private Transform handPoint;

    [SerializeField]
    private GameObject pickedObject;

    [SerializeField]
    private float areaToSearch;

    private Transform playerPosition;

    private void Awake()
    {
        playerPosition = GetComponent<Transform>(); 
    }

    private void Update()
    {
        if(Input.GetKey("f") && pickedObject == null)
        {
           CreateCollider();
        }
    }

    private void CreateCollider()
    {
        GameObject nearestObject;
        Vector2 itemPos;
        Vector2 playerPos = new Vector2(playerPosition.position.x, playerPosition.position.z);
        Collider[] items = Physics.OverlapSphere(transform.position, areaToSearch); 
        foreach(Collider item in items) 
        {
            itemPos = new Vector2(item.transform.position.x, item.transform.position.z);
            Vector2 oreWaSex = itemPos - playerPos;
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaToSearch);
    }

}
