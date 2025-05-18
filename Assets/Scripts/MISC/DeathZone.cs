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
            StartCoroutine(RespawnTime(collision.gameObject));

            //make screen grey
            //sent player to check point
            //make screen normal

        }
    }

    private IEnumerator RespawnTime(GameObject player)
    {
        yield return new WaitForSeconds(1.0f); // delay for death animation

        if (player.name.StartsWith("Player1"))
        {
            player.transform.position = p1Respawn.position;
        }
        else
        {
            player.transform.position = p2Respawn.position;
        }

        // Optional: reactivate input, reset state
       // player.GetComponent<PlayerController>().Revive(); // if you have a method like this
    }
}
