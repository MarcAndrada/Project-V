using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyitem : MonoBehaviour
{
    [SerializeField]
    private KeyItems item;

    [SerializeField]
    private Rigidbody rb;

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    public KeyItems GetItem()
    {
        return item;
    }

    public void SetRb()
    {
        rb.isKinematic = true;
    }
}
