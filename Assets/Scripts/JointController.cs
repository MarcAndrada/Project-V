using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    private float km;
    private Vector2 ln;

    [SerializeField]
    private GameObject player2;
 
    private Rigidbody rb1;

    public bool existsJoint;

    private void Awake()
    {
        km = 0.3f;
        player2 =  FindObjectOfType<GameObject>();
        ln.x = 50f;
        ln.y = 100f;
        existsJoint = false;
        rb1 = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(existsJoint)
        {
            Vector3 vectorJoint =  Joint(this.gameObject, player2);
            
        }
    }

    Vector3 Joint(GameObject player1, GameObject player2)
    {
        Vector3 force = Vector3.zero;

        force.x = -km * (ln.x - (player1.transform.position.x - player2.transform.position.x));
        force.y = -km * (ln.y - (player1.transform.position.y - player2.transform.position.y));
        return force;
    }
}
