using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallInteractions : MonoBehaviour
{
    [SerializeField]
    KeyItems keyItems;

    [SerializeField]
    Keyitem item;

    [SerializeField]
    private GameObject allow;

    [SerializeField]
    private GameObject prohibited;

    [SerializeField]
    private bool canBreak;
    private void OnTriggerEnter(Collider other)
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem")) //Faltaría pasarle que el player esta sujetando un objeto, porque si no simplemene podrá interecturar aun estando el objeto en el suelo
        {
            canBreak = true;
            allow.SetActive(true);
            prohibited.SetActive(false);
        }
        else
        {
            prohibited.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (keyItems == item.GetItem() && other.CompareTag("KeyItem"))
        { 
            canBreak = false;
            allow.SetActive(false);
            prohibited.SetActive(false);
        }
    }
    private void Update()
    {
        if(canBreak && Input.GetKeyDown(KeyCode.Q))
        {
            allow.SetActive(false);
            prohibited.SetActive(false);
            Destroy(gameObject);
        }
    }


}
