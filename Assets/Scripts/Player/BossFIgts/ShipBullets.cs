using UnityEngine;

public class ShipBullets : Bullet
{
    public override void Shoot(Vector2 direction = default, float power = 0)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0;
        rb.AddForce(direction * power, ForceMode2D.Impulse);

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(((1 << collision.gameObject.layer) & interactableLayers.value) != 0)
        {

            //if collider with boss, do smomthing
            Bullet otherBullet = collision.gameObject.GetComponent<Bullet>();
            if(otherBullet != null)
            {
                otherBullet.DestroyBullet();
            }


            Destroy(gameObject);
        }
    }
}
