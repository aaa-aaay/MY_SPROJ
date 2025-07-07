using Unity.Cinemachine;
using UnityEngine;

public class EnterShip : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject ship;
    [SerializeField] private ShipControls shipControls;
    [SerializeField] private GameObject GolemBoss;
    [SerializeField] private float cameraOffset;
    [SerializeField] Camera sceneCamera;
    [SerializeField] CinemachineCamera cineMachineCamera;

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
        shipCount = 0;
        GolemBoss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerController>();
            if (controller == null) return;

            if (controller.playerNo == 1)
                player1In = true;
            else
                player2In = true;

            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var controller = other.GetComponent<PlayerController>();
            if (controller == null) return;

            if (controller.playerNo == 1)
                player1In = false;
            else
                player2In = false;

            if (!player1In && !player2In)
                canvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (player1In && !player1Boarded)
        {
            var player = PlayerManager.Instance.player1.gameObject;
            if (player.GetComponent<PlayerController>().interactAction.WasPressedThisFrame())
            {
                if (BoardShip(player))
                    player1Boarded = true;
            }
        }

        if (player2In && !player2Boarded)
        {
            var player = PlayerManager.Instance.player2.gameObject;
            if (player.GetComponent<PlayerController>().interactAction.WasPressedThisFrame())
            {
                if (BoardShip(player))
                    player2Boarded = true;
            }
        }
    }

    private bool BoardShip(GameObject player)
    {
        if (player.transform.parent == ship.transform)
            return false; // Already boarded

        player.transform.SetParent(ship.transform, false);
        player.transform.localPosition = Vector3.zero;

        var sr = player.GetComponentInChildren<SpriteRenderer>();
        if (sr) sr.enabled = false;

        var col = player.GetComponent<Collider2D>();
        if (col) col.enabled = false;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb) rb.bodyType = RigidbodyType2D.Static;

        shipCount++;

        if (shipCount == 2)
        {
            GolemBoss.SetActive(true);
            shipControls.StartCar();
            CameraManager.Instance.SetMainCam(sceneCamera, cineMachineCamera);
            CameraManager.Instance.SwitchMode(CameraManager.mode.Single, ship, new Vector2(cameraOffset, 0));
        }

        return true;
    }
}
