using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour
{
    [SerializeField] 
    MovingItems movingItem;

    [SerializeField]
    Score score;

    protected Rigidbody rb;

    private WallInteractions wall;

    [HideInInspector]
    public bool picked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public MovingItems GetItem()
    {
        return movingItem;
    }

    public void StopPhysics()
    {
        rb.isKinematic = true;
    }

    private void Score()
    {
        score.SetScore(movingItem.GetScore());
    }

    public WallInteractions AtachedWall()
    {
        return wall;
    }
    public void SetWall(WallInteractions _wall)
    {
        wall= _wall;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (movingItem.GetItemType() != MovingItems.ItemType.key && collision.collider.CompareTag("Truck"))
        {
            Score();
            Destroy(gameObject);
        }
    }


}
