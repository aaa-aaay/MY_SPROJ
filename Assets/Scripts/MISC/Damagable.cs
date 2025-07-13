using UnityEngine;

public class Damagable : MonoBehaviour
{
    private SpriteRenderer sRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private Color originalColor;
    private Explosion explosion;
    [SerializeField]private int health;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        explosion = GetComponentInChildren<Explosion>();
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
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        sRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sRenderer.color = originalColor;
    }
}
