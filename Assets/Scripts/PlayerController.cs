

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementController _MovementController;
    private PlayerInput _PlayerInput;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _MovementController = GetComponent<MovementController>();
        moveAction = _PlayerInput.actions["Move"];
        jumpAction = _PlayerInput.actions["Jump"];

        haveDoubleJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        


        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        dirH = Mathf.Lerp(dirH, moveInput.x, Time.deltaTime * slowdownSpeed);

        if (jumpAction.WasPressedThisFrame() && GetIsGrounded()) {
            isJump = true;
        }
        if (jumpAction.WasPressedThisFrame() && canDoubleJump && !haveDoubleJumped) { 
            canDoubleJump = false;
            haveDoubleJumped = true;
            isJump = true;
        }



        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
    }

    private void FixedUpdate()
    {

        if (dirH != 0.0f)
        _MovementController.MoveHorizontal(dirH * moveSpeed);

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

        return Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground"));

    }
}
