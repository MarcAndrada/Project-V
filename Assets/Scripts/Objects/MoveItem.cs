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
   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Truck"))
        {
            Score();
            Destroy(gameObject);
        }
    }

    private void Score()
    {
        score.SetScore(movingItem.GetScore());
    }

}
