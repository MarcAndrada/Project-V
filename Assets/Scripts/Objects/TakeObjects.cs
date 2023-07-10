using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using static UnityEditor.Progress;

public class TakeObjects : MonoBehaviour
{
    [SerializeField]
    private Transform handPoint;

    [SerializeField]
    private bool pickedObject = false;

    [SerializeField]
    private float areaToSearch;

    [SerializeField]
    private LayerMask objectLayer; 
    private Rigidbody pickedObjectRB;
    private Collider pickedObjectCollider; 

    private void Update()
    {
        if(Input.GetKeyDown("f") && !pickedObject)
        {
            GameObject nearestObject = CreateCollider();
            if (nearestObject != null)
            {
                TakeNearestObject(nearestObject);
            }
        }
        //else if (Input.GetKeyUp("f") && pickedObject)
        //{
        //    ReleaseObject();
        //}
    }

    private void TakeNearestObject(GameObject _nearestObject)
    {
        Debug.Log("entra"); 
        pickedObject = true;
        _nearestObject.transform.position = handPoint.position;

        // Desactivar collision y fisicas
        pickedObjectRB = _nearestObject.GetComponent<Rigidbody>();
        pickedObjectCollider = _nearestObject.GetComponent<Collider>();
        pickedObjectRB.isKinematic = true;
        pickedObjectCollider.enabled = false;

        // Hacerlo hijo
        _nearestObject.transform.SetParent(handPoint.transform);
      
    }
    private GameObject CreateCollider()
    {
        GameObject _nearestObject = null; 
        Vector2 playerPos = new Vector2(this.transform.position.x, this.transform.position.z);

        Collider[] items = Physics.OverlapSphere(transform.position, areaToSearch, objectLayer);
        
        float smallestDistance = areaToSearch; 
        foreach (Collider item in items)
        {
            Vector2 itemPos = new Vector2(item.transform.position.x, item.transform.position.z);
            float currentDistance = Vector2.Distance(itemPos, playerPos); 
            if (smallestDistance > currentDistance)
            {
                smallestDistance = currentDistance;
                _nearestObject = item.gameObject;
            }
        }
        return _nearestObject; 
        Debug.Log(_nearestObject); 
    }

    private void ReleaseObject()
    {
        Debug.Log("Salgo");
        pickedObject = false;

        // Activar collision y fisicas y dejar de ser hijo 
        pickedObjectRB.isKinematic = false;
        pickedObjectCollider.enabled = true;
        pickedObjectRB.transform.SetParent(null);
        pickedObjectRB = null; 
        pickedObjectCollider = null;
        
    }

    public bool GetPickedObject()
    {
        return pickedObject; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaToSearch);
    }

}
