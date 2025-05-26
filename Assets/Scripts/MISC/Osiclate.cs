using UnityEngine;

public class Osiclate : MonoBehaviour
{
    public float moveDistance = 5f;
    public float speed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * moveDistance;
        transform.position = new Vector3(startPosition.x + x, startPosition.y, startPosition.z);
    }
}
