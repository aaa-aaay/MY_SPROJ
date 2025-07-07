

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
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float dashPower = 4.0f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] public int playerNo;

    [HideInInspector] public float dirH = 0.0f;
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
        if (isDashing) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Dash);
        else if (Mathf.Abs(dirH) > 0.1f && GetIsGrounded()) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Run);
        else if (_rb.linearVelocityY < -0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Fall);
        else if (_rb.linearVelocityY > 0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Jump);
        else _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Idle);





        Debug.DrawRay(transform.position, Vector2.down * rayCastGroundLength, Color.red);
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(dirH) > 0.1f && !isDashing && !disableHorizontalMove)
        {
            _MovementController.MoveHorizontal(dirH * moveSpeed);
        }


        if (isJump)
        {
            _MovementController.MoveVertical(jumpSpeed);
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
    }

    private bool GetIsGrounded()
    {

        return Physics2D.Raycast(transform.position, Vector2.down, rayCastGroundLength, LayerMask.GetMask("Ground"));

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
        PPManager.Instance.SetBlackAndWhite(false, playerNo);
        gameObject.transform.position = RespawnPosition.position;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        IsDead = false;
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
        IsDead = true;
        _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Death);
        StartCoroutine(StartRevive());
    }

}
