using UnityEngine;

public class Osiclate : MonoBehaviour
{
    public float moveDistance = 5f;
    public float speed = 2f;
    public float pauseTime = 0.0f;

    private Vector3 startPosition;
    private float timeCounter;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private float lastDirection = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTime)
            {
                isPaused = false;
                pauseTimer = 0f;
            }
            return;
        }

        timeCounter += Time.deltaTime;
        float x = Mathf.Sin(timeCounter * speed) * moveDistance;
        transform.position = new Vector3(startPosition.x + x, startPosition.y, startPosition.z);

        // Detect direction change to trigger pause
        float direction = Mathf.Cos(timeCounter * speed);
        if (Mathf.Sign(direction) != Mathf.Sign(lastDirection) && Mathf.Abs(direction) < 0.1f)
        {
            isPaused = true;
        }
        lastDirection = direction;
    }
}
