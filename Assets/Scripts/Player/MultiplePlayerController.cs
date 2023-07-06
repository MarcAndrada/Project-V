using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplePlayerController : MonoBehaviour
{
    private CameraController cameraController;

    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void JoinnedPlayer(PlayerInput obj)
    {
        cameraController.AddPlayer(obj.gameObject);
    }
    public void LeftPlayer(PlayerInput obj)
    {
        cameraController.RemovePlayer(obj.gameObject);
    }
}
