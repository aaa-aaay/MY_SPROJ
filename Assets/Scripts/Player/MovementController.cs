using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float LowJumpMultiplier = 2.0f;
    private Coroutine rollCoroutine;
    private float gravityScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void MoveHorizontal(float vX) 
    {
        rb.linearVelocity = new Vector2(vX, rb.linearVelocity.y);
    }
    public void MoveVertical(float vY, bool keepVelocity = false)
    {
        if(!keepVelocity)
        rb.linearVelocity = Vector2.up * vY;
        else rb.linearVelocity = new Vector2(rb.linearVelocity.x, vY);


    }

    public float playerVerticalMovement(bool isJumpHeld)
    {


        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !isJumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }
        return rb.linearVelocity.y;
    }

    public void StopMovement()
    {
        rb.gravityScale = gravityScale;
        rb.linearVelocity = Vector2.zero;
    }


    public void Rolling(float rollPower)
    {
        //rb.linearVelocity = new Vector2(rollPower, 0);
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(rollPower, 0f);

        //rb.linearVelocity = Vector2.right * rollPower;

        //rb.AddForce(Vector2.right * rollPower,ForceMode2D.Impulse);

    }

    public void MoveInDirection(Vector2 direction)
    {
        rb.linearVelocity = direction * LowJumpMultiplier;

    }
}
