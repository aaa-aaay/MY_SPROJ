using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 100f;

    [SerializeField] private bool oscilate = false;
    [SerializeField] private float angle = 45f;

    private float directionMultiplier;

    void Start()
    {
        // Start direction: clockwise = -1, anticlockwise = +1
        directionMultiplier = clockwise ? -1f : 1f;
    }

    void Update()
    {
        if (!oscilate)
        {
            transform.Rotate(0f, 0f, directionMultiplier * speed * Time.deltaTime);
        }
        else
        {
            float z = Mathf.Sin(Time.time * speed * Mathf.Deg2Rad) * angle;
            transform.localRotation = Quaternion.Euler(0f, 0f, z * directionMultiplier);
        }
    }
}
