using UnityEngine;
using UnityEngine.UIElements;

public class ColorShootAbility : PlayerAbilities
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject granadePrefab;

    private float shootCD;


    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private float shootPower = 10.0f;
    [SerializeField] private Transform firePoint;
    private float fireCooldown = 0f;

    private GameObject nade;


    private void Update()
    {

        if(player.IsDead) return;


        if (abilityAction1.IsInProgress())
        {
            Debug.Log("Holding Shoot");
            fireCooldown -= Time.deltaTime;


            if (fireCooldown <= 0f)
            {
                Debug.Log("Bullet Shot");
                AudioManager.instance.PlaySFX("LaserBullet");
                ShootorThrowGrenade(bulletPrefab);
                fireCooldown = fireRate;
            }
        }
        else
        {
            fireCooldown = 0f;
        }

        if (nade == null && abilityAction2.WasPressedThisFrame()) {
            ShootorThrowGrenade(granadePrefab);
            //throw genreade


        }


    }

    private void ThrowGrenade()
    {
        Vector2 aimInput = player.aimAction.ReadValue<Vector2>().normalized;
        if (aimInput == Vector2.zero)
        {
            aimInput = new Vector2(transform.parent.transform.localScale.x, 0);
        }
    }

    private void ShootorThrowGrenade(GameObject objectPrefab)
    {
        Vector2 aimInput = player.aimAction.ReadValue<Vector2>().normalized;
        if (aimInput == Vector2.zero)
        {

            aimInput = new Vector2(transform.parent.transform.localScale.x, 0);
        }

        GameObject bulletOBJ = Instantiate(objectPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletOBJ.GetComponent<Bullet>();

        if (objectPrefab == granadePrefab)
        {
            bullet.SetInputAction(abilityAction2);
            nade = bulletOBJ;

        }

        bullet.Shoot(aimInput, shootPower);



    }
}
