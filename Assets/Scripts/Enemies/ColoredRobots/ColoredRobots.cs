using UnityEngine;

public class ColoredRobots : ColorPuzzle
{

    [SerializeField] private int damage = 1;

    private SpriteRenderer sRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private Color originalColor;
    private Explosion explosion;
    [SerializeField] private int health;
    [HideInInspector] public bool hasDashed;

    public float dashForce = 10.0f;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        explosion = GetComponentInChildren<Explosion>();
    }

    public override void Interact()
    {
        health--;
        if (health <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            if (explosion != null) { explosion.TriggerExplosoin(gameObject); }
            else
                Destroy(gameObject);
        }
        StartCoroutine(FlashWhite());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            controller.TakeDamage(damage);
        }
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        sRenderer.color = Color.yellow;
        yield return new WaitForSeconds(flashDuration);
        sRenderer.color = originalColor;
    }
}
