using UnityEngine;

public class ColorShootAbility : PlayerAbilities
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject GranadePrefab;

    private float shootCD;


    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private float shootPower = 10.0f;
    [SerializeField] private Transform firePoint;
    private float fireCooldown = 0f;


    private void Update()
    {
        if (abilityAction1.IsInProgress())
        {
            Debug.Log("Holding Shoot");
            fireCooldown -= Time.deltaTime;


            if (fireCooldown <= 0f)
            {
                Debug.Log("Bullet Shot");
                ShootBullet();
                fireCooldown = fireRate;
            }
        }
        else
        {
            fireCooldown = 0f;
        }

        if (abilityAction2.WasPressedThisFrame()) {

            //throw genreade


        }
    }


    private void ShootBullet()
    {
        Vector2 aimInput = player.aimAction.ReadValue<Vector2>().normalized;

        if (aimInput == Vector2.zero)
        {
            aimInput = Vector2.left;
        }

        GameObject bulletOBJ = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletOBJ.GetComponent<Bullet>();
        bullet.Shoot(aimInput, shootPower);
    }
}
