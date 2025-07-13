using System.Runtime.CompilerServices;
using UnityEngine;


public class LaserBullet : Bullet, ColoredAbilities
{

    public bool redBullet;

    public bool IsRedColor { get; set; }

    private void Start()
    {
        IsRedColor = redBullet;
    }

    public override void Shoot(Vector2 direction = default, float power = 0)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayers.value) != 0)
        {
            Damagable damagable = collision.GetComponent<Damagable>();
            if(damagable != null) damagable.Hit(damage);
            Destroy(gameObject);
        }
    }



}
