using NUnit.Framework;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private int playerCount = 0;
    public GameObject secondPlayer;
    private int savedCheckpoints;

    [SerializeField] private CheckPoint[] checkPoints;


    private void Start()
    {
        if (!PlayerSelectionManager.Instance.loadedData)
        {
            savedCheckpoints = PlayerSelectionManager.Instance.loadedCheckPointNo;
            PlayerSelectionManager.Instance.loadedData = true;
        }
        else
        {
            savedCheckpoints = 0;
        }



       

        for (int i = 0; i < 2; i++)
        {
            GameObject prefab = PlayerSelectionManager.Instance.GetCharacterPrefabForPlayer(i);
            InputDevice device = PlayerSelectionManager.Instance.GetDeviceForPlayer(i);

            if (prefab != null && device != null)
            {
                string scheme = null;
                InputDevice[] devicesToPair = null;

                if (device is Gamepad)
                {
                    scheme = "Gamepad";
                    devicesToPair = new InputDevice[] { device };
                    Debug.Log("Found Gamepad");
                }
                else if (device is Keyboard || device is Mouse)
                {
                    scheme = "Keyboard&Mouse";
                    // Pair both keyboard + mouse together
                    devicesToPair = new InputDevice[] { Keyboard.current, Mouse.current };
                    Debug.Log("Found Keyboard & Mouse");
                }

                if (scheme != null && devicesToPair != null)
                {
                    PlayerInput input = PlayerInput.Instantiate(prefab, controlScheme: scheme, pairWithDevices: devicesToPair);
                    input.transform.position = SpawnPoints[i].position;

                    PlayerController player = input.GetComponent<PlayerController>();
                    if (i == 0)
                    {
                        input.transform.position = checkPoints[savedCheckpoints].p1Respawn.transform.position;
                        player.RespawnPosition = checkPoints[savedCheckpoints].p1Respawn.transform;
                    }

                    else
                    {
                        input.transform.position = checkPoints[savedCheckpoints].p2Respawn.transform.position;
                        player.RespawnPosition = checkPoints[savedCheckpoints].p2Respawn.transform;
                    }



                    Camera playerCamera = input.transform.parent.GetComponentInChildren<Camera>();
                    CameraManager.Instance.SetPlayerCameras(playerCamera, i + 1);
                    PlayerManager.Instance.SetPlayers(input.gameObject.GetComponent<PlayerController>(), i + 1);
                }
            }
        }

        CameraManager.Instance.InitCameras();


    }


    private void SetSpawnPoint()
    {



        //load in the game object
        ProgressSaveData data = new ProgressSaveData();


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


    //public void OnPlayerJoined(PlayerInput playerInput)
    //{
    //    // Prevent more than 2 players from joining
    //    if (playerCount >= 2)
    //    {
    //        Destroy(playerInput.gameObject); // Remove extra player
    //        Debug.Log("Player limit reached. Destroying extra player.");
    //        return;
    //    }

    //    // Set spawn position
    //    playerInput.transform.position = SpawnPoints[playerCount].transform.position;

    //    // Optional: Change player prefab after 1st joins (if needed)
    //    if (playerCount == 0)
    //    {
    //        PlayerInputManager.instance.playerPrefab = secondPlayer;
    //    }

    //    playerCount++;
    //    Debug.Log($"Player {playerCount} joined");

    //    // Camera and player manager logic
    //    Camera playerCamera = playerInput.transform.parent.GetComponentInChildren<Camera>();
    //    int index = playerInput.playerIndex;

    //    CameraManager.Instance.SetPlayerCameras(playerCamera, index + 1);
    //    PlayerManager.Instance.SetPlayers(playerInput.gameObject.GetComponent<PlayerController>(), index + 1);

    //    // Initialize cameras when both players are ready
    //    if (playerCount == 2)
    //    {
    //        CameraManager.Instance.InitCameras();

    //        // Disable further joining after both players are in
    //        PlayerInputManager.instance.DisableJoining();
    //        gameObject.SetActive(false);
    //        Debug.Log("Disabled further joining after 2 players.");
    //    }
    //}

    //public void OnPlayerJoined(PlayerInput playerInput)
    //{

    //    if (playerCount >= 2)
    //    {
    //        Destroy(playerInput.gameObject);
    //        return;
    //    }

    //    // Get the correct prefab for this player
    //    GameObject prefab = PlayerSelectionManager.Instance.GetCharacterPrefabForPlayer(playerCount);
    //    if (prefab != null)
    //    {
    //        PlayerInputManager.instance.playerPrefab = prefab;
    //    }

    //    playerInput.transform.position = SpawnPoints[playerCount].position;

    //    playerCount++;
    //    Debug.Log($"Player {playerCount} joined");

    //    Camera playerCamera = playerInput.transform.parent.GetComponentInChildren<Camera>();
    //    int index = playerInput.playerIndex;

    //    CameraManager.Instance.SetPlayerCameras(playerCamera, index + 1);
    //    PlayerManager.Instance.SetPlayers(playerInput.GetComponent<PlayerController>(), index + 1);

    //    if (playerCount == 2)
    //    {
    //        CameraManager.Instance.InitCameras();
    //        PlayerInputManager.instance.DisableJoining();
    //        gameObject.SetActive(false);
    //    }
    //}

}
