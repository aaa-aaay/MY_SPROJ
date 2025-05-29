using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    [SerializeField] private float playerNoFor;
    [SerializeField] private Canvas buttonUI;
    private bool playerIn;
    public bool buttonPressed;
    private PlayerController player;


    private void Start()
    {
        buttonUI.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.StartsWith("Player")) {

            player = collision.gameObject.GetComponent<PlayerController>();

            playerIn = true;
            if(player.playerNo == 1)
            {
                buttonUI.gameObject.layer = 6;
            }
            if (player.playerNo == 2) {
                buttonUI.gameObject.layer = 7;
            }
            buttonUI.enabled = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Player"))
        {
            playerIn = false;
            buttonUI.enabled = false;

        }
    }
    private void Update()
    {
        if(playerIn)
        if (player.interactAction.WasPressedThisFrame())
        {
            buttonPressed = true;
        }
    }
}
