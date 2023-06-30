using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{
    private float km, kl;

    [SerializeField]
    private PlayerController player1;

    [SerializeField]
    private PlayerController player2;

    private void Awake()
    {
        km = 0.3f;
        kl = 10.0f;
        player1 = GetComponent<PlayerController>();
        player2 = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
