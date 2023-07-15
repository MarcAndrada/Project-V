using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayersReadyController : MonoBehaviour
{
    const int MAX_PLAYERS = 2;

    [SerializeField]
    private MultiplePlayerController multiplayerController;
    [SerializeField]
    private CameraController cameraController;

    [Space,SerializeField]
    private GameObject[] joinGameButtonsUI;
    [SerializeField]
    private GameObject startGameButton;

    [Space, SerializeField]
    private GameObject[] playerSpawnPos;

    private List<PlayerInput> players;
    private List<Vector3> playersStartPos;

    private Vector3 camStarterPos;


    // Start is called before the first frame update
    void Start()
    {
        camStarterPos = cameraController.transform.position;
        cameraController.transform.position = new Vector3(0,10000,0);
        players = new List<PlayerInput>();
        playersStartPos = new List<Vector3>();
        CheckIfShowStartGameButton();
    }


    public void AddPlayer(PlayerInput _newPlayer)
    {
        players.Add(_newPlayer);
        playersStartPos.Add(_newPlayer.transform.position);
        PlacePlayerOnMenu(players.IndexOf(_newPlayer));
        SetPlayerInputEvents(_newPlayer);
        CheckIfShowStartGameButton();
    }   

    private void PlacePlayerOnMenu(int _playerIndex)
    {
        //Ocultar los botones de unirse en el lado que se 
        joinGameButtonsUI[_playerIndex].SetActive(false);
        //mover el player al punto exacto del menu
        players[_playerIndex].transform.position = playerSpawnPos[_playerIndex].transform.position;
    }
    private void SetPlayerInputEvents(PlayerInput _playerInput)
    {
        _playerInput.currentActionMap.FindAction("StartGame").performed += StartGameEvent;
        _playerInput.currentActionMap.FindAction("GoBack").performed += RemovePlayerEvent;
    }

    public void RemovePlayerEvent(InputAction.CallbackContext obj)
    {
        //Destruimos el ultimo player
        int playerToDestroyID = players.Count - 1;
        Destroy(players[playerToDestroyID].gameObject);
        //Lo quitamos de las listas
        players.RemoveAt(playerToDestroyID);
        playersStartPos.RemoveAt(playerToDestroyID);
        //Hacemos aparecer de nuevo la UI
        joinGameButtonsUI[playerToDestroyID].SetActive(true);
    }
    public void StartGameEvent(InputAction.CallbackContext obj)
    {
        if (players.Count >= MAX_PLAYERS)
        {
            StartGame();
        }
    }

    private void CheckIfShowStartGameButton()
    {
        if (players.Count >= MAX_PLAYERS)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }

    private void StartGame()
    {

        cameraController.transform.position = camStarterPos;
        cameraController.enabled = true;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = playersStartPos[i];
            players[i].actions.FindActionMap("PlayerSelectMenu").Disable();
            players[i].actions.FindActionMap("Gameplay").Enable();
            
        }


        gameObject.SetActive(false);
    }
}