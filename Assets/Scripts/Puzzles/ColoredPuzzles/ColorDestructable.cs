
using System.Collections.Generic;
using UnityEngine;

public class ColorDestructable : ColorPuzzle
{
    [SerializeField] private int health;
    private Explosion explosion;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing;
    private float flashDuration = 0.1f;
    private float flashTimer;

    [SerializeField] private List<GameObject> objectsToBeDestoryed = new List<GameObject>();

    private void Start()
    {
        explosion = GetComponentInChildren<Explosion>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public override void Interact()
    {
        if (isFlashing) return; // Avoid stacking flashes

        health--;
        StartCoroutine(FlashWhite());

        if (health <= 0)
        {
            foreach(GameObject obj in objectsToBeDestoryed)
            {
                Destroy(obj);
            }
            explosion.TriggerExplosoin(gameObject);
        }
    }

    private System.Collections.IEnumerator FlashWhite()
    {
        isFlashing = true;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}
