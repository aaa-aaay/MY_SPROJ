using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorGrenade : Bullet, ColoredAbilities
{
    public bool IsRedColor { get; set; }

    [SerializeField] private bool isRedColor;

    private InputAction triggerAction;
    private Rigidbody2D rb;

    [SerializeField] private BoxCollider2D boxcollider;
    [SerializeField] private CircleCollider2D circlecollider;





    private void Update()
    {
        if (triggerAction != null && triggerAction.WasPressedThisFrame())
        {
            boxcollider.enabled = false;
            circlecollider.enabled = true;
            DestroyBullet();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & interactableLayers.value) != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    ColorBarrier barrier = GetComponent<ColorBarrier>();
    //    if (barrier != null)
    //    {
    //        barrier.Interact();
    //    }
    //}

    public override void Shoot(Vector2 direction = default, float power = 0)
    {
        circlecollider.enabled = false;
        IsRedColor = isRedColor;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * power, ForceMode2D.Impulse);

        //if (direction != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }
    public override void SetInputAction(InputAction action)
    {

        this.triggerAction = action;
    }


    public override void DestroyBullet()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        base.DestroyBullet();
        return;
    }
}
