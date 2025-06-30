using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControls : MonoBehaviour, IDeath
{
    [SerializeField]private int defaultShipHealth;
    [SerializeField]private GameObject sprite;
    private Animator animator;
    private InputAction playerAction;
    private bool shipStarted;
    


    public float upwardForce = 5f;
    public float forwardSpeed = 5f;
    private Rigidbody2D rb;
    private bool jump;

    //Shooting Mech
    [SerializeField] private Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private float shootPower =  10.0f;
    private float fireCooldown = 0f;
    private bool canShoot;


    [HideInInspector]public float shipHealth = 3;

    public Transform RespawnPosition { get; set; }
    public bool IsDead { get; set; }

    private void Start()
    {
        IsDead = false;
        rb =GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        shipHealth = defaultShipHealth;
    }

    public void StartCar()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        shipStarted = true;

    }

    private void Update()
    {
        if (IsDead || !shipStarted) return;
        // Pressing space -> go up
        if (PlayerManager.Instance.GetPlayer1().jumpAction.IsInProgress())
        {
            Debug.Log("Pressed");
            jump = true;

        }


        if (PlayerManager.Instance.GetPlayer2().abilityAction.IsInProgress()) {
            fireCooldown -= Time.deltaTime;


            if (fireCooldown <= 0f)
            {
                ShootBullet();
                fireCooldown = fireRate;
            }

        }
        else
        {
            fireCooldown = 0f;
        }



        if (shipHealth <= 0)
        {

            StartDying();
        }




    }

    private void FixedUpdate()
    {
        if(IsDead) return;
        if (shipStarted)
        {

            // Always move forward
            rb.linearVelocity = new Vector2(forwardSpeed, rb.linearVelocity.y);


            if (jump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, upwardForce);
                jump = false;
            }

            // Rotate to face movement direction with smoothing and clamping
            if (rb.linearVelocity.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;

                // Clamp the target angle to max range (e.g. -30 to +30 degrees)
                targetAngle = Mathf.Clamp(targetAngle, -30f, 30f);

                float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.fixedDeltaTime * 3f);
                rb.MoveRotation(smoothedAngle);
            }


        }

    }



    private void ShootBullet()
    {
        Vector2 aimInput = PlayerManager.Instance.GetPlayer2().aimAction.ReadValue<Vector2>();

        if (aimInput == Vector2.zero)
        {
            aimInput = Vector2.left;
        }

        GameObject bulletOBJ = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletOBJ.GetComponent<Bullet>();
        bullet.Shoot(aimInput, shootPower);
    }

    public void StartDying()
    {
        IsDead = true;
        StartCoroutine(StartRevive());
    }



    private IEnumerator StartRevive()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("explode");
        sprite.SetActive(false);
        //play explosion animation


        yield return new WaitForSeconds(0.3f);
        PPManager.Instance.SetBlackAndWhite(true, 3);

        yield return new WaitForSeconds(3.0f);
        PPManager.Instance.SetBlackAndWhite(false, 3);
        gameObject.transform.position = RespawnPosition.position;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sprite.SetActive(true);
        shipHealth = defaultShipHealth;
        IsDead = false;
    }
}
