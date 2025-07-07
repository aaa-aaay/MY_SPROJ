using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GolemBullet : Bullet
{
    private Boid boid;
    ShipControls shipControls;
    [SerializeField] private Color damageColor;
    private SpriteRenderer spriteRenderer;
    private Color originalColor = Color.white;

    [SerializeField] private float health = 2;
    [SerializeField] private float fadeDuration = 0.2f;

    public override void Shoot(Vector2 direction = default, float power = 0)
    {
        boid = GetComponent<Boid>();
        boid.target = bulletTarget;
        shipControls = bulletTarget.GetComponent<ShipControls>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            base.DestroyBullet();
            return;
        }


        // Start fade coroutine
        if (spriteRenderer != null)
        {
            StopAllCoroutines();  // Ensure no overlapping fades
            StartCoroutine(FadeDamageColor());
        }


        //fade to damage color and fade back





    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayers.value) != 0)
        {
            if (collision.gameObject == shipControls.gameObject)
            {

                shipControls.ShipHit();
                DestroyBullet();



            }
            health = 0;
            DestroyBullet();
        }


    }



    private IEnumerator FadeDamageColor()
    {
        // Fade to damage color
        float t = 0f;
        while (t < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(originalColor, damageColor, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = damageColor;

        // Fade back to original color
        t = 0f;
        while (t < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(damageColor, originalColor, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = originalColor;
    }



}
