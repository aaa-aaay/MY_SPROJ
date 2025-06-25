using UnityEngine;
[DefaultExecutionOrder(-9)]
public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance { get; private set; }

    public PlayerController player1;
    public PlayerController player2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayers(PlayerController p, int playerNo)
    {
        if(playerNo == 1)
        {
            player1 = p;
            Debug.Log("p1 inited");

        }
        else if(playerNo == 2)
        {
            player2 = p;
            Debug.Log("p2 inited");
        }


    }
    public PlayerController GetPlayer1()
    {
        return player1;
    }
    public PlayerController GetPlayer2()
    {
        return player2;
    }
}
