using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private int playerCount;
    public GameObject secondPlayer;


    private void Start()
    {

    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerCount == 2) return;
        playerInput.transform.position = SpawnPoints[playerCount].transform.position;
        playerCount++;

        PlayerInputManager.instance.playerPrefab = secondPlayer;

        // Get the Camera component from the same GameObject
        Camera playerCamera = playerInput.transform.parent.GetComponentInChildren<Camera>();
        int index = playerInput.playerIndex;

        CameraManager.Instance.SetPlayerCameras(playerCamera, index + 1);
        PlayerManager.Instance.SetPlayers(playerInput.gameObject.GetComponent<PlayerController>(), index + 1);
        if (index == 1)
        {
            CameraManager.Instance.InitCameras();
        }
    }
}
    