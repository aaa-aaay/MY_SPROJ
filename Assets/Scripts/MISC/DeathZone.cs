using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player.isDead) return;
            player.PlayerDeath();

            //make screen grey
            //sent player to check point
            //make screen normal

        }
    }
}
