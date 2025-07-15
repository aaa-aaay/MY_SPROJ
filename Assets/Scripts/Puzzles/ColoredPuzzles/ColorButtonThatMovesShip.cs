using UnityEngine;

public class ColorButtonThatMovesShip : ColorPuzzle
{
    [SerializeField] private GameObject ship;

    [SerializeField] private float shipMoveBackSpeed = 1f;       // Drift speed to the left
    [SerializeField] private float shipNudgeRightDistance = 2f;  // Nudge distance to the right
    [SerializeField] private float moveSpeed = 5f;               // Move speed toward target
    [SerializeField] private Transform endLimit;                 // Max right limit

    private Vector2 targetPosition;
    private Vector2 startPosition;

    private float driftPauseTimer = 0f;

    private void Start()
    {
        startPosition = ship.transform.position;
        targetPosition = startPosition;
    }

    public override void Interact()
    {
        // Nudge right but clamp to endLimit
        targetPosition += Vector2.right * shipNudgeRightDistance;
        targetPosition.x = Mathf.Min(targetPosition.x, endLimit.position.x);

        // Pause drifting for a short duration to allow visible forward movement
        driftPauseTimer = 1f; // 1 second pause
    }

    private void Update()
    {
        // Countdown the drift pause timer
        if (driftPauseTimer > 0f)
        {
            driftPauseTimer -= Time.deltaTime;
        }

        // If timer is done, drift left slowly
        if (driftPauseTimer <= 0f)
        {
            targetPosition += Vector2.left * shipMoveBackSpeed * Time.deltaTime;
            targetPosition.x = Mathf.Clamp(targetPosition.x, startPosition.x, endLimit.position.x);
        }

        // Move ship smoothly toward target position
        Vector2 currentPosition = ship.transform.position;
        ship.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
