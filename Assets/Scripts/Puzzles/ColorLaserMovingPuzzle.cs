using UnityEngine;

public class ColorLaserMovingPuzzle : MonoBehaviour
{
    private bool puzzleStarted;
    private bool puzzleEnded;
    [SerializeField] private Collider2D colldier;

    [SerializeField] private GameObject[] rightSideLasers;
    [SerializeField] private GameObject[] leftSideLasers;

    [SerializeField] private Vector3 rightTargetOffset = new Vector3(-3f, 0, 0);
    [SerializeField] private Vector3 leftTargetOffset = new Vector3(3f, 0, 0);
    [SerializeField] private float moveSpeed = 2f;

    private Vector3[] rightStartPositions;
    private Vector3[] leftStartPositions;

    [SerializeField] private Laser laser1;
    [SerializeField] private Laser laser2;


    private bool player1Entered;
    private bool player2Entered;

    private void Start()
    {
        puzzleStarted = false;
        puzzleEnded = false;
        player1Entered = false;
        player2Entered = false;
        laser1.DisableLaser();
        laser2.EnableLaser();

        // Store initial positions
        rightStartPositions = new Vector3[rightSideLasers.Length];
        leftStartPositions = new Vector3[leftSideLasers.Length];

        for (int i = 0; i < rightSideLasers.Length; i++)
        {
            if (rightSideLasers[i] != null)
            {
                rightStartPositions[i] = rightSideLasers[i].transform.position;
                rightSideLasers[i].SetActive(false); // Deactivate
            }
        }

        for (int i = 0; i < leftSideLasers.Length; i++)
        {
            if (leftSideLasers[i] != null)
            {
                leftStartPositions[i] = leftSideLasers[i].transform.position;
                leftSideLasers[i].SetActive(false); // Deactivate
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if(player.playerNo == 1) { player1Entered = true; }
            else if(player.playerNo == 2) { player2Entered = true; }

            if(player1Entered && player2Entered) puzzleStarted = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)  
    {
        if (other.gameObject.CompareTag("Player"))
        {
            puzzleStarted = true;
            laser1.EnableLaser();
            laser2.EnableLaser();
        }
    }

    private void Update()
    {
        if (puzzleEnded)
        {
            laser1.DisableLaser();
            laser2.DisableLaser();
            return;
        }

        if (puzzleStarted)
        {
            // Move right lasers left
            for (int i = 0; i < rightSideLasers.Length; i++)
            {
                if (rightSideLasers[i] != null)
                {
                    Vector3 target = rightStartPositions[i] + rightTargetOffset;
                    rightSideLasers[i].transform.position = Vector3.MoveTowards(
                        rightSideLasers[i].transform.position,
                        target,
                        moveSpeed * Time.deltaTime);
                }
            }

            // Move left lasers right
            for (int i = 0; i < leftSideLasers.Length; i++)
            {
                if (leftSideLasers[i] != null)
                {
                    Vector3 target = leftStartPositions[i] + leftTargetOffset;
                    leftSideLasers[i].transform.position = Vector3.MoveTowards(
                        leftSideLasers[i].transform.position,
                        target,
                        moveSpeed * Time.deltaTime);
                }
            }

            // Check if player left puzzle
            if (colldier != null && !colldier.IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                resetPuzzle();
            }

            // End puzzle if all lasers are destroyed
            if (AllLasersDestroyed())
            {
                puzzleEnded = true;
            }
        }
    }

    private void resetPuzzle()
    {
        puzzleStarted = false;
        laser1.DisableLaser();
        laser2.EnableLaser();

        for (int i = 0; i < rightSideLasers.Length; i++)
        {
            if (rightSideLasers[i] != null)
                rightSideLasers[i].transform.position = rightStartPositions[i];
        }

        for (int i = 0; i < leftSideLasers.Length; i++)
        {
            if (leftSideLasers[i] != null)
                leftSideLasers[i].transform.position = leftStartPositions[i];
        }
    }

    private bool AllLasersDestroyed()
    {
        foreach (var laser in rightSideLasers)
        {
            if (laser != null) return false;
        }
        foreach (var laser in leftSideLasers)
        {
            if (laser != null) return false;
        }
        return true;
    }
}
