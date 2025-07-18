using UnityEngine;

public class Damagable : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private Color originalColor;
    private Explosion explosion;
    [SerializeField]private int health;

    [SerializeField] private Color hitColor = Color.red;

    [SerializeField] healthBar healthBar;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        explosion = GetComponentInChildren<Explosion>();

        if(healthBar  != null)
        {
            healthBar.SetMaxHealth(health);
            healthBar.SetHealth(health);
        }

    }

    public void Hit(int dmage)
    {
        health--;
        if (health <= 0)
        {
            if (explosion != null) { explosion.TriggerExplosoin(gameObject); }
            else 
            Destroy(gameObject);
        }
        StartCoroutine(FlashWhite());


        if(healthBar != null)
        healthBar.SetHealth(health);
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        sRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        sRenderer.color = originalColor;
    }
}
