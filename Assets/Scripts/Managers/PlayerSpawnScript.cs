using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private int playerCount;
    public GameObject secondPlayer;


    private void Start()
    {
        PlayerInputManager.instance.JoinPlayer();
        PlayerInputManager.instance.playerPrefab = secondPlayer;
        PlayerInputManager.instance.JoinPlayer();

    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerCount == 2) return;
        playerInput.transform.position = SpawnPoints[playerCount].transform.position;
        playerCount++;
    }
}
    