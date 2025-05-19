using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform p1Respawn;
    [SerializeField] private Transform p2Respawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.playerNo == 1)
            {
                player.repsawnPosition = p1Respawn;
            }
            else
            {
                player.repsawnPosition = p2Respawn;
            }
        }
    }
}
