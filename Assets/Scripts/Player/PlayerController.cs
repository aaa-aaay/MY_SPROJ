

using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour, IDeath
{
    private PlayerInput _PlayerInput;
    private Rigidbody2D _rb;

    private MovementController _MovementController;
    private AnimationManager _AnimationManager;

    //Player Actions
    public InputAction moveAction;
    public InputAction jumpAction;
    private InputAction dashAction;
    private InputAction hurtAction;
    public InputAction interactAction;
    public InputAction abilityAction;
    public InputAction abilityAction2;
    public InputAction abilityAction3;
    public InputAction aimAction;
    public InputAction changeCamAction;

    //respawning
    public Transform repsawnPosition;


    [HideInInspector] public float moveSpeed = 10;
    [SerializeField] private float slowdownSpeed = 7;
    [SerializeField] public float jumpSpeed = 10;
    [SerializeField] private float dashPower = 4.0f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private int startingHealth = 5;
    [SerializeField] private float maxSlideSpeed = 6f;
    [SerializeField] public int playerNo;

    [HideInInspector] public float dirH = 0.0f;
    private int health; 
    public float rayCastGroundLength = 0.5f;
    private bool isJump;
    private bool canDoubleJump;
    private bool haveDoubleJumped;
    private bool dashReset = true;
    private bool dash = false;
    private bool isDashing = false;
    [HideInInspector] public bool disableHorizontalMove = false;
    private bool frozen = false;

    [SerializeField] private Volume playerVolume;
    [SerializeField] private SpriteRenderer sRenderer;
    private Color originalColor;
    private bool stopRevive;
    public Transform RespawnPosition { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        _PlayerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _MovementController = GetComponent<MovementController>();
        _AnimationManager = GetComponentInChildren<AnimationManager>();

        moveAction = _PlayerInput.actions["Move"];
        jumpAction = _PlayerInput.actions["Jump"];
        dashAction = _PlayerInput.actions["Dash"];
        hurtAction = _PlayerInput.actions["Hurt"];
        interactAction = _PlayerInput.actions["Interact"];
        abilityAction = _PlayerInput.actions["Ability"];
        abilityAction2 = _PlayerInput.actions["Ability2"];
        abilityAction3 = _PlayerInput.actions["Ability3"];
        aimAction = _PlayerInput.actions["Look"];
        changeCamAction = _PlayerInput.actions["SwitchCamera"];


        PPManager.Instance.SetPlayerVolunmes(playerVolume, playerNo);

        haveDoubleJumped = false;
        isDashing = false;
        stopRevive = false;
        health = startingHealth;
        originalColor = sRenderer.color;
    }
    void Start()
    {



    }
    void Update()
    {

        //moving left right
        if (IsDead || frozen) return;
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        dirH = Mathf.Lerp(dirH, moveInput.x, Time.deltaTime * slowdownSpeed);
        if (dirH > 0) gameObject.transform.localScale = new Vector2(1, 1);
        else gameObject.transform.localScale = new Vector2(-1, 1);


        //jump and double jump
        if (jumpAction.WasPressedThisFrame() && GetIsGrounded()) {
            isJump = true;

        }
        if (jumpAction.WasPressedThisFrame() && canDoubleJump && !haveDoubleJumped) { 
            canDoubleJump = false;
            haveDoubleJumped = true;
            isJump = true;
        }

        if (dashAction.WasPressedThisFrame() && dashReset && !disableHorizontalMove)
        {
            dash = true;
        }
        if(hurtAction.WasPressedThisFrame())
        {
            PPManager.Instance.EnableVignette(true,playerNo);

        }

        if (changeCamAction.WasPressedThisFrame())
        {
            CameraManager.Instance.SwitchModeButton();
        }


        // slideResults = _rb.Slide(_rb.linearVelocity, Time.deltaTime, slideMovement);

        //animations
        if(disableHorizontalMove) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Fall);
        else if (isDashing) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Dash);
        else if (Mathf.Abs(dirH) > 0.1f && GetIsGrounded()) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Run);
        else if (_rb.linearVelocityY < -0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Fall);
        else if (_rb.linearVelocityY > 0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Jump);
        else _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Idle);





        Debug.DrawRay(transform.position, Vector2.down * rayCastGroundLength, Color.red);
        Debug.DrawRay(transform.position + Vector3.left * 0.8f, Vector2.down * rayCastGroundLength, Color.red);
        Debug.DrawRay(transform.position + Vector3.right * 0.8f, Vector2.down * rayCastGroundLength, Color.red);


    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(dirH) > 0.1f && !isDashing && !disableHorizontalMove)
        {
            _MovementController.MoveHorizontal(dirH * moveSpeed);
        }


        if (isJump)
        {
            if(disableHorizontalMove)
            _MovementController.MoveVertical(jumpSpeed,true);

            else _MovementController.MoveVertical(jumpSpeed);
            isJump = false;
            canDoubleJump = true;
        }
        bool isJumpHeld = jumpAction.IsPressed();
        _MovementController.playerVerticalMovement(isJumpHeld);



        if(haveDoubleJumped && GetIsGrounded())
        {
            haveDoubleJumped = false;
        }
        if (!dashReset && GetIsGrounded()) {
            dashReset = true;
        }

        if (dash)
        {
            dashReset = false;
            dash = false;

            if (!isDashing)
            {
                isDashing = true;
                float tempPower = dashPower;

                if (transform.localScale.x < 0)
                {

                    tempPower = -dashPower;

                }
                else if (transform.localScale.x > 0)
                {
                    tempPower = dashPower;
                }
                _MovementController.Rolling(tempPower);
                Invoke("RollComplete", dashDuration);

            }

        }


        if (disableHorizontalMove && GetIsGrounded())
        {
            Vector2 currentVel = _rb.linearVelocity;


            // OR: Clamp only horizontal speed while preserving vertical movement
            if (Mathf.Abs(currentVel.x) > maxSlideSpeed)
            {
                _rb.linearVelocity = new Vector2(Mathf.Sign(currentVel.x) * maxSlideSpeed, currentVel.y);
            }
        }





    }

    private bool GetIsGrounded()
    {

        Vector2 origin = transform.position;
        float rayLength = rayCastGroundLength;
        LayerMask groundMask = LayerMask.GetMask("Ground");

        // Center
        if (Physics2D.Raycast(origin, Vector2.down, rayLength, groundMask)) return true;

        // Slightly left
        if (Physics2D.Raycast(origin + Vector2.left * 0.8f, Vector2.down, rayLength, groundMask)) return true;

        // Slightly right
        if (Physics2D.Raycast(origin + Vector2.right * 0.8f, Vector2.down, rayLength, groundMask)) return true;

        return false;
    }




    private void RollComplete()
    {
        isDashing = false;
        _MovementController.StopMovement();

    }


    private IEnumerator StartRevive()
    {

        _rb.constraints = RigidbodyConstraints2D.FreezeAll;


        yield return new WaitForSeconds(0.6f);
        PPManager.Instance.SetBlackAndWhite(true, playerNo);




        yield return new WaitForSeconds(3.0f);
        if (stopRevive) yield return null;
        PPManager.Instance.SetBlackAndWhite(false, playerNo);
        gameObject.transform.position = RespawnPosition.position;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        IsDead = false;
        health = startingHealth;
    }

    public void FreezePlayer(bool freeze)
    {
        if(freeze)
        {
            _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Idle);
            frozen = true;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        } 


        else
        {
            frozen = false;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void IDeath.StartDying()
    {
        if(IsDead) return;
        IsDead = true;
        _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Death);
        if (stopRevive) return;
        StartCoroutine(StartRevive());
    }

    public void TakeDamage(int damage)
    {
        if(IsDead) return;
        health = health - damage;

        StartCoroutine(HitEffect());
        //play hit effect
        PPManager.Instance.EnableVignette(true,playerNo);

        if(health <= 0)
        {
            IDeath deathHandler = GetComponent<IDeath>();
            if (deathHandler != null)
            {
                deathHandler.StartDying();
            }
        }
    }

    private System.Collections.IEnumerator HitEffect()
    {

        sRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sRenderer.color = originalColor;
    }

    public void SetStopRevive(bool stopRevive, bool revive)
    {
        this.stopRevive = stopRevive;
        if (revive) {
            gameObject.transform.position = RespawnPosition.position;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            IsDead = false;
            health = startingHealth;



        }

    }
}
