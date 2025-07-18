using UnityEngine;

public class ColorButtonThatMovesShip : ColorPuzzle
{
    [SerializeField] private GameObject ship;

    [SerializeField] private float shipMoveBackSpeed = 1f;       // Drift speed left
    [SerializeField] private float shipNudgeRightDistance = 2f;  // Nudge distance
    [SerializeField] private float moveSpeed = 5f;               // Move speed
    [SerializeField] private Transform endLimit;                 // Max right limit

    private Vector2 targetPosition;
    private Vector2 startPosition;


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
    }

    private void Update()
    {

        targetPosition += Vector2.left * shipMoveBackSpeed * Time.deltaTime;

        // Clamp to not go past startPosition (left limit)
        targetPosition.x = Mathf.Clamp(targetPosition.x, startPosition.x, endLimit.position.x);

        // Move ship toward target position
        Vector2 currentPosition = ship.transform.position;
        ship.transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}