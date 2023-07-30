using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyItemsController : MoveItem
{
    private int totalPlayersConnected; 

    private List<Rigidbody> players;
    private List<ConfigurableJoint> playerJoints;

    [SerializeField]
    private ConfigurableJoint itemJoint;
    [SerializeField]
    private ConfigurableJoint secondaryItemJoint;

    private List<Collider> sideColliders;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        itemJoint = GetComponent<ConfigurableJoint>();
    }
    private void Start()
    {
        players = new List<Rigidbody>();
        playerJoints = new List<ConfigurableJoint>();
        sideColliders = new List<Collider>();
    }

    public void AddPlayer(Rigidbody _playerToAdd, Collider _side) 
    {
        _playerToAdd.transform.position = new Vector3(_side.transform.position.x, _playerToAdd.transform.position.y, _side.transform.position.z);
        totalPlayersConnected++;
        players.Add(_playerToAdd);
        //Agregar el componente de joint al player
        playerJoints.Add(_playerToAdd.gameObject.GetComponent<ConfigurableJoint>());
        sideColliders.Add(_side);
        _side.enabled = false;
        UpdateItemBehaviour();
    }

    public void RemovePlayer(Rigidbody _playerToRemove)
    {
        totalPlayersConnected--;
        UpdateItemBehaviour();
        //Quitar componente de joint
        int removeIndex = players.IndexOf(_playerToRemove);
        SetFreeJointValues(playerJoints[removeIndex]);
        sideColliders[removeIndex].enabled = true;

        sideColliders.RemoveAt(removeIndex);
        playerJoints.RemoveAt(removeIndex);
        players.Remove(_playerToRemove);

    }

    private void UpdateItemBehaviour() 
    {
        switch (totalPlayersConnected)
        {
            case 0:
                //Hacer que todo este desconectado
                //(Quitar el componente del joint de los colliders)
                SetFreeJointValues(itemJoint);
                SetFreeJointValues(secondaryItemJoint);
                break;
            case 1:
                //Hacer que el player este conectado con el collider contrario
                //(ponerle el componente de joint al otro lado y conectarlo con el player que se queda)
                SetConnectedJointValues(playerJoints[0], rb);
                SetConnectedJointValues(itemJoint, players[0]);
                SetFreeJointValues(secondaryItemJoint);
                players[0].GetComponent<PlayerController>().movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_HEAVY_S);
                break;
            case 2:
                Debug.Log("Lo Tenemos");
                //Hacer que el player este conectado con el otro player
                //(Hacer que cada player tenga el joint configurado para que conecte con el otro player)
                SetConnectedJointValues(playerJoints[0], rb);
                SetConnectedJointValues(playerJoints[1], rb);
                SetConnectedJointValues(itemJoint, players[0]);
                SetConnectedJointValues(secondaryItemJoint, players[1]);
                players[0].GetComponent<PlayerController>().movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_HEAVY_M);
                players[1].GetComponent<PlayerController>().movementController.ChangeState(PlayerMovementController.MovementState.GRABBING_HEAVY_M);

                break;
            default:
                break;
        }
    }

    private void SetConnectedJointValues(ConfigurableJoint _currentJoint, Rigidbody _attachBody) 
    {
        _currentJoint.xMotion = ConfigurableJointMotion.Limited;
        _currentJoint.yMotion = ConfigurableJointMotion.Limited;
        _currentJoint.zMotion = ConfigurableJointMotion.Limited;
        _currentJoint.angularXMotion = ConfigurableJointMotion.Locked;
        _currentJoint.angularYMotion = ConfigurableJointMotion.Locked;
        _currentJoint.angularZMotion = ConfigurableJointMotion.Locked;

        _currentJoint.connectedBody = _attachBody;
    }
    private void SetFreeJointValues(ConfigurableJoint _currentJoint)
    {
        _currentJoint.xMotion = ConfigurableJointMotion.Free;
        _currentJoint.yMotion = ConfigurableJointMotion.Free;
        _currentJoint.zMotion = ConfigurableJointMotion.Free;
        _currentJoint.angularXMotion = ConfigurableJointMotion.Free;
        _currentJoint.angularYMotion = ConfigurableJointMotion.Free;
        _currentJoint.angularZMotion = ConfigurableJointMotion.Free;

        _currentJoint.connectedBody = null;
    }
}
