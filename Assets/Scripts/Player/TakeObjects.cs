using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

    public Keyitem item { get; private set; }

    private void Awake()
    {
        //interactions = GameObject.FindObjectOfType<WallInteractions>();
    }

    public void CheckCanTakeObject() 
    {
        GameObject nearestObject = CheckNearestObject();
        if (nearestObject != null)
        {
            TakeNearestObject(nearestObject);
        }
    }
    private GameObject CheckNearestObject()
    {
        GameObject _nearestObject = null;
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
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
    }

    private void TakeNearestObject(GameObject _nearestObject)
    {
        pickedObject = true;
        _nearestObject.transform.position = handPoint.position;

        // Desactivar collision y fisicas
        pickedObjectRB = _nearestObject.GetComponent<Rigidbody>();
        pickedObjectCollider = _nearestObject.GetComponent<Collider>();
        if(_nearestObject.tag == "KeyItem")
        {
            item = _nearestObject.GetComponent<Keyitem>();
            item.GetItem();
        }

        pickedObjectRB.isKinematic = true;
        pickedObjectCollider.enabled = false;
        // Hacerlo hijo
        _nearestObject.transform.SetParent(handPoint.transform);
    }

    public void ReleaseObject()
    {
        Debug.Log("Salgo");
        if (pickedObject)
        {
            pickedObject = false;

            // Activar collision y fisicas y dejar de ser hijo 
            pickedObjectRB.isKinematic = false;
            pickedObjectCollider.enabled = true;
            pickedObjectRB.transform.SetParent(null);
            pickedObjectRB = null;
            pickedObjectCollider = null;
            item = null;
        }

    }

    public bool GetPickedObject()
    {
        return pickedObject; 
    }

    public Keyitem GetItems()
    {
        return item;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, areaToSearch);
    }

}