using UnityEngine;

public class NodeShootAbility : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float destoryBulletCD = 3.0f;
    private PlayerController player;
    private GameObject currentBulletOBJ;
    private float timer;

    private bool bulletFiring;
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        timer = 0;
    }

    private void Update()
    {
        if ((bulletFiring))
        {
            timer += Time.deltaTime;

            if (currentBulletOBJ == null) {
                timer = 0;
                bulletFiring = false;
            
            }


            if (timer > destoryBulletCD)
            {
                timer = 0;
                bulletFiring = false;
                if (currentBulletOBJ != null) { 
                    Destroy(currentBulletOBJ); //can put this as a function in the bullet GO and play an animation instead (for improvement)
                }
            }
        }

        if (player.abilityAction.WasCompletedThisFrame() && !bulletFiring)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {

        Vector2 aimDirection = player.aimAction.ReadValue<Vector2>().normalized;
        if (aimDirection == Vector2.zero) aimDirection = new Vector2(transform.parent.transform.localScale.x, 0);

        currentBulletOBJ = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = currentBulletOBJ.GetComponent<Rigidbody2D>();
        NodeShootBullet currentBullet = currentBulletOBJ.GetComponent<NodeShootBullet>();

        if (bulletRb != null && currentBullet !=null)
        {
            bulletFiring = true;

            bulletRb.gravityScale = 0f;
            bulletRb.linearVelocity = aimDirection * bulletSpeed;
            currentBullet.SetInput(player.abilityAction);



        }
    }


}
