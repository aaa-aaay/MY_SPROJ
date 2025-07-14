using UnityEngine;

public class BossHands : ColorPuzzle
{
    public bool colorActivated = false;


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

        base.OnTriggerEnter2D(collision);
    }

}
