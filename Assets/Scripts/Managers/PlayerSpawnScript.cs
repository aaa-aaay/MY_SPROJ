using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnScript : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private int playerCount;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.transform.position = SpawnPoints[playerCount].transform.position;

        if (playerCount == 0) { 
            //playerInput.GetComponent<PlayerController>()
        
        
        }
        playerCount++;
    }
}
