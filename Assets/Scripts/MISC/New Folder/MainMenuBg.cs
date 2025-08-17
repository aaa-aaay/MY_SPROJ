using UnityEngine;

public class MainMenuBg : MonoBehaviour
{
    private float startPos, length;
    public float parallaxEffect = 1f;
    public float moveSpeed = 1f;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        // Move the background left
        float distance = parallaxEffect * moveSpeed * Time.deltaTime;
        transform.position += Vector3.left * distance;

        // Wrap background when it moves fully off-screen
        if (transform.position.x < startPos - length)
        {
            transform.position += new Vector3(length * 2f, 0f, 0f);
        }
    }
}
