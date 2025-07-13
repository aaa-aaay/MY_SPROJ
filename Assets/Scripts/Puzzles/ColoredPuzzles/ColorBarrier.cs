using UnityEngine;

public class ColorBarrier : ColorPuzzle
{
    private Collider2D circleCollider;
    private SpriteRenderer spriteRender;
    private void Start()
    {
        circleCollider = GetComponent<Collider2D>();
        spriteRender = GetComponent<SpriteRenderer>();

    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        ColorGrenade colorGrenade = collision.GetComponent<ColorGrenade>();
        if(colorGrenade != null)
        {
            if (colorGrenade.IsRedColor && redColor)
            {
                Interact();
            }
            if (!colorGrenade.IsRedColor && !redColor)
            {
                Interact();
            }
        }

    }
    public override void Interact()
    {
        circleCollider.enabled = false;
        spriteRender.enabled = false;
    }
}
