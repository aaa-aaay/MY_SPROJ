using System.Runtime.CompilerServices;
using UnityEngine;

public class FightEnemyPuzzle : MonoBehaviour
{
    [SerializeField] Laser laser1;
    [SerializeField] Laser laser2;

    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;


    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;

    [SerializeField] private int maxSpawnedEnemies;

    private GameObject currentEnemy1, currentEnemy2;

    private int enemy1Count, enemy2Count;


    private bool player1Enter;
    private bool player2Enter;
    private bool puzzleStarted;
    private bool puzzleFinished;


    private void Start()
    {
        player1Enter = false;
        player2Enter = false;
        puzzleStarted = false;
        puzzleFinished = false;

        enemy1Count = 0;
        enemy2Count = 0;

        laser1.DisableLaser();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(puzzleStarted  || puzzleFinished) return;


        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
            if (p.playerNo == 1) player1Enter = true;
            else if (p.playerNo == 2) player2Enter = true;

            if (player1Enter && player2Enter) { StartPuzzle(); }
        }
    }


    private void Update()
    {

        if (puzzleFinished) return;
        if (puzzleStarted) {


            if(currentEnemy1 == null)
            {
                SpawnEnemy(1);
            }
            if (currentEnemy2 == null) {
                SpawnEnemy(2);
            }


            if (enemy1Count >= maxSpawnedEnemies &&
                enemy2Count >= maxSpawnedEnemies &&
                IsDead(currentEnemy1) &&
                IsDead(currentEnemy2))
            {

                Debug.Log("the puzzle is over 9");
                puzzleFinished = true;
                laser2.DisableLaser();
            }




        }



    }

    private void StartPuzzle()
    {
        puzzleStarted = true;
        laser1.EnableLaser();

        SpawnEnemy(1);
        SpawnEnemy(2);


    }

    private void SpawnEnemy(int enemyno)
    {





        if (enemyno == 1 && enemy1Count < maxSpawnedEnemies) {
            
            currentEnemy1 = Instantiate(enemy1, spawnPoint1.position, Quaternion.identity);
            enemy1Count++;

        }
        if (enemyno == 2 && enemy2Count < maxSpawnedEnemies) {
            
            currentEnemy2 = Instantiate(enemy2, spawnPoint2.position, Quaternion.identity);
            enemy2Count++;


        }
    }

    private bool IsDead(GameObject go)
    {
        return go == null || go.Equals(null);
    }

}
