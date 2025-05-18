using UnityEngine;

public class Stairs : MonoBehaviour
{
    private float originalSpeed;
    private float originalMass;
    private float originalLength;
    [SerializeField] private PhysicsMaterial2D highFrctionMatt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            originalSpeed = player.moveSpeed;

            player.moveSpeed = 5;
            originalLength = player.rayCastGroundLength;
            player.rayCastGroundLength = originalLength * 2;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            player.moveSpeed = originalSpeed;
            rb.sharedMaterial = null;
            player.rayCastGroundLength = originalLength;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if(!(Mathf.Abs(player.dirH) > 0.1f))
            {
                rb.sharedMaterial = highFrctionMatt;
            }
            else
            {
                rb.sharedMaterial = null;
            }
        }
    }
}
