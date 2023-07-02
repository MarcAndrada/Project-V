using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractions : MonoBehaviour
{
    [SerializeField]
    KeyItems keyItems;

    [SerializeField]
    Keyitem item;

    private void OnTriggerEnter(Collider other)
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem")) //Faltar�a pasarle que el player esta sujetando un objeto, porque si no simplemene podr� interecturar aun estando el objeto en el suelo
        {
            Debug.Log("SI");
        }
        else
        {
            Debug.Log("NO");
        }
    }
}
