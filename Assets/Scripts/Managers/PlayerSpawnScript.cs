using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private int playerCount = 0;
    public GameObject secondPlayer;


    private void Start()
    {
    }

    //public void OnPlayerJoined(PlayerInput playerInput)
    //{
    //    if (playerCount >= 1) {
    //        PlayerInputManager manager = GetComponent<PlayerInputManager>();
    //        manager.DisableJoining();
    //        Debug.Log("disabled joinging");


    //    }

    //    playerInput.transform.position = SpawnPoints[playerCount].transform.position;
    //    playerCount++;
    //    Debug.Log(playerCount);

    //    PlayerInputManager.instance.playerPrefab = secondPlayer;

    //    // Get the Camera component from the same GameObject
    //    Camera playerCamera = playerInput.transform.parent.GetComponentInChildren<Camera>();
    //    int index = playerInput.playerIndex;

    //    CameraManager.Instance.SetPlayerCameras(playerCamera, index + 1);
    //    PlayerManager.Instance.SetPlayers(playerInput.gameObject.GetComponent<PlayerController>(), index + 1);
    //    if (index == 1)
    //    {
    //        CameraManager.Instance.InitCameras();
    //    }
    //}


    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // Prevent more than 2 players from joining
        if (playerCount >= 2)
        {
            Destroy(playerInput.gameObject); // Remove extra player
            Debug.Log("Player limit reached. Destroying extra player.");
            return;
        }

        // Set spawn position
        playerInput.transform.position = SpawnPoints[playerCount].transform.position;

        // Optional: Change player prefab after 1st joins (if needed)
        if (playerCount == 0)
        {
            PlayerInputManager.instance.playerPrefab = secondPlayer;
        }

        playerCount++;
        Debug.Log($"Player {playerCount} joined");

        // Camera and player manager logic
        Camera playerCamera = playerInput.transform.parent.GetComponentInChildren<Camera>();
        int index = playerInput.playerIndex;

        CameraManager.Instance.SetPlayerCameras(playerCamera, index + 1);
        PlayerManager.Instance.SetPlayers(playerInput.gameObject.GetComponent<PlayerController>(), index + 1);

        // Initialize cameras when both players are ready
        if (playerCount == 2)
        {
            CameraManager.Instance.InitCameras();

            // Disable further joining after both players are in
            PlayerInputManager.instance.DisableJoining();
            gameObject.SetActive(false);
            Debug.Log("Disabled further joining after 2 players.");
        }
    }

}
