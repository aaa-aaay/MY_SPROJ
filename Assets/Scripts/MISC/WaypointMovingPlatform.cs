using UnityEngine;

public class WaypointMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform targetPos;
    [SerializeField] private bool oscilate = false;
    [SerializeField] private float speed = 2f;
    [SerializeField] PuzzleButton button;

    private Transform currentTarget;

    void Start()
    {
        transform.position = startPos.position;
        currentTarget = targetPos;
    }

    void Update()
    {


        if (button != null) {
            if (button.buttonPressed) {
                Move();



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

        // If reached target, switch direction if oscillating
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            if (oscilate)
            {
                currentTarget = (currentTarget == targetPos) ? startPos : targetPos;
            }
        }
    }
}
