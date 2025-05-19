using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] Transform p1Respawn;
    [SerializeField] Transform p2Respawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.PlayerDeath();

            //make screen grey
            //sent player to check point
            //make screen normal

        }
    }
}
