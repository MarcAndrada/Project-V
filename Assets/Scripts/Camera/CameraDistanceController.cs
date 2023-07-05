using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceController : MonoBehaviour
{
    private enum CameraMovement { NONE, ZOOM_IN, ZOOM_OUT };

    [SerializeField]
    private CameraMovement camState = CameraMovement.NONE;


    [Header("Players"), SerializeField]
    private Collider[] players;
    [Header("Players Variables"), SerializeField]
    private float minYDistance;
    private float zOffset;
    private float playersY;

    [Space, Header("Cameras"), SerializeField]
    private Camera insideCamera;
    [SerializeField]
    private Camera externalCamera;

    [Header("Cameras Variables"), SerializeField, Range(0, 1)]
    private float movementSpeed;
    [SerializeField]
    private float zoomOutSpeed;
    [SerializeField]
    private float zoomInSpeed;
    [SerializeField]
    private float XZSpeed;
    
    



    private void Start()
    {
        //Guardamos la Y del primer Player
        playersY = players[0].transform.position.y;
        //Seteamos todos los players con la misma posicion en Y
        foreach (Collider item in players)
        {
            item.transform.position = new Vector3(item.transform.position.x, playersY, item.transform.position.z);
        }
        //colocar la camara a la distancia minima
        transform.position = GetMiddlePointBetweenPlayers() - transform.forward * minYDistance;
        zOffset = transform.position.z - GetMiddlePointBetweenPlayers().z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCamDistance();
        MoveCamera();
    }

    private void CheckCamDistance()
    {
        bool zoomIn = true;
        bool zoomOut = false;
        foreach (Collider item in players)
        {
            Plane[] camFrustrum = GeometryUtility.CalculateFrustumPlanes(externalCamera);
            if (!GeometryUtility.TestPlanesAABB(camFrustrum, item.bounds))
            {
                //Si esta fuera de la camara exterior alejamos la cam
                zoomOut = true;
            }
            else
            {
                //Si esta dentro de la camara
                camFrustrum = GeometryUtility.CalculateFrustumPlanes(insideCamera);

                //Comprueba que no haya ningun player en algun borde
                //(lo miramos comprobando que esten dentro del frustrum de la camara interior)
                if (!GeometryUtility.TestPlanesAABB(camFrustrum, item.bounds))
                {
                    //Si no hay ningun player fuera de la camara interna hacer ZOOM_IN 
                    zoomIn = false;
                }



            }
        }


        if (zoomOut)
        {
            camState = CameraMovement.ZOOM_OUT;
        }
        else if(zoomIn)
        {
            camState = CameraMovement.ZOOM_IN;
        }
        else
        {
            camState = CameraMovement.NONE;
        }




    }

    private void MoveCamera() 
    {
        Vector3 destinyPos = transform.position;

        if (camState != CameraMovement.NONE)
        {
            float zoomSpeed = 1;
            switch (camState)
            {
                case CameraMovement.ZOOM_IN:
                    zoomSpeed = -zoomInSpeed;
                    break;
                case CameraMovement.ZOOM_OUT:
                    zoomSpeed = zoomOutSpeed;
                    break;
                default:
                    break;
            }
            destinyPos -= transform.forward * zoomSpeed;
        }

        Vector3 middlePos = GetMiddlePointBetweenPlayers();

        Vector3 XZDir = new Vector3
            (
            middlePos.x - transform.position.x,
            0,
            (middlePos.z + zOffset) - transform.position.z
            );
        destinyPos += XZDir * XZSpeed;

        transform.position = Vector3.Lerp
            (
            transform.position,
            destinyPos,
            movementSpeed * Time.fixedDeltaTime
            );
        
    }
    private Vector3 GetMiddlePointBetweenPlayers()
    {
        Vector3 middlePoint = Vector3.zero;

        foreach (Collider item in players)
        {
            middlePoint += item.transform.position;
        }
        middlePoint.y = playersY;
        middlePoint /= players.Length;
        return middlePoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(GetMiddlePointBetweenPlayers(),0.2f);
    }

}
