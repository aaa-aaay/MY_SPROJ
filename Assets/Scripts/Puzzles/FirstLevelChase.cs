using Unity.VisualScripting;
using UnityEngine;

public class FirstLevelChase : MonoBehaviour
{

    [SerializeField] Laser laser;
    private WaypointMovingPlatform laserMovement;
    [SerializeField] Transform startingTransofrm;

    private PlayerController player1;
    private PlayerController player2;

    private bool player1Enter;
    private bool player2Enter;
    private bool puzzleStarted;


    private void Start()
    {
        laser.DisableLaser();
        laserMovement = laser.gameObject.GetComponent<WaypointMovingPlatform>();
        laser.gameObject.SetActive(false);

        player1Enter = false;
        player2Enter = false;
        puzzleStarted = false;
    }

    private void Update()
    {

        if (puzzleStarted)
        {
            if(player1 is IDeath dying1 && player2 is IDeath dying2){


                if (dying1.IsDead && !dying2.IsDead)
                {
                    dying2.StartDying();
                    RestartPuzzle();

                }
                else if (dying2.IsDead && !dying1.IsDead)
                {
                    dying1.StartDying();
                    RestartPuzzle();

                }


            }


        }
    }

    private void RestartPuzzle()
    {
        player1Enter = false;
        player2Enter = false;
        puzzleStarted = false;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!puzzleStarted && collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player.playerNo == 1) { player1Enter = true; player1 = player;  }
            else if (player.playerNo == 2) { player2Enter = true; player2 = player;  }

            if (player1Enter && player2Enter) {
                puzzleStarted = true;
                laser.gameObject.SetActive(true);
                laser.EnableLaser();
                laser.transform.position = startingTransofrm.position;


                Debug.Log("Laser Started");
            } 
        }
    }
}
