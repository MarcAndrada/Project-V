using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoveItem;
using static UnityEditor.Progress;

public class MoveItem : MonoBehaviour
{
    [SerializeField] 
    MovingItems movingItem;

    [SerializeField]
    Score score;

    private Rigidbody rb;

    private WallInteractions wall;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(movingItem.GetType() != MovingItems.ItemType.key && collision.collider.CompareTag("Truck"))
        {
            Score();
            Destroy(gameObject);
        }
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

}
