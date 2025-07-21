using UnityEngine;

public class ColorButtonThatMovesShip : ColorPuzzle
{
    [SerializeField] private GameObject ship;

    [SerializeField] private float pushForce = 2f;            // How strong the nudge right is
    [SerializeField] private float returnSpeed = 1f;          // How quickly it returns left
    [SerializeField] private float moveLerpSpeed = 5f;        // How smooth the movement is
    [SerializeField] private Transform endLimit;              // Right boundary

    private Vector2 velocity = Vector2.zero;
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = ship.transform.position;
    }

    public override void Interact()
    {
        // Add push velocity to the right
        velocity += Vector2.right * pushForce;
    }

    private void Update()
    {
        Vector2 currentPosition = ship.transform.position;

        // Apply return force to bring it back
        velocity += Vector2.left * returnSpeed * Time.deltaTime;

        // Apply velocity
        currentPosition += velocity * Time.deltaTime;

        // Clamp within limits
        float clampedX = Mathf.Clamp(currentPosition.x, startPosition.x, endLimit.position.x);
        currentPosition = new Vector2(clampedX, currentPosition.y);

        // Smoothly move the ship
        ship.transform.position = Vector2.Lerp(ship.transform.position, currentPosition, moveLerpSpeed * Time.deltaTime);

        // Optional: damp velocity when near start position to prevent overshooting
        if (Mathf.Abs(currentPosition.x - startPosition.x) < 0.01f)
        {
            velocity = Vector2.zero;
        }
    }
}
