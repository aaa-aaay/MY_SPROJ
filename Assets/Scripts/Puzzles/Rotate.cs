using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 100f;

    void Update()
    {
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, direction * speed * Time.deltaTime);
    }
}
