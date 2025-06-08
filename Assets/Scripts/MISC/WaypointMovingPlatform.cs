using UnityEngine;

public class WaypointMovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform targetPos;
    [SerializeField] private bool oscilate = false;
    [SerializeField] private float speed = 2f;
    [SerializeField] PuzzleButton button;
    
    private bool reachedTarget = false;


    private Transform currentTarget;

    void Start()
    {
        transform.position = startPos.position;
        startPos.position = transform.position;
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

        if (!reachedTarget && Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            reachedTarget = true;

            if (oscilate)
            {
                currentTarget = (currentTarget == targetPos) ? startPos : targetPos;
            }
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
