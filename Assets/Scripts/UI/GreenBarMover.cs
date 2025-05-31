using UnityEngine;

public class GreenBarMover : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float moveRange = 100f;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private bool movingUp = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float direction = movingUp ? 1 : -1;
        rectTransform.anchoredPosition += new Vector2(0, direction * moveSpeed * Time.deltaTime);

        float distance = rectTransform.anchoredPosition.y - startPos.y;

        if (movingUp && distance >= moveRange)
            movingUp = false;
        else if (!movingUp && distance <= -moveRange)
            movingUp = true;
    }
}
