using UnityEngine;

public class OnTriggerDialouge : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
    private bool dialougeStarted;
    public bool dialougeFinished;
    private void Start()
    {
        dialougeFinished = false;
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialougeFinished)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.GetPlayer1().FreezePlayer(true);
            PlayerManager.Instance.GetPlayer2().FreezePlayer(true);
            dialogueTrigger.TriggerDialouge();
            dialougeStarted = true;
        }
    }

    private void Update()
    {
        if (dialougeStarted)
        {
            if (!DialogueManager.Instance.StillHaveDialogue())
            {
                PlayerManager.Instance.GetPlayer1().FreezePlayer(false);
                PlayerManager.Instance.GetPlayer2().FreezePlayer(false);
                dialougeStarted = false;
                dialougeFinished = true;
                return;
            }
            if (PlayerManager.Instance.GetPlayer1().interactAction.WasPressedThisFrame() || PlayerManager.Instance.GetPlayer2().interactAction.WasPressedThisFrame())
            {

                DialogueManager.Instance.DisplayNextSentence();

            }
        }
    }
}
