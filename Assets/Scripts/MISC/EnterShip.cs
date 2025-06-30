using UnityEngine;

public class EnterShip : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject ship;
    [SerializeField] ShipControls shipControls;
    [SerializeField] GameObject GolemBoss;
    [SerializeField] float  cameraOffset;

    private bool player1In;
    private bool player2In;
    private bool player1Boarded;
    private bool player2Boarded;
    private int shipCount;

    private void Start()
    {
        canvas.SetActive(false);
        player1Boarded = false;
        player2Boarded = false;
        GolemBoss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            if (other.gameObject.GetComponent<PlayerController>().playerNo == 1)
                player1In = true;
            else
                player2In = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
            if (collision.gameObject.GetComponent<PlayerController>().playerNo == 1)
                player1In = false;
            else
                player2In = false;
        }
    }

    private void Update()
    {
        if (player1In && !player1Boarded)
        {
            var player = PlayerManager.Instance.player1.gameObject;
            if (player.GetComponent<PlayerController>().interactAction.WasPressedThisFrame())
            {
                BoardShip(player);
                player1Boarded = true;
            }
        }

        if (player2In && !player2Boarded)
        {
            var player = PlayerManager.Instance.player2.gameObject;
            if (player.GetComponent<PlayerController>().interactAction.WasPressedThisFrame())
            {
                BoardShip(player);
                player2Boarded = true;
            }
        }
    }

    private void BoardShip(GameObject player)
    {
        player.transform.SetParent(ship.transform, false);
        player.transform.localPosition = Vector3.zero;
        player.GetComponentInChildren<SpriteRenderer>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        shipCount++;

        if (shipCount == 2)
        {

            GolemBoss.SetActive(true);
            shipControls.StartCar();
            CameraManager.Instance.SwitchMode(CameraManager.mode.Single, ship, new Vector2(cameraOffset, 0));
        }
    }
}
