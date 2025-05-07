

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



    public float moveSpeed = 10;
    public float slowdownSpeed = 7;
    public float jumpSpeed = 10;
    private float dirH = 0.0f;
    private bool isJump;
    private bool canDoubleJump;
    private bool haveDoubleJumped;

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

        haveDoubleJumped = false;
    }
    void Update()
    {
        


        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        dirH = Mathf.Lerp(dirH, moveInput.x, Time.deltaTime * slowdownSpeed);
        if (dirH > 0) gameObject.transform.localScale = new Vector2(1, 1);
        else gameObject.transform.localScale = new Vector2(-1, 1);

        if (jumpAction.WasPressedThisFrame() && GetIsGrounded()) {
            isJump = true;

        }
        if (jumpAction.WasPressedThisFrame() && canDoubleJump && !haveDoubleJumped) { 
            canDoubleJump = false;
            haveDoubleJumped = true;
            isJump = true;
        }



        //animations
        if(_rb.linearVelocityY < -0.1) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Fall);
        else if (_rb.linearVelocityY > 0.1 ) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Jump);
        else if (Mathf.Abs(dirH) > 0.1f && GetIsGrounded()) _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Run);
        else _AnimationManager.ChangeAnimationState(AnimationManager.AnimationState.Idle);





        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red);
    }

    private void FixedUpdate()
    {

        if (dirH != 0.0f)
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
    }

    private bool GetIsGrounded()
    {

        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));

    }
}
