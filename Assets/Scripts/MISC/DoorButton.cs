using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private Canvas buttonUI;
    [SerializeField] private int playerNoFor;

    private PlayerController playerController;

    public bool buttonPressed;
    private bool playerIn;
    private void Start()
    {
        buttonUI.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerNoFor == 1 && collision.gameObject.name.StartsWith("Player1") || playerNoFor == 2 && collision.gameObject.name.StartsWith("Player2") || playerNoFor == 3)
        {
            buttonUI.enabled = true;
            playerIn = true;
            playerController = collision.gameObject.GetComponent<PlayerController>();
            

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerNoFor == 1 && collision.gameObject.name.StartsWith("Player1") || playerNoFor == 2 && collision.gameObject.name.StartsWith("Player2") || playerNoFor == 3)
        {
            buttonUI.enabled = false;
        }

    } 

    private void Update()
    {
        if(!playerIn) return;
        if (playerController == null) return;

        if (playerController.interactAction.WasPressedThisFrame())
        {
            buttonUI.enabled = false;
            buttonPressed = true;
            playerController.FreezePlayer(true);
        }

    }
}
