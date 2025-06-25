using UnityEngine;

public class WaypointMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform targetPos;
    [SerializeField] private bool oscilate = false;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float pauseTime = 1f;
    [SerializeField] private PuzzleButton button;

    private Transform currentTarget;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private bool reachedTarget = false;

    void Start()
    {
        transform.position = startPos.position;
        startPos.position = transform.position;
        currentTarget = targetPos;
    }

    void Update()
    {
        if (button != null)
        {
            if (!button.buttonPressed) return;
        }

        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTime)
            {
                isPaused = false;
                pauseTimer = 0f;
                if (oscilate)
                {
                    currentTarget = (currentTarget == targetPos) ? startPos : targetPos;
                }
            }
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (!reachedTarget && Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            reachedTarget = true;
            isPaused = true;
        }

        if (Vector3.Distance(transform.position, currentTarget.position) > 0.01f)
        {
            reachedTarget = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 originalScale = collision.transform.localScale;
            collision.transform.SetParent(transform, true);
            collision.transform.localScale = originalScale;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
