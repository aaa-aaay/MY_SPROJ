using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private float shootDelay = 1f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float fireRate = 0.5f;

    private float fireCooldown;
    private bool playerInSight;

    private void Update()
    {
        playerInSight = false; // Reset before loop

        Vector2 direction = -transform.right;
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.transform.position, direction, detectionRange);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                break;
            }
        }

        if (!playerInSight)
        {
            fireCooldown = shootDelay;
        }

        if (playerInSight)
        {
            fireCooldown -= Time.deltaTime;

            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = fireRate;
            }
        }
    }


    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Bullet shoot = bullet.GetComponent<Bullet>();
        shoot.Shoot(-transform.right, shootSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize raycast range in editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.right * detectionRange);
    }
}
