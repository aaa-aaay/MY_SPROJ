using UnityEngine;

public class BossHands : ColorPuzzle
{
    public bool colorActivated = false;
    [SerializeField] private int damage = 1;


    public override void Interact()
    {
        if (!colorActivated) return;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }

        base.OnTriggerEnter2D(collision);
    }

}
