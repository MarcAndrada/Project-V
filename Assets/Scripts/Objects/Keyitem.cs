using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyitem : MonoBehaviour
{
    [SerializeField]
    private KeyItems item;

    public KeyItems GetItem()
    {
        return item;
    }
}
