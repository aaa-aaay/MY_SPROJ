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
            if(player is IDeath dying)
            {

                if (player.playerNo == 1)
                {

                    dying.RespawnPosition = p1Respawn;

                }
                else
                {
                    dying.RespawnPosition = p2Respawn;
                }
            }

            return;

        }




        if (other.gameObject.GetComponent<IDeath>() != null)
        {
            IDeath death = other.gameObject.GetComponent<IDeath>();

            death.RespawnPosition = p1Respawn;
        }

    }
}
