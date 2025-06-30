using Unity.VisualScripting;
using UnityEngine;

public class GolemBullet : Bullet
{
    private Boid boid;
    ShipControls shipControls;
    [SerializeField] private float health = 2;

    public override void Shoot(Vector2 direction = default, float power = 0)
    {
        boid = GetComponent<Boid>();
        boid.target = bulletTarget;
        shipControls = bulletTarget.GetComponent<ShipControls>();
    }
    private void Update()
    {
        if(shipControls != null && shipControls.IsDead)
        {
            DestroyBullet();
        }
    }


    public override void DestroyBullet()
    {
        health--;
        if(health <= 0)
        {
            base.DestroyBullet();
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayers.value) != 0)
        {
            if (collision.gameObject == shipControls.gameObject)
            {

                shipControls.shipHealth--;
                DestroyBullet();



            }

            DestroyBullet();
        }


    }



}
