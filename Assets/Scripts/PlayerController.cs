

using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _PlayerInput;
    private Rigidbody2D _rb;

    private MovementController _MovementController;
    private AnimationManager _AnimationManager;

    //Player Actions
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction dashAction;


    public float moveSpeed = 10;
    public float slowdownSpeed = 7;
    public float jumpSpeed = 10;
    public float dashPower = 4.0f;
    public float dashDuration = 0.3f;

    public float dirH = 0.0f;
    public float rayCastGroundLength = 0.5f;
    private bool isJump;
    private bool canDoubleJump;
    private bool haveDoubleJumped;
    private bool dashReset = true;
    private bool dash = false;
    private bool isDashing = false;
    private bool isDead = false;


    private void Awake()
    {
        _PlayerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _MovementController = GetComponent<MovementController>();
        _AnimationManager = GetComponentInChildren<AnimationManager>();

        moveAction = _PlayerInput.actions["Move"];
        jumpAction = _PlayerInput.actions["Jump"];
        dashAction = _PlayerInput.actions["Dash"];

        haveDoubleJumped = false;
        isDashing = false;


    }
    void Update()
    {

        //moving left right
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

        if (dashAction.WasPressedThisFrame() && dashReset)
        {
            dash = true;
        }


        // slideResults = _rb.Slide(_rb.linearVelocity, Time.deltaTime, slideMovement);

        //animations
        if (!isDead)
        {
            if (isDashing) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Dash);
            else if (Mathf.Abs(dirH) > 0.1f && GetIsGrounded()) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Run);
            else if (_rb.linearVelocityY < -0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Fall);
            else if (_rb.linearVelocityY > 0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Jump);
            else _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Idle);
        }





        Debug.DrawRay(transform.position, Vector2.down * rayCastGroundLength, Color.red);
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(dirH) > 0.1f && !isDashing)
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

    public void PlayerDeath()
    {
        _PlayerInput.enabled = false;
        _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Death);
        StartCoroutine(FreezeAfterDeathAnim());
    }

    private IEnumerator FreezeAfterDeathAnim()
    {
        yield return new WaitForSeconds(0.1f); 
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isDead = true;
    }


}
