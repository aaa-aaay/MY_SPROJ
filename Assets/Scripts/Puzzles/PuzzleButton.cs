using System.Runtime.CompilerServices;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    [SerializeField] private float playerNoFor;
    [SerializeField] private Canvas buttonUI;
    [SerializeField] private bool lockPlayer = false;
    private bool playerIn;
    public bool buttonPressed;
    [HideInInspector]public PlayerController player;


    private void Start()
    {
        buttonUI.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.StartsWith("Player")) {

            player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null) {


                playerIn = true;
                if (player.playerNo == 1)
                {
                    buttonUI.gameObject.layer = 6;
                }
                if (player.playerNo == 2)
                {
                    buttonUI.gameObject.layer = 7;
                }
                buttonUI.enabled = true;

            }



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
        if (player != null && playerIn && player.interactAction.WasPressedThisFrame())
        {
            AudioManager.instance.PlaySFX("UIConfirm");
            buttonPressed = true;
            if(lockPlayer) player.FreezePlayer(true);
        }
            
    }
}
