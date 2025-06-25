using UnityEngine;

public class DialougeUIControl : MonoBehaviour
{
    public bool dialougeStarted;
    PlayerController p1;
    PlayerController p2;
    private void Update()
    {
        if (dialougeStarted)
        {

            if (PlayerManager.Instance.GetPlayer1().interactAction.WasPressedThisFrame() || PlayerManager.Instance.GetPlayer2().interactAction.WasPressedThisFrame())
            {

                DialogueManager.Instance.DisplayNextSentence();

            }
        }

    }
}
