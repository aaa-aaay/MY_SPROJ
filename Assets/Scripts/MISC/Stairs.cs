using UnityEngine;

public class Stairs : MonoBehaviour
{
    private float originalSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            originalSpeed = player.moveSpeed;
            player.moveSpeed = 5;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.moveSpeed = originalSpeed;
        }
    }
}
